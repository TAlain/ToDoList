namespace ToDoList.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ToDoList.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDoList.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ToDoList.Models.ApplicationDbContext";
        }

        protected override void Seed(ToDoList.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.Email == "Alain_thoen@hotmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "Alain_thoen@hotmail.com"};

                manager.Create(user, "password");

            }
        }
    }
}
