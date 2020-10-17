using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossCuttingConcerns.Models;
using Microsoft.Extensions.Logging;

namespace CrossCuttingConcerns.Repositories
{
   public class Repository : IRepository<Guid>
   {
      private readonly Dictionary<Guid, Entity<Guid>> _entities = new Dictionary<Guid, Entity<Guid>>();
      private readonly ILogger _logger;

      public Repository(ILogger<Repository> logger)
      {
         _logger = logger;
      }

      public IQueryable<Entity<Guid>> GetAll()
      {
         return _entities.Values.AsQueryable();
      }

      public Guid Add(Entity<Guid> newEntity)
      {
         if (newEntity.Id == Guid.Empty)
            newEntity.Id = Guid.NewGuid();
         else
         {
            if (_entities.ContainsKey(newEntity.Id))
            {
               throw new Exception("The specified entity already exist!");
            }
         }

         _entities[newEntity.Id] = newEntity;

         return newEntity.Id;
      }

      public Entity<Guid> Read(Guid existingEntityId)
      {
         if (!_entities.ContainsKey(existingEntityId))
         {
            throw new Exception("The specified entity" + existingEntityId + " does not exist!");
         }

         return _entities[existingEntityId];
      }

      public bool Update(Entity<Guid> existingEntity)
      {
         if (!_entities.ContainsKey(existingEntity.Id))
         {
            _logger.LogInformation("The specified entity" + existingEntity.Id + " does not exist!");
            return false;
         }

         _entities[existingEntity.Id] = existingEntity;
         return true;
      }

      public bool Delete(Guid existingEntityId)
      {
         if (!_entities.ContainsKey(existingEntityId))
         {
            _logger.LogInformation("The specified entity" + existingEntityId + " does not exist!");
            return false;
         }

         return _entities.Remove(existingEntityId);
      }
   }
}
