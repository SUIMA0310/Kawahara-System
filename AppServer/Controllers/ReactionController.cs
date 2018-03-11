using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using AppServer.Models;
using AppServer.Models.HubModels;

namespace AppServer.Controllers
{
    public class ReactionController : Controller
    {
        #region fields

        private ApplicationDbContext _Db;

        #endregion fields

        #region Constructors

        public ReactionController()
        {
        }

        public ReactionController( ApplicationDbContext db )
        {
            this._Db = db;
        }

        #endregion Constructors

        #region Properties

        public ApplicationDbContext Db
        {
            get {
                return this._Db ?? (this._Db = new ApplicationDbContext());
            }
        }

        #endregion Properties

        // GET: Reaction/5
        public async Task<ActionResult> Index( string Id )
        {
            var presentation = await this.Db.Presentations.FindAsync(Id);
            if ( presentation == null ) {
                return HttpNotFound();
            }

            var rund = new Random();

            this.ViewBag.Presentation = presentation;
            this.ViewBag.Color = new Color(
                red: rund.Next( 256 ),
                green: rund.Next( 256 ),
                blue: rund.Next( 256 )
            );

            return View();
        }
    }
}