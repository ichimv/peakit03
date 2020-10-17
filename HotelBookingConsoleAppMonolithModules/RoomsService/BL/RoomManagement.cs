using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossCuttingConcerns.Models;
using CrossCuttingConcerns.Repositories;
using Microsoft.Extensions.Logging;
using RoomsService.Commands;
using RoomsService.Models;

namespace RoomsService.BL
{
   public class RoomManagement
   {
      static RoomManagement _singleton;

      public static RoomManagement Instance()
      {
         if (_singleton == null)
            _singleton = new RoomManagement();

         return _singleton;
      }

      private IRepository<Guid> _repository;
      private RoomTypeDTO _singleRoomType;
      private RoomTypeDTO _doubleRoomType;
      private RoomTypeDTO _tripleRoomType;
      private RoomTypeDTO _penthouseRoomType;

      public RoomManagement()
      {
         LoggerFactory loggerFactory = new LoggerFactory();
         ILogger<Repository> repositoryLogger = loggerFactory.CreateLogger<Repository>();
         _repository = new Repository(repositoryLogger);

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

      public void GetRoomPrice(GetRoomPriceCommand getRoomPriceCommand)
      {
         RoomDTO roomDTO = _repository.Read(getRoomPriceCommand.RoomId) as RoomDTO;
         getRoomPriceCommand.Price = new PriceCalculator(_repository).CalculatePrice(roomDTO, (getRoomPriceCommand.EndDate - getRoomPriceCommand.StartDate).Days);
      }


      public Guid GetAvailableRoomGuid(string roomTypeTitle)
      {
         RoomDTO roomDTO = GetAvailableRoomByRoomType(roomTypeTitle);
         return roomDTO.Id;
      }

      public void UpdateRoomStatus(UpdateRoomStatusCommand command)
      {
         RoomDTO roomDTO = _repository.Read(command.RoomId) as RoomDTO;
         roomDTO.IsFree = command.IsFree;

         _repository.Update(roomDTO);
      }

      private RoomDTO GetAvailableRoomByRoomType(string roomTypeTitle)
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
   }
}
