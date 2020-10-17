using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossCuttingConcerns.Commands;
using RoomsService.BL;

namespace RoomsService.Commands
{
   public class UpdateRoomStatusCommandHandler : ICommandHandler<UpdateRoomStatusCommand>
   {
      public Task HandleAsync(UpdateRoomStatusCommand command, CancellationToken cancellationToken = default)
      {
         RoomManagement.Instance()
            .UpdateRoomStatus(command);
         return Task.CompletedTask;
      }
   }
}
