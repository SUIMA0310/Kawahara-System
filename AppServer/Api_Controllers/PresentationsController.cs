using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using AppServer.Models;

namespace AppServer.Api_Controllers
{
    [Authorize]
    public class PresentationsController : ApiController
    {
        private ApplicationDbContext Db = new ApplicationDbContext();

        // GET: api/Presentations
        public IQueryable<Presentation> GetPresentations()
        {
            return this.Db.Presentations;
        }

        // GET: api/Presentations/5
        [ResponseType( typeof( Presentation ) )]
        public async Task<IHttpActionResult> GetPresentation( string id )
        {
            Presentation presentation = await this.Db.Presentations.FindAsync(id);
            if ( presentation == null ) {
                return NotFound();
            }

            return Ok( presentation );
        }

        // PUT: api/Presentations/5
        [ResponseType( typeof( void ) )]
        public async Task<IHttpActionResult> PutPresentation( string id, Presentation presentation )
        {
            if ( !this.ModelState.IsValid ) {
                return BadRequest( this.ModelState );
            }

            if ( id != presentation.Id ) {
                return BadRequest();
            }

            this.Db.Entry( presentation ).State = EntityState.Modified;

            try {
                await this.Db.SaveChangesAsync();
            } catch ( DbUpdateConcurrencyException ) {
                if ( !PresentationExists( id ) ) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode( HttpStatusCode.NoContent );
        }

        // POST: api/Presentations
        [ResponseType( typeof( Presentation ) )]
        public async Task<IHttpActionResult> PostPresentation( Presentation presentation )
        {
            if ( !this.ModelState.IsValid ) {
                return BadRequest( this.ModelState );
            }

            this.Db.Presentations.Add( presentation );

            try {
                await this.Db.SaveChangesAsync();
            } catch ( DbUpdateException ) {
                if ( PresentationExists( presentation.Id ) ) {
                    return Conflict();
                } else {
                    throw;
                }
            }

            return CreatedAtRoute( "DefaultApi", new { id = presentation.Id }, presentation );
        }

        // DELETE: api/Presentations/5
        [ResponseType( typeof( Presentation ) )]
        public async Task<IHttpActionResult> DeletePresentation( string id )
        {
            Presentation presentation = await this.Db.Presentations.FindAsync(id);
            if ( presentation == null ) {
                return NotFound();
            }

            this.Db.Presentations.Remove( presentation );
            await this.Db.SaveChangesAsync();

            return Ok( presentation );
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing ) {
                this.Db.Dispose();
            }
            base.Dispose( disposing );
        }

        private bool PresentationExists( string id )
        {
            return this.Db.Presentations.Count( e => e.Id == id ) > 0;
        }
    }
}