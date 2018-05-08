using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace tp1.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
               : base("DefaultConnection")
        { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}