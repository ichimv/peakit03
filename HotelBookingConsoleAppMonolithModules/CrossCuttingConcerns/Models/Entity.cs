using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCuttingConcerns.Models
{
   public abstract class Entity<T>
   {
      public T Id { get; set; }
   }
}
