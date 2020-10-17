using System;
using System.Collections.Generic;
using System.Text;
using CrossCuttingConcerns.Queries;

namespace RoomsService.Queries
{
   public class AvailableRoomQuery : IQuery<AvailableRoomQueryResult>
   {
      public string RoomTypeTitle { get; set; }
   }
}
