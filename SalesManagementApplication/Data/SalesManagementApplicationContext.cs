using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesManagementApplication.Models;

namespace SalesManagementApplication.Data
{
    public class SalesManagementApplicationContext : DbContext
    {
        public SalesManagementApplicationContext (DbContextOptions<SalesManagementApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<SalesManagementApplication.Models.Seller> Seller { get; set; } = default!;

        public DbSet<SalesManagementApplication.Models.Sale>? Sale { get; set; }
    }
}
