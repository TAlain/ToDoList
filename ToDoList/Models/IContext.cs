using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public interface IContext
    {
        DbSet<ToDoItem> ToDoItems { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
        int SaveChanges();
        void Dispose();
        DbEntityEntry Entry(object entity);
    }
}
