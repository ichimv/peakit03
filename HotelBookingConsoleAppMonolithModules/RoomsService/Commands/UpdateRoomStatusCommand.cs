using System;
using System.Collections.Generic;
using System.Text;
using CrossCuttingConcerns.Commands;

namespace RoomsService.Commands
{
   public class UpdateRoomStatusCommand : ICommand
   {
      public Guid RoomId { get; set; }
      public bool IsFree { get; set; }
   }
}
