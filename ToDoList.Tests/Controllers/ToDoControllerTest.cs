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
    public class ToDoControllerTest
    {
        IContext mocksetTodo;
        ToDoItemsController controller;
        ToDoItem TodoItem;

        [SetUp]
        public void SetUp()
        {
            mocksetTodo = new TestDbContext();
            controller = new ToDoItemsController(mocksetTodo);
            TodoItem = new Mock<ToDoItem>().Object;
        }

        #region Basic Cruds
        [Test]
        public void ToDoIndex()
        {
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView().WithModel<List<ToDoItem>>();
        }

        [Test]
        public void ToDoDetails()
        {
            //fail
            controller.WithCallTo(c => c.Details(TodoItem.Id)).ShouldGiveHttpStatus(404);

            //save and success
            mocksetTodo.ToDoItems.Add(TodoItem);
            controller.WithCallTo(c => c.Details(TodoItem.Id)).ShouldRenderDefaultView().WithModel<ToDoItem>();
        }

        [Test]//post
        public void ToDoCreate()
        {
            //back to default if invalid parameter
            controller.WithCallTo(c => c.Create()).ShouldRenderDefaultView();

            //to index if saved
            controller.WithCallTo(c => c.Create(TodoItem)).ShouldRedirectTo(c => c.Index);
            Assert.That(mocksetTodo.ToDoItems, Contains.Item(TodoItem));
        }
        
        #endregion


    }
}
