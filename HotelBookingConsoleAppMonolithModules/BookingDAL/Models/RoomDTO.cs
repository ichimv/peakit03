using System;
using System.Collections.Generic;
using System.Text;
using CrossCuttingConcerns.Models;

namespace BookingDAL.Models
{
   public class RoomDTO : Entity<Guid>
   {
      public string RoomNumber { get; set; }
      public Guid RoomTypeId { get; set; }

      public bool IsFree { get; set; }
   }
}
