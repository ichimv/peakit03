using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using BookingDAL.Models;
using CrossCuttingConcerns.Models;
using CrossCuttingConcerns.Repositories;
using Microsoft.Extensions.Logging;

namespace BookingBL
{
   public class BookingManagement
   {
      private IRepository<Guid> _repository;

      private RoomTypeDTO _singleRoomType;
      private RoomTypeDTO _doubleRoomType;
      private RoomTypeDTO _tripleRoomType;
      private RoomTypeDTO _penthouseRoomType;

      public BookingManagement(ILogger<BookingManagement> logger, IRepository<Guid> repository)
      {
         _repository = repository;

         AddRoomTypes();
         AddRooms();
      }

      private void AddRoomTypes()
      {
         _singleRoomType = new RoomTypeDTO { Title = "Single", Price = 50 };
         _doubleRoomType = new RoomTypeDTO { Title = "Double", Price = 100 };
         _tripleRoomType = new RoomTypeDTO { Title = "Triple", Price = 150 };
         _penthouseRoomType = new RoomTypeDTO { Title = "Penthouse", Price = 200 };

         _repository.Add(_singleRoomType);
         _repository.Add(_doubleRoomType);
         _repository.Add(_tripleRoomType);
         _repository.Add(_penthouseRoomType);
      }

      private void AddRooms()
      {
         _repository.Add(new RoomDTO { RoomNumber = "1", RoomTypeId = _singleRoomType.Id, IsFree = true });
         _repository.Add(new RoomDTO { RoomNumber = "2", RoomTypeId = _doubleRoomType.Id, IsFree = true });
         _repository.Add(new RoomDTO { RoomNumber = "3", RoomTypeId = _tripleRoomType.Id, IsFree = true });
         _repository.Add(new RoomDTO { RoomNumber = "4", RoomTypeId = _penthouseRoomType.Id, IsFree = true });
      }

      public Guid AddBooking(Guid guestIdentity, string firstName, string lastName,
         string roomTypeTitle, DateTime startDate, DateTime endDate)
      {
         RoomDTO availableRoom = GetAvailableRoomByRoomType(roomTypeTitle, startDate, endDate);
         if (availableRoom == null)
            return Guid.Empty;

         Guid guestGuid = GetGuestGuid(guestIdentity, firstName, lastName);
         OrderDTO orderDTO = new OrderDTO()
         {
            RoomId = availableRoom.Id,
            StartDate = startDate,
            EndDate = endDate,
            GuestId = guestGuid,
            Price = new PriceCalculator(_repository).CalculatePrice(availableRoom, (endDate - startDate).Days)
         };
         _repository.Add(orderDTO);
         availableRoom.IsFree = false;

         RoomHistoryDTO roomHistoryDTO = new RoomHistoryDTO()
         {
            RoomId = availableRoom.Id,
            GuestId = guestGuid,
            OrderId = orderDTO.Id
         };

         return orderDTO.Id;
      }

      public bool CancelBooking(Guid orderGuid)
      {
         OrderDTO orderDTO = _repository.Read(orderGuid) as OrderDTO;
         if (orderDTO == null)
            return false;

         RoomDTO roomDTO = _repository.Read(orderDTO.RoomId) as RoomDTO;
         roomDTO.IsFree = true;

         return _repository.Delete(orderGuid);
      }

      private RoomTypeDTO GetRoomType(RoomDTO roomDTO)
      {
         IQueryable<Entity<Guid>> allEntities = _repository.GetAll();
         foreach (var entity in allEntities)
         {
            RoomTypeDTO roomTypeDTO = entity as RoomTypeDTO;
            if (roomTypeDTO == null)
               continue;

            if (roomTypeDTO.Id == roomDTO.RoomTypeId)
            {
               return roomTypeDTO;
            }
         }
         throw new Exception("No room type for this room?");
      }

      private RoomDTO GetAvailableRoomByRoomType(string roomTypeTitle, DateTime startDate, DateTime endDate)
      {
         IQueryable<Entity<Guid>> allEntities = _repository.GetAll();
         foreach (var entity in allEntities)
         {
            RoomDTO roomDTO = entity as RoomDTO;
            if (roomDTO == null)
               continue;

            if (roomDTO.IsFree)
            {
               RoomTypeDTO roomTypeDTO = GetRoomType(roomDTO);
               if (!roomTypeDTO.Title.Equals(roomTypeTitle))
                  continue;

               return roomDTO;
            }
         }
         return null;
      }

      private Guid GetGuestGuid(Guid guestIdentity, string firstName, string lastName)
      {
         IQueryable<Entity<Guid>> allEntities = _repository.GetAll();
         foreach (var entity in allEntities)
         {
            GuestDTO guestDTO = entity as GuestDTO;
            if (guestDTO == null)
               continue;

            if (guestDTO.Identity == guestIdentity)
            {
               // Make sure first and second name are the same
               return guestDTO.Id;
            }
         }

         GuestDTO newGuestDTO = new GuestDTO()
         {
            Identity = guestIdentity,
            FirstName = firstName,
            LastName = lastName
         };
         _repository.Add(newGuestDTO);

         return newGuestDTO.Id;
      }
   }
}
