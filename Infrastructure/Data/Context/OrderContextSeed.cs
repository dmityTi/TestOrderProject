using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context)
        {
            UseRealDataBase(context);
            //OR:
            //UseTestDataBase(context);

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrders());
            }
            
            await context.SaveChangesAsync();
        }

        private static void UseRealDataBase(OrderContext context)
        {
            context.Database.Migrate();
        }
        
        private static void UseTestDataBase(OrderContext context)
        {
            context.Database.EnsureDeleted();
            // the database that is created cannot be later updated using migrations.
            context.Database.EnsureCreated();
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 70002234,
                    Date = DateTime.Now, 
                    Status = OrderStatus.Cancelled, 
                    InvoiceNumber = 10,
                    Articles = new List<Article>
                    {
                        new Article
                        {
                            Nomenclature = "00471520079",
                            Title = "Track Suit Women",
                            Amount = 2,
                            BrutPrice = 120
                        }
                    },
                    Payments = new List<Payment>
                    {
                        new Payment
                        {
                            MethodName = "INVOICE",
                            Amount = 140
                        }
                    },
                    BillingAddress = new BillingAddress
                    {
                        Email = "wittmann.k@web.de",
                        FullName = "Krissi Wittmann",
                        Street = "Allerstrasse",
                        HomeNumber = 47,
                        City = "Berlin",
                        Country = "DE",
                        Zip = 12049
                    }
                }
            };
        }
    }
}