using System;
using System.Collections.Generic;
using System.Text;

namespace BookingDAL.Models
{
   public abstract class Entity<T>
   {
      public T Id { get; set; }
   }
}
