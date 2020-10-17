using System;
using System.Collections.Generic;
using System.Text;
using CrossCuttingConcerns.Queries;

namespace OrdersService.Queries
{
   public class RoomAttachedToOrderQuery : IQuery<RoomAttachedToOrderQueryResult>
   {
      public Guid OrderId { get; set; }
   }
}
