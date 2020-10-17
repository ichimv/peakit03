using CrossCuttingConcerns.Models;
using System;

namespace GuestsService.Models
{
   public class GuestDTO : Entity<Guid>
   {
      public Guid Identity { get; set; }

      public string FirstName { get; set; }
      public string LastName { get; set; }
   }
}
