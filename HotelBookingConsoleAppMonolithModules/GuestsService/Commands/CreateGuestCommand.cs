using System;
using System.Collections.Generic;
using System.Text;
using CrossCuttingConcerns.Commands;

namespace GuestsService.Commands
{
   public class CreateGuestCommand : ICommand
   {
      public Guid Identity { get; set; }

      public string FirstName { get; set; }
      public string LastName { get; set; }

      public Guid GuestId { get; set; }
   }
}
