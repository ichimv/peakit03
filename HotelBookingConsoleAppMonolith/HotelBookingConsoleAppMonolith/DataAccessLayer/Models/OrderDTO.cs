using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDAL.Models
{
   public class OrderDTO : Entity<Guid>
   {
      public Guid RoomId { get; set; }
      public Guid GuestId { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public decimal Price { get; set; }
   }
}
