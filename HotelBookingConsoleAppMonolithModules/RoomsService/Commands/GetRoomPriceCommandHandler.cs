using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossCuttingConcerns.Commands;
using RoomsService.BL;

namespace RoomsService.Commands
{
   public class GetRoomPriceCommandHandler : ICommandHandler<GetRoomPriceCommand>
   {
      public Task HandleAsync(GetRoomPriceCommand command, CancellationToken cancellationToken = default)
      {
         RoomManagement.Instance()
            .GetRoomPrice(command);
         return Task.CompletedTask;
      }
   }
}
