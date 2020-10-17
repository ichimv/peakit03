using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDAL.Models
{
   public class RoomTypeDTO : Entity<Guid>
   {
      public string Title { get; set; }
      public decimal Price { get; set; }
   }
}
