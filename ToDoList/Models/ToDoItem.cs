using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Workstate Workstate  { get; set; }
        public virtual string ApplicationUser_Id { get; set; }
        [Display(Name="Assigned User")]
        [ForeignKey("ApplicationUser_Id")]
        public virtual ApplicationUser AssignedUser { get; set; }
    }
    public enum Workstate
    {
        [Display(Name="To Do")]
        Todo,
        [Display(Name="In Progress")]
        InProgress,
        Done
    }
}