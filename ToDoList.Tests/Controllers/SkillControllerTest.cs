using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ToDoList.Controllers;
using ToDoList.Models;
using TestStack.FluentMVCTesting;


namespace ToDoList.Tests.Controllers
{
    [TestFixture]
    public class SkillControllerTest
    {
        IContext mocksetTodo;
        SkillsController controller;
        Skill skill;
        [SetUp]
        public void SetUp()
        {
            mocksetTodo = new TestDbContext();
            controller = new SkillsController(mocksetTodo);
            skill = new Mock<Skill>().Object;
        }
        #region Basic Crud

        [Test]
        public void SkillIndex()
        {
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView().WithModel<List<Skill>>();
        }

        [Test]//post
        public void ToDoEdit()
        {
            //back to default if invalid parameter
            controller.WithCallTo(c => c.Edit(skill.Id + 1)).ShouldGiveHttpStatus(404);

            //to index if saved
            controller.WithCallTo(c => c.Edit(skill)).ShouldRedirectTo(c => c.Index);
        }
        
        //etc  See ToDoController for more examples
        
        #endregion
    }
}
