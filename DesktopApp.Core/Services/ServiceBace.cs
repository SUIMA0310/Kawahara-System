using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Services
{
    /// <summary>
    /// Serviceの基底Class
    /// </summary>
    public abstract class ServiceBace : IDisposable
    {

        public ServiceBace()
        {
            this.Disposable = new CompositeDisposable();
        }

        /// <summary>
        /// IDisposableをまとめるコンテナ
        /// </summary>
        protected CompositeDisposable Disposable { get; }

        /// <summary>
        /// Serviceが不要になった際にまとめてDisposeする。
        /// </summary>
        public virtual void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}
