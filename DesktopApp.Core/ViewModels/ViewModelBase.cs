using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings.Extensions;

namespace DesktopApp.ViewModels
{
    /// <summary>
    /// ベースとなるViewModel
    /// </summary>
    public class ViewModelBase : IDisposable
    {

        public ViewModelBase()
        {

            //IDisposableをまとめる
            this.Disposable = new CompositeDisposable();

            //TitleのReactivePropertyを用意
            this.Title = new ReactiveProperty<string>( this.GetType().ToString() ).AddTo(this.Disposable);

        }

        /// <summary>
        /// View のタイトル
        /// </summary>
        public virtual ReactiveProperty<string> Title { get; protected set; }

        /// <summary>
        /// IDisposableをまとめるコンテナ
        /// </summary>
        protected CompositeDisposable Disposable { get; }

        /// <summary>
        /// Viewが不要になった際に、ViewModelの破棄処理を行う
        /// </summary>
        public virtual void Dispose()
        {
            //まとめてDisposeする
            this.Disposable.Dispose();
        }
    }
}
