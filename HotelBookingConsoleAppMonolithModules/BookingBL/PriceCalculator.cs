using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookingDAL.Models;
using CrossCuttingConcerns.Models;
using CrossCuttingConcerns.Repositories;

namespace BookingBL
{
   public class PriceCalculator
   {
      private IRepository<Guid> _repository;

      public PriceCalculator(IRepository<Guid> repository)
      {
         _repository = repository;
      }

      public decimal CalculatePrice(RoomDTO roomDTO, int daysNumber)
      {
         RoomTypeDTO roomTypeDTO = GetRoomType(roomDTO);
         return roomTypeDTO.Price * daysNumber;
      }

      protected RoomTypeDTO GetRoomType(RoomDTO roomDTO)
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
   }
}
