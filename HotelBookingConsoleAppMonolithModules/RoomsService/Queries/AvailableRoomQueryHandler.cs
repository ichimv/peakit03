using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossCuttingConcerns.Queries;
using RoomsService.BL;

namespace RoomsService.Queries
{
   public class AvailableRoomQueryHandler : IQueryHandler<AvailableRoomQuery, AvailableRoomQueryResult>
   {
      public Task<AvailableRoomQueryResult> HandleAsync(AvailableRoomQuery query, CancellationToken cancellationToken = default)

      {
         AvailableRoomQueryResult availableRoomQueryResult = new AvailableRoomQueryResult()
         {
            AvailableRoomGuid = RoomManagement.Instance()
               .GetAvailableRoomGuid(query.RoomTypeTitle)
         };

         return Task.FromResult(availableRoomQueryResult);
      }

   }
}
