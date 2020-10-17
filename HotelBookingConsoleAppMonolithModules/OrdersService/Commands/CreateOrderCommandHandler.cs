using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossCuttingConcerns.Commands;
using OrdersService.BL;

namespace OrdersService.Commands
{
   public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
   {
      public Task HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
      {
         command.OrderId = OrderManagement.Instance()
            .GetOrderGuid(command);
         return Task.CompletedTask;
      }
   }
}
