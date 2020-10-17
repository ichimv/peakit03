using GuestsService.Commands;
using OrdersService.Commands;
using RoomsService.Commands;
using System;
using System.Threading.Tasks;
using OrdersService.Queries;
using RoomsService.Queries;

namespace HotelBookingConsoleAppCQRS
{
   class Program
   {
      static void Main(string[] args)
      {
         try
         {
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(1);

            Guid orderGuid1 = AddBookingAsync(Guid.NewGuid(), "John", "Johanson",
               "Single", startDate, endDate).Result;

            Guid orderGuid2 = AddBookingAsync(Guid.NewGuid(), "John", "Johanson",
               "Double", startDate, endDate).Result;

            CancelBookingAsync(orderGuid1).ConfigureAwait(true);

            orderGuid1 = AddBookingAsync(Guid.NewGuid(), "John", "Johanson",
               "Single", startDate, endDate).Result;

            CancelBookingAsync(orderGuid2).ConfigureAwait(true);

         }
         catch (Exception e)
         {
            Console.WriteLine(e);
         }
      }

      static async Task<Guid> AddBookingAsync(Guid guestIdentity, string firstName, string lastName,
         string roomTypeTitle, DateTime startDate, DateTime endDate)
      {
         var availableRoomQuery = new AvailableRoomQuery() { RoomTypeTitle = roomTypeTitle };
         var availableRoomQueryHandler = new AvailableRoomQueryHandler();
         AvailableRoomQueryResult availableRoomQueryResult = await availableRoomQueryHandler.HandleAsync(availableRoomQuery);

         var roomPriceCommand = new GetRoomPriceCommand()
         {
            RoomId = availableRoomQueryResult.AvailableRoomGuid,
            StartDate = startDate,
            EndDate = endDate
         };
         var roomPriceCommandHandler = new GetRoomPriceCommandHandler();
         await roomPriceCommandHandler.HandleAsync(roomPriceCommand);

         var createGuestCommand = new CreateGuestCommand()
         {
            Identity = guestIdentity,
            FirstName = firstName,
            LastName = lastName
         };
         var createGuestCommandHandler = new CreateGuestCommandHandler();
         await createGuestCommandHandler.HandleAsync(createGuestCommand);

         var createOrderCommand = new CreateOrderCommand()
         {
            RoomId = availableRoomQueryResult.AvailableRoomGuid,
            GuestId = createGuestCommand.GuestId,
            StartDate = startDate,
            EndDate = endDate,
            Price = roomPriceCommand.Price
         };
         var createOrderCommandHandler = new CreateOrderCommandHandler();
         await createOrderCommandHandler.HandleAsync(createOrderCommand);

         return createOrderCommand.OrderId;
      }

      static async Task CancelBookingAsync(Guid orderGuid)
      {
         var roomIdAttachedToOrderQuery = new RoomAttachedToOrderQuery() { OrderId = orderGuid };
         var roomIdAttachedToOrderQueryHandler = new RoomAttachedToOrderQueryHandler();
         RoomAttachedToOrderQueryResult roomIdAttachedToOrderQueryResult = await roomIdAttachedToOrderQueryHandler.HandleAsync(roomIdAttachedToOrderQuery);
         if (roomIdAttachedToOrderQueryResult.RoomId == Guid.Empty)
            return;

         var updateRoomStatusCommand = new UpdateRoomStatusCommand()
         {
            RoomId = roomIdAttachedToOrderQueryResult.RoomId,
            IsFree = true
         };
         var updateRoomStatusCommandHandler = new UpdateRoomStatusCommandHandler();
         await updateRoomStatusCommandHandler.HandleAsync(updateRoomStatusCommand);

         var deleteOrderCommand = new DeleteOrderCommand() { OrderGuid = orderGuid };
         var deleteOrderCommandHandler = new DeleteOrderCommandHandler();
         await deleteOrderCommandHandler.HandleAsync(deleteOrderCommand);
      }
   }
}
