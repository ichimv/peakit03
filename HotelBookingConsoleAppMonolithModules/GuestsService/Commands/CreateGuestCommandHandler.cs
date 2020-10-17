using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossCuttingConcerns.Commands;
using GuestsService.BL;

namespace GuestsService.Commands
{
   public class CreateGuestCommandHandler : ICommandHandler<CreateGuestCommand>
   {
      public Task HandleAsync(CreateGuestCommand command, CancellationToken cancellationToken = default)
      {
         command.GuestId = GuestManagement.Instance()
            .GetGuestGuid(command);
         return Task.CompletedTask;
      }
   }
}
