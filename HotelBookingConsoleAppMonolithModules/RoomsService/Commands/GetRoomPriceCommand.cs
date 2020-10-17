using System;
using System.Collections.Generic;
using System.Text;
using CrossCuttingConcerns.Commands;

namespace RoomsService.Commands
{
   public class GetRoomPriceCommand : ICommand
   {
      public Guid RoomId { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }

      public decimal Price { get; set; }
   }
}
