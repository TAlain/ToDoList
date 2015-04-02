using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using Microsoft.AspNet.Identity;

namespace ToDoList.Controllers
{
    [Authorize]
    public class ToDoItemsController : Controller
    {
        private IContext db;
        ToDoManager manager;


        public ToDoItemsController() 
        {
             this.db = new ApplicationDbContext();
        }
        //Context for testing
        public ToDoItemsController(IContext context) 
        {
            this.db = context;
        }


        // GET: ToDoItems
        public ActionResult Index()
        {            
            var toDoItems = db.ToDoItems.Include(i => i.AssignedUser);
            return View(toDoItems.ToList());
        }

        [HttpPost]
        public ActionResult UserPickup([Bind(Include="item_id")]int item_id)
        {
            var item = db.ToDoItems.Find(item_id);
            ModelState.Clear();
            manager = new ToDoManager(db.Users.Find(User.Identity.GetUserId()));

            if(manager.CurrentUserPicksUp(item))
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "You don't have the required Skills!");
            var toDoItems = db.ToDoItems.Include(i => i.AssignedUser);
            return View("Index",toDoItems.ToList());
 
        }
        [HttpPost]
        public ActionResult UserRelease([Bind(Include = "item_id")]int item_id)
        {
            var item = db.ToDoItems.Find(item_id);
            manager = new ToDoManager(db.Users.Find(User.Identity.GetUserId()));
            manager.CurrentUserReleaseItem(item);
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: ToDoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "UserName");
            ViewData["Skills"] = new MultiSelectList(db.Skills, "Id", "Title");
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Workstate,ApplicationUser_Id,SelectedSkills")] ToDoItem toDoItem)
        {
            ViewData["Skills"] = new MultiSelectList(db.Skills, "Id", "Title", toDoItem.SelectedSkills);
            if (ModelState.IsValid)
            {
                toDoItem.AddSkills(toDoItem.SelectedSkills, db.Skills.ToList());

                db.ToDoItems.Add(toDoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "Email", toDoItem.ApplicationUser_Id);
            return View(toDoItem);
        }

        // GET: ToDoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            ViewData["Skills"] = new MultiSelectList(db.Skills, "Id", "Title", toDoItem.ToDoSkills.Select(x =>  x.Id).ToArray());
            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "Email", toDoItem.ApplicationUser_Id);
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Workstate,ApplicationUser_Id,SelectedSkills")] ToDoItem toDoItem)
        {
            ViewData["Skills"] = new MultiSelectList(db.Skills, "Id", "Title", toDoItem.SelectedSkills);
            
            if (ModelState.IsValid)
            {
                var edit = new ToDoItem(); 
                edit = toDoItem;
                toDoItem = db.ToDoItems.Include(i => i.ToDoSkills).SingleOrDefault(item => item.Id == toDoItem.Id); 
                var skills= db.Skills.ToList();

                if (toDoItem != null)
                {
                toDoItem.EditValues(edit, skills);
                db.Entry(toDoItem).State = EntityState.Modified;                    
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "Email", toDoItem.ApplicationUser_Id);
            return View(toDoItem);
        }

        // GET: ToDoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            db.ToDoItems.Remove(toDoItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
