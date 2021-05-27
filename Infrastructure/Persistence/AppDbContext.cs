using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zedcrest.DocumentManager.Domain.Entities;

namespace Zedcrest.DocumentManager.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public AppDbContext() { 
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual  DbSet<Document> Documents { get; set; }
    }
}
