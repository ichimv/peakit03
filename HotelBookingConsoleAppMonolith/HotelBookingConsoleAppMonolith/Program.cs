using System;
using BookingBL;
using BookingDAL.Repositories;
using Microsoft.Extensions.Logging;


namespace HotelBookingConsoleApp
{
   class Program
   {
      static void Main(string[] args)
      {
         LoggerFactory loggerFactory = new LoggerFactory();

         ILogger<Repository> repositoryLogger = loggerFactory.CreateLogger<Repository>();
         ILogger<BookingManagement> bookingManagementLogger = loggerFactory.CreateLogger<BookingManagement>();

         Repository repository = new Repository(repositoryLogger);
         BookingManagement bookingManagement = new BookingManagement(bookingManagementLogger, repository);

         DateTime startDate = DateTime.Now;
         DateTime endDate = startDate.AddDays(1);

         try
         {
            Guid orderGuid1 = bookingManagement.AddBooking(new Guid(), "John", "Johanson",
               "Single", startDate, endDate);

            Guid orderGuid2 = bookingManagement.AddBooking(new Guid(), "John", "Johanson",
               "Double", startDate, endDate);

            Guid orderGuid3 = bookingManagement.AddBooking(new Guid(), "John", "Johanson",
               "Single", startDate, endDate);

            bookingManagement.CancelBooking(orderGuid1);

            orderGuid3 = bookingManagement.AddBooking(new Guid(), "John", "Johanson",
               "Single", startDate, endDate);

            bookingManagement.CancelBooking(orderGuid1);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }
   }
}
