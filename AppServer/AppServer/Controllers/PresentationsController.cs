using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppServer.Models;
//using AppServer.Models;
using Microsoft.AspNet.Identity.Owin;

namespace AppServer.Controllers
{
    public class PresentationsController : Controller
    {

        #region fields

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        #endregion

        #region Constructors

        public PresentationsController()
        {
        }
        public PresentationsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        #endregion

        #region Properties

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        #endregion

        // GET: Presentations
        public async Task<ActionResult> Index()
        {
            return View(await db.Presentations.ToListAsync());
        }

        // GET: Presentations/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentation presentation = await db.Presentations.FindAsync(id);
            if (presentation == null)
            {
                return HttpNotFound();
            }
            return View(presentation);
        }

        // GET: Presentations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Presentations/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,HasReaction")] Presentation presentation)
        {
            if (ModelState.IsValid)
            {
                presentation.Id = Guid.NewGuid();
                db.Presentations.Add(presentation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(presentation);
        }

        // GET: Presentations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentation presentation = await db.Presentations.FindAsync(id);
            if (presentation == null)
            {
                return HttpNotFound();
            }
            return View(presentation);
        }

        // POST: Presentations/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,HasReaction")] Presentation presentation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presentation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(presentation);
        }

        // GET: Presentations/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentation presentation = await db.Presentations.FindAsync(id);
            if (presentation == null)
            {
                return HttpNotFound();
            }
            return View(presentation);
        }

        // POST: Presentations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Presentation presentation = await db.Presentations.FindAsync(id);
            db.Presentations.Remove(presentation);
            await db.SaveChangesAsync();
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
