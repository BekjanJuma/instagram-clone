using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Instagram.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : this("DefaultConnection") { }

        public ApplicationContext(string connStringName) : base(connStringName) { }

        public static ApplicationContext Create()
        {
            var context = new ApplicationContext();
            context.Database.CreateIfNotExists();
            return context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}