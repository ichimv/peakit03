using CrossCuttingConcerns.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomsService.Models
{
   public class RoomDTO : Entity<Guid>
   {
      public string RoomNumber { get; set; }
      public Guid RoomTypeId { get; set; }

      public bool IsFree { get; set; }
   }
}
