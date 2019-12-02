using Alinta.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alinta.Core.Infrastructure
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext()
        {
        }

        public CustomerDBContext(DbContextOptions<CustomerDBContext> options)
       : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }         
    }
}
