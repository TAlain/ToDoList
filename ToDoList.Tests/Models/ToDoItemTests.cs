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
    public class ToDoItemTests
    {
        ToDoItem item;
        ToDoItem edit;
        Skill skill1;
        List<Skill> skills = new List<Skill>();

        [SetUp]
        public void Setup()
        {
            skill1 = new Mock<Skill>().Object;
            skills.Add(skill1);
            item = new ToDoItem() { Title= "Title", Description ="Description", Workstate=Workstate.Todo };
            edit = new ToDoItem() { Title = "Edited", Description = "Edited", Workstate = Workstate.Done };
            edit.SelectedSkills.Add(item.Id);
        }
        [Test]
        public void EditItem()
        {
            item.EditValues(edit, skills);
            
            Assert.That(item.Title, Is.EqualTo("Edited"));
            Assert.That(item.Description, Is.EqualTo("Edited"));
            Assert.That(item.Workstate, Is.EqualTo(Workstate.Done));
            Assert.That(item.ToDoSkills, Contains.Item(skill1));
        }
    }
}
