using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossCuttingConcerns.Commands;
using OrdersService.BL;

namespace OrdersService.Commands
{
   public class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
   {
      public Task HandleAsync(DeleteOrderCommand command, CancellationToken cancellationToken = default)
      {
         command.Succeed = OrderManagement.Instance()
            .DeleteOrder(command);
         return Task.CompletedTask;
      }
   }
}
