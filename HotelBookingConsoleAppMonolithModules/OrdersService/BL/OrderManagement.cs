using System;
using System.Collections.Generic;
using System.Text;
using CrossCuttingConcerns.Repositories;
using Microsoft.Extensions.Logging;
using OrdersService.Commands;
using OrdersService.Models;
using OrdersService.Queries;

namespace OrdersService.BL
{
   public class OrderManagement
   {
      static OrderManagement _singleton;
      public static OrderManagement Instance()
      {
         if (_singleton == null)
            _singleton = new OrderManagement();

         return _singleton;
      }

      private IRepository<Guid> _repository;

      public OrderManagement()
      {
         LoggerFactory loggerFactory = new LoggerFactory();
         ILogger<Repository> repositoryLogger = loggerFactory.CreateLogger<Repository>();
         _repository = new Repository(repositoryLogger);
      }

      public Guid GetRoomGuid(RoomAttachedToOrderQuery roomIdAttachedToOrderQuery)
      {
         OrderDTO orderDTO = _repository.Read(roomIdAttachedToOrderQuery.OrderId) as OrderDTO;
         if (orderDTO == null)
            return Guid.Empty;
         return orderDTO.RoomId;
      }

      public Guid GetOrderGuid(CreateOrderCommand createOrderCommand)
      {
         OrderDTO orderDTO = new OrderDTO()
         {
            RoomId = createOrderCommand.RoomId,
            StartDate = createOrderCommand.StartDate,
            EndDate = createOrderCommand.EndDate,
            GuestId = createOrderCommand.GuestId,
            Price = createOrderCommand.Price
         };
         _repository.Add(orderDTO);
         createOrderCommand.OrderId = orderDTO.Id;

         return orderDTO.Id;
      }

      public bool DeleteOrder(DeleteOrderCommand deleteOrderCommand)
      {
         return _repository.Delete(deleteOrderCommand.OrderGuid);
      }
   }
}
