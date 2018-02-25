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
    [Authorize]
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
            var user = await this.UserManager.FindByIdAsync( this.User.Identity.GetUserId() );
            return View( user.Presentations );
        }

        // GET: Presentations/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Presentation presentation = await this._Db.Presentations.FindAsync( id );
            if (presentation == null) {
                return HttpNotFound();
            }
            if (presentation.Owner.Id != this.User.Identity.GetUserId()) {
                return HttpNotFound();
            }
            return View( presentation );
        }

        // GET: Presentations/Create
        public ActionResult Create()
        {
            return View( new CreateViewModel() );
        }

        // POST: Presentations/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            if (this.ModelState.IsValid) {
                var presentation = new Presentation {
                    Name = viewModel.Name,
                    HasReactionType = viewModel.HasReactionType,
                    ReactionCount = 0,
                    Owner = this._Db.Users.Find( this.User.Identity.GetUserId() )
                };

                try {
                    this._Db.Presentations.Add( presentation );
                    await this._Db.SaveChangesAsync();
                } catch (Exception ex) {
                    string str = ex.Message;
                }

                return RedirectToAction( "Index" );
            }

            return View( viewModel );
        }

        // GET: Presentations/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Presentation presentation = await this._Db.Presentations.FindAsync( id );
            if (presentation == null) {
                return HttpNotFound();
            }
            if (presentation.Owner.Id != this.User.Identity.GetUserId()) {
                return HttpNotFound();
            }
            var edit = new EditViewModel {
                Id = presentation.Id,
                Name = presentation.Name,
                HasReactionType = presentation.HasReactionType
            };
            return View( edit );
        }

        // POST: Presentations/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel edit)
        {
            if (this.ModelState.IsValid) {
                var presentation = await this._Db.Presentations.FindAsync( edit.Id );

                if (presentation == null) {
                    return HttpNotFound();
                }
                if (presentation.Owner.Id != this.User.Identity.GetUserId()) {
                    return HttpNotFound();
                }

                presentation.Name = edit.Name;
                presentation.HasReactionType = edit.HasReactionType;
                this._Db.Entry( presentation ).State = EntityState.Modified;
                await this._Db.SaveChangesAsync();
                return RedirectToAction( "Index" );
            }
            return View( edit );
        }

        // GET: Presentations/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Presentation presentation = await this._Db.Presentations.FindAsync( id );
            if (presentation == null) {
                return HttpNotFound();
            }
            if (presentation.Owner.Id != this.User.Identity.GetUserId()) {
                return HttpNotFound();
            }
            return View( presentation );
        }

        // POST: Presentations/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Presentation presentation = await this._Db.Presentations.FindAsync( id );
            if (presentation == null) {
                return HttpNotFound();
            }
            if (presentation.Owner.Id != this.User.Identity.GetUserId()) {
                return HttpNotFound();
            }
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
