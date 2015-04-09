using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Authorize]
    public class SkillsController : Controller
    {
        private IContext db;

         public SkillsController() 
        {
             this.db = new ApplicationDbContext();
        }
        //Context for testing
         public SkillsController(IContext context) 
        {
            this.db = context;
        }
        // GET: Skills
        public ActionResult Index()
        {
            return View(db.Skills.ToList());
        }

        // GET: Skills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // GET: Skills/Create
        public ActionResult Create()
        {
            ViewData["Users"] = new MultiSelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,SelectedUsers")] Skill skill)
        {
            ViewData["Users"] = new MultiSelectList(db.Users, "Id", "Name", skill.SelectedUsers);
            if (ModelState.IsValid)
            {
                foreach (var userid in skill.SelectedUsers)
                {
                    skill.Users.Add(db.Users.Find(userid));
                }

                db.Skills.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skill);
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            ViewData["Users"] = new MultiSelectList(db.Users, "Id", "UserName");
            return View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,SelectedUsers")] Skill skill)
        {
            ViewData["Users"] = new MultiSelectList(db.Users, "Id", "Name", skill.SelectedUsers);
            if (ModelState.IsValid)
            {
                var edit = new Skill();
                edit = skill;
                skill = db.Skills.Include(i => i.Users).SingleOrDefault(item => item.Id == skill.Id);
                if (skill != null)
                {
                    skill.Title = edit.Title;
                    skill.Users.Clear();
                    foreach (var userid in edit.SelectedUsers)
                    {
                        skill.Users.Add(db.Users.Find(userid));
                    }
                    db.Entry(skill).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skill);
        }

        // GET: Skills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skills.Find(id);
            db.Skills.Remove(skill);
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
