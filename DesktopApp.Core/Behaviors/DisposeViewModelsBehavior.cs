using System;
using System.Collections.Specialized;

using Prism.Common;
using Prism.Regions;

namespace DesktopApp.Behaviors
{
    /// <summary>
    /// Viewが削除される際に、ViewModel含めDisposeする
    /// </summary>
    public class DisposeViewModelsBehavior : RegionBehavior
    {
        protected override void OnAttach()
        {
            this.Region.Views.CollectionChanged += this.ViewsCollectionChanged;
        }

        protected virtual void ViewsCollectionChanged( object sender, NotifyCollectionChangedEventArgs eventArgs )
        {
            //Eventの種類を確認
            switch ( eventArgs.Action ) {
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace: {
                    foreach ( object oldView in eventArgs.OldItems ) {
                        //ViewからViewModelを取り出して、Disposeする
                        MvvmHelpers.ViewAndViewModelAction<IDisposable>( oldView, d => d.Dispose() );
                    }

                    break;
                }
            }
        }
    }
}