using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesktopApp.Helpers
{
    public static class HubProxyExtensions
    {

        public static IObservable<Unit> On(this IHubProxy proxy, bool capturesSynchronizationContext = false, [CallerMemberName]string eventName = null)
        {
            var context = capturesSynchronizationContext
                        ? SynchronizationContext.Current
                        : null;
            return proxy.On(eventName.Substring("On".Length), context);
        }

        public static IObservable<Unit> On(this IHubProxy proxy, string eventName, SynchronizationContext context)
        {
            var sequence = Observable.Create<Unit>(observer => {
                Action onData = () => observer.OnNext(Unit.Default);
                return proxy.On(eventName, onData);
            });
            return context == null ? sequence : sequence.ObserveOn(context);
        }

        public static Task Invoke(this IHubProxy self, [CallerMemberName] string method = null, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("message", nameof(method));
            }

            return self.Invoke(method, args);
        }

    }
}
