using Alinta.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alinta.Core.Infrastructure
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CustomerDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<CustomerDBContext>>()))
            {
                // Look for any board games.
                if (context.Customers.Any())
                {
                    return;   // Data was already seeded
                }

                context.Customers.AddRange(
                    new Customer()
                    {
                        Id = 1,
                        FirstName = "Katie",
                        LastName = "Davis",
                        DateOfBirth = Convert.ToDateTime("04/01/1959")
                    },
                    new Customer()
                    {
                        Id = 2,
                        FirstName = "Jacob",
                        LastName = "Grimm",
                        DateOfBirth = Convert.ToDateTime("04/01/1785")
                    },
                    new Customer()
                    {
                        Id = 3,
                        FirstName = "Judith",
                        LastName = "Viorst",
                        DateOfBirth = Convert.ToDateTime("02/02/1931")
                    },
                    new Customer()
                    {
                        Id = 4,
                        FirstName = "Dave",
                        LastName = "Berry",
                        DateOfBirth = Convert.ToDateTime("03/07/1947")
                    },
                    new Customer()
                    {
                        Id = 5,
                        FirstName = "James",
                        LastName = "Berenstain",
                        DateOfBirth = Convert.ToDateTime("26/07/1923")
                    },
                    new Customer()
                    {
                        Id = 6,
                        FirstName = "Neil",
                        LastName = "Gailman",
                        DateOfBirth = Convert.ToDateTime("10/11/1960")
                    });

                context.SaveChanges();
            }
        }
    }
}
