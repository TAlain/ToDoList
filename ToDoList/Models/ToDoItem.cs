using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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

        public virtual ICollection<Skill> ToDoSkills { get; set; }
        public List<int> SelectedSkills { get; set; }

        public ToDoItem()
        {
            ToDoSkills = new List<Skill>();
            SelectedSkills = new List<int>();
        }

        public ToDoItem EditValues(ToDoItem edit, DbSet<Skill> skills)
        {
                AddSkills(edit.SelectedSkills, skills);
                Title = edit.Title;
                Description = edit.Description;
                Workstate = edit.Workstate;
                ApplicationUser_Id = edit.ApplicationUser_Id;
                return this;
        }


        public void AddSkills(List<int> selectedSkills, DbSet<Skill> skills)
        {
            ToDoSkills.Clear();
            foreach (var skillId in selectedSkills)
            {
                ToDoSkills.Add(skills.Find(skillId));
            }
        }
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