using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesManagementApplication.Data;
using System;
using System.Linq;

namespace SalesManagementApplication.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SalesManagementApplicationContext(
                serviceProvider.GetRequiredService<DbContextOptions<SalesManagementApplicationContext>>()))
            {
                if (!context.Seller.Any())
                {
                    var salesFor0 = new List<Sale>
                    {new Sale
                        {
                            CreatedDate = new DateTime(2022,1,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,2,3),
                            Price = 25.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,3,3),
                            Price = 476.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,4,3),
                            Price = 1222.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,5,3),
                            Price = 65.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,6,3),
                            Price = 96.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,6,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,7,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,7,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,8,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,8,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,8,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,8,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2022,8,3),
                            Price = 12.4m,
                        }
                    };
                    var salesFor1 = new List<Sale>
                    {
                        new Sale
                        {
                            CreatedDate = new DateTime(2021,6,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2021,6,3),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2021,6,3),
                            Price = 12.4m,
                        },
                    };
                    var salesFor2 = new List<Sale>
                    {
                        new Sale
                        {
                            CreatedDate = new DateTime(2020,5,21),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2020,5,21),
                            Price = 12.4m,
                        },
                        new Sale
                        {
                            CreatedDate = new DateTime(2020,5,21),
                            Price = 12.4m,
                        },
                    };
                    context.Seller.AddRange(
                       new Seller
                       {
                           Name = "Fox",
                           LastName = "Mulder",
                           Sales = salesFor0,
                       },
                       new Seller
                       {
                           Name = "Dana",
                           LastName = "Scully",
                           Sales = salesFor1
                       }, new Seller
                       {
                           Name = "Mr.",
                           LastName = "Robot",
                           Sales = salesFor2
                       }
                       );
                }
                context.SaveChanges();


            }
        }
    }
}
