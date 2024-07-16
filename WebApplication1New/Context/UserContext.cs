using KrishnaDairyDotNetApi.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1New.Entity;

namespace WebApplication1New.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options)
          : base(options)
        {
        }
        public DbSet<UserEntity> Customertable { get; set; }

        public DbSet<ProductEntity> ProductTable { get; set; }

        public DbSet<CartEntity> CartTable { get; set; }
    }
}
