using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace LoginUsingJwtV2.Models
{
    public class LoginDbContext : DbContext
    {
        

        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {

        }
       
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<AppUserClaims> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }



       

        //protected override void OnConfiguring(
        //        DbContextOptionsBuilder optionsBuilder)
        //{
           
        // }


    }

}
