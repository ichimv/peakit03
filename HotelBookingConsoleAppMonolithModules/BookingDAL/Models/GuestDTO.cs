using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDAL.Models
{
   public class GuestDTO : Entity<Guid>
   {
      public Guid Identity { get; set; }

      public string FirstName { get; set; }
      public string LastName { get; set; }
   }
}
