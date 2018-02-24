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
using AppServer.Models.PresentationViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace AppServer.Controllers
{
    public class PresentationsController : Controller
    {

        #region fields

        private ApplicationDbContext _Db = new ApplicationDbContext();
        private ApplicationUserManager _UserManager;

        #endregion

        #region Constructors

        public PresentationsController()
        {
        }
        public PresentationsController(ApplicationUserManager userManager)
        {
            this.UserManager = userManager;
        }

        #endregion

        #region Properties

        public ApplicationUserManager UserManager {
            get {
                return this._UserManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                this._UserManager = value;
            }
        }

        #endregion

        // GET: Presentations
        public async Task<ActionResult> Index()
        {
            return View( await this._Db.Presentations.ToListAsync() );
        }

        // GET: Presentations/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Presentation presentation = await this._Db.Presentations.FindAsync( id );
            if (presentation == null) {
                return HttpNotFound();
            }
            return View( presentation );
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
        public async Task<ActionResult> Create(PresentationsViewModel viewModel)
        {
            if (this.ModelState.IsValid) {
                var presentation = new Presentation {
                    Id = Guid.NewGuid(),
                    Name = viewModel.Name,
                    HasReactionType = viewModel.HasReactionType,
                    ReactionCount = 0,
                    Owner = this._Db.Users.Find( this.User.Identity.GetUserId() )
                };
                this._Db.Presentations.Add( presentation );
                await this._Db.SaveChangesAsync();
                return RedirectToAction( "Index" );
            }

            return View( viewModel );
        }

        // GET: Presentations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Presentation presentation = await this._Db.Presentations.FindAsync( id );
            if (presentation == null) {
                return HttpNotFound();
            }
            return View( presentation );
        }

        // POST: Presentations/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PresentationsViewModel presentation)
        {
            if (this.ModelState.IsValid) {
                this._Db.Entry( presentation ).State = EntityState.Modified;
                await this._Db.SaveChangesAsync();
                return RedirectToAction( "Index" );
            }
            return View( presentation );
        }

        // GET: Presentations/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Presentation presentation = await this._Db.Presentations.FindAsync( id );
            if (presentation == null) {
                return HttpNotFound();
            }
            return View( presentation );
        }

        // POST: Presentations/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Presentation presentation = await this._Db.Presentations.FindAsync( id );
            this._Db.Presentations.Remove( presentation );
            await this._Db.SaveChangesAsync();
            return RedirectToAction( "Index" );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                this._Db.Dispose();
            }
            base.Dispose( disposing );
        }
    }
}
