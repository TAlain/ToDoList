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
                var user = new ApplicationUser { Email = "Alain@hotmail.com", UserName ="alain"};
                manager.Create(user, "password");
                user = new ApplicationUser { Email = "Jos@hotmail.com", UserName = "Jos" };
                manager.Create(user, "password");
                user = new ApplicationUser { Email = "Bob@hotmail.com", UserName = "Bob" };
                manager.Create(user, "password");
                user = new ApplicationUser { Email = "Kim@hotmail.com", UserName = "Kim" };
                manager.Create(user, "password");
                user = new ApplicationUser { Email = "Jef@hotmail.com", UserName = "Jef" };
                manager.Create(user, "password");
                user = new ApplicationUser { Email = "Annette@hotmail.com", UserName = "Annette" };
                manager.Create(user, "password");

                context.ToDoItems.AddOrUpdate(i => i.Id,
                    new ToDoItem { Title = "Make UML",            Description = "Write And Print UML",         Workstate = Workstate.Todo },
                    new ToDoItem { Title = "Write Tests",         Description = "Install Nunit start design",  Workstate = Workstate.Todo },
                    new ToDoItem { Title = "Create Models",       Description = "Code First Models",           Workstate = Workstate.Todo },
                    new ToDoItem { Title = "Create Controllers",  Description = "Scaffold Away",               Workstate = Workstate.Todo },
                    new ToDoItem { Title = "Create Views",        Description = "Razor @clean",                Workstate = Workstate.Todo },
                    new ToDoItem { Title = "Design",              Description = "HallWay UIX and PhotoShop",   Workstate = Workstate.Todo }
                    );

                context.Skills.AddOrUpdate(i => i.Id,
                    new Skill { Title = "Architecture"},
                    new Skill { Title = "C#"},
                    new Skill { Title = "Nunit"},
                    new Skill { Title = "HTML,"},
                    new Skill { Title = "CSS,"},
                    new Skill { Title = "ColorFever"}                 
                    );

            }
        }
    }
}
