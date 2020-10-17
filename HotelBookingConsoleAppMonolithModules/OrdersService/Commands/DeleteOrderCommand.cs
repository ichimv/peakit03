using System;
using System.Collections.Generic;
using System.Text;
using CrossCuttingConcerns.Commands;

namespace OrdersService.Commands
{
   public class DeleteOrderCommand : ICommand
   {
      public Guid OrderGuid { get; set; }

      public bool Succeed { get; set; }
   }
}
