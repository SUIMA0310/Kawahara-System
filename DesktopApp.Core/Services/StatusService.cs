using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Disposables;

namespace DesktopApp.Services
{

    public class StatusService : IDisposable, IStatusService
    {

        private ILoggerFacade Logger { get; }
        protected CompositeDisposable Disposable { get; }
        private Subject<string> Subject { get; }
        private string StatusMessage { get; set; }

        public ReactiveProperty<string> Status { get; }

        public StatusService(ILoggerFacade logger)
        {
            this.Logger = logger;


            this.Subject = new Subject<string>();
            this.Disposable = new CompositeDisposable();
            this.Status = new ReactiveProperty<string>( eStatusMessages.Ready.DisplayName() );


            this.Subject.Do( (msg) => {

                this.Status.Value = msg;

            } )
            .Delay( TimeSpan.FromSeconds( 5 ) )
            .Subscribe( msg => {

                if (this.Status.Value == msg)
                {
                    this.Status.Value = this.StatusMessage;
                }

            } )
            .AddTo( this.Disposable );

            this.Status.AddTo( this.Disposable );

        }

        public void SetStatus(eStatusMessages statusMessages)
        {
            this.SetStatus( statusMessages.DisplayName() );
        }
        public void SetStatus(string msg)
        {
            this.Logger.Log( $"Status : {msg}", Category.Info, Priority.Low );
            this.StatusMessage = msg;
            this.Status.Value = this.StatusMessage;
        }

        public void SetInfomation( string msg )
        {
            this.Subject.OnNext( msg );
        }

        public void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}
