using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}