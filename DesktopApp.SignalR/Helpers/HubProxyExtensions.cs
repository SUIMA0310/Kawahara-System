using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;

namespace DesktopApp.Helpers
{
    public static class HubProxyExtensions
    {
        public static IObservable<Unit> On( this IHubProxy proxy, bool capturesSynchronizationContext = false, [CallerMemberName]string eventName = null )
        {
            var context = capturesSynchronizationContext
                        ? SynchronizationContext.Current
                        : null;
            return proxy.On( eventName.Substring( "On".Length ), context );
        }

        public static IObservable<Unit> On( this IHubProxy proxy, string eventName, SynchronizationContext context )
        {
            var sequence = Observable.Create<Unit>(observer =>
            {
                Action onData = () => observer.OnNext(Unit.Default);
                return proxy.On(eventName, onData);
            });
            return context == null ? sequence : sequence.ObserveOn( context );
        }

        public static IObservable<T> On<T>( this IHubProxy proxy, bool capturesSynchronizationContext = false, [CallerMemberName]string eventName = null )
        {
            var context = capturesSynchronizationContext
                        ? SynchronizationContext.Current
                        : null;
            return proxy.On<T>( eventName.Substring( "On".Length ), context );
        }

        public static IObservable<T> On<T>( this IHubProxy proxy, string eventName, SynchronizationContext context )
        {
            var sequence = Observable.Create<T>(observer =>
            {
                Action<T> onData = (x) => observer.OnNext(x);
                return proxy.On(eventName, onData);
            });
            return context == null ? sequence : sequence.ObserveOn( context );
        }

        public static IObservable<(T1, T2)> On<T1, T2>( this IHubProxy proxy, bool capturesSynchronizationContext = false, [CallerMemberName]string eventName = null )
        {
            var context = capturesSynchronizationContext
                        ? SynchronizationContext.Current
                        : null;
            return proxy.On<T1, T2>( eventName.Substring( "On".Length ), context );
        }

        public static IObservable<(T1, T2)> On<T1, T2>( this IHubProxy proxy, string eventName, SynchronizationContext context )
        {
            var sequence = Observable.Create<(T1, T2)>(observer =>
            {
                Action<T1, T2> onData = (x1, x2) => observer.OnNext((x1, x2));
                return proxy.On(eventName, onData);
            });
            return context == null ? sequence : sequence.ObserveOn( context );
        }

        public static Task Invoke( this IHubProxy self, [CallerMemberName] string method = null, params object[] args )
        {
            if ( string.IsNullOrWhiteSpace( method ) ) {
                throw new ArgumentException( "message", nameof( method ) );
            }

            return self.Invoke( method, args );
        }
    }
}