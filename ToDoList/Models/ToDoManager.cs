using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace ToDoList.Models
{
    public class ToDoManager
    {
        public ApplicationUser CurrentUser { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();

        public ToDoManager(ApplicationUser currentuser)
        {
            CurrentUser = currentuser;
        }
        public bool CurrentUserPicksUp(ToDoItem item)
        {
            if (UserHasSkillsFor(item))
            {
                CurrentUser.AssignedItems.Add(item);
                item.Workstate = Workstate.InProgress;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CurrentUserReleaseItem(ToDoItem item)
        {
            item.ApplicationUser_Id = null;
            item.Workstate = Workstate.Todo;
        }

        private bool UserHasSkillsFor(ToDoItem item)
        {
            bool valid = true;
            if (item.ToDoSkills.Count != 0)
            {               
                foreach (Skill itemSkill in item.ToDoSkills)
                {
                    if (UserDoesNotHave(itemSkill))
                    {
                        return false;
                    } 
                }
                valid = IfUserHasAnySkills();

            }
            return valid;
        }

        private bool UserDoesNotHave(Skill itemSkill)
        {
            if (!CurrentUser.UserSkills.Contains(itemSkill))
            {
                return true;
            }
            return false;
        }

        private bool IfUserHasAnySkills()
        {
            if (CurrentUser.UserSkills.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}