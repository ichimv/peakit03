using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossCuttingConcerns.Queries;
using OrdersService.BL;

namespace OrdersService.Queries
{
   public class RoomAttachedToOrderQueryHandler : IQueryHandler<RoomAttachedToOrderQuery, RoomAttachedToOrderQueryResult>
   {
      public Task<RoomAttachedToOrderQueryResult> HandleAsync(RoomAttachedToOrderQuery query, CancellationToken cancellationToken = default)

      {
         RoomAttachedToOrderQueryResult roomIdAttachedToOrderQueryResult = new RoomAttachedToOrderQueryResult()
         {
            RoomId = OrderManagement.Instance().GetRoomGuid(query)
         };

         return Task.FromResult(roomIdAttachedToOrderQueryResult);
      }
   }
}
