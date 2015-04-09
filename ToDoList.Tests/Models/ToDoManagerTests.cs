using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Tests.Models
{
    [TestFixture]
    public class ToDoManagerTests
    {
        ToDoManager toDoManager;
        ToDoItem item;
        Skill skill1;
        Skill skill2;
        ApplicationUser user;

        [SetUp]
        public void Setup()
        {
            user = new ApplicationUser { Email = "Alain@hotmail.com", UserName = "alain" };
            skill1 = new Mock<Skill>().Object; skill1.Title = "Skill1";
            skill2 = new Mock<Skill>().Object; skill2.Title = "Skill2";
            item = new ToDoItem(); item.ToDoSkills.Add(skill1);
            toDoManager = new ToDoManager(user);
        }

        [Test]
        public void AssignWithoutNoSkills()
        {
            toDoManager.CurrentUserPicksUp(item);

            Assert.That(user.AssignedItems, Is.Not.Contains(item));
        }

        [Test]
        public void AssignWithRequiredSkills()
        {
            user.UserSkills.Add(skill1);
            toDoManager.CurrentUserPicksUp(item);

            Assert.That(user.AssignedItems,Contains.Item(item));
        }
        [Test]
        public void AssignWithPartialSkills()
        {
            user.UserSkills.Add(skill1);
            item.ToDoSkills.Add(skill2);
            toDoManager.CurrentUserPicksUp(item);

            Assert.That(user.AssignedItems, Is.Not.Contains(item));
        }


    }
}
