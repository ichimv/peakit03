using System;
using System.Collections.Generic;
using System.Text;
using CrossCuttingConcerns.Commands;

namespace OrdersService.Commands
{
   public class CreateOrderCommand : ICommand
   {
      public Guid RoomId { get; set; }
      public Guid GuestId { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public decimal Price { get; set; }

      public Guid OrderId { get; set; }
   }
}
