using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossCuttingConcerns.Models;
using CrossCuttingConcerns.Repositories;
using GuestsService.Commands;
using GuestsService.Models;
using Microsoft.Extensions.Logging;

namespace GuestsService.BL
{
   public class GuestManagement
   {
      static GuestManagement _singleton;

      public static GuestManagement Instance()
      {
         if (_singleton == null)
            _singleton = new GuestManagement();

         return _singleton;
      }

      private IRepository<Guid> _repository;

      public GuestManagement()
      {
         LoggerFactory loggerFactory = new LoggerFactory();
         ILogger<Repository> repositoryLogger = loggerFactory.CreateLogger<Repository>();
         _repository = new Repository(repositoryLogger);
      }

      public Guid GetGuestGuid(CreateGuestCommand command)
      {
         IQueryable<Entity<Guid>> allEntities = _repository.GetAll();
         foreach (var entity in allEntities)
         {
            GuestDTO guestDTO = entity as GuestDTO;
            if (guestDTO == null)
               continue;

            if (guestDTO.Identity == command.Identity)
            {
               // Make sure first and second name are the same
               return guestDTO.Id;
            }
         }

         GuestDTO newGuestDTO = new GuestDTO()
         {
            Identity = command.Identity,
            FirstName = command.FirstName,
            LastName = command.LastName
         };
         _repository.Add(newGuestDTO);

         return newGuestDTO.Id;
      }
   }
}
