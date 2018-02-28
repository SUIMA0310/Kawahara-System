using System;
using System.Reactive.Disposables;

namespace DesktopApp.Services
{

    /// <summary>
    /// Windowの状態管理を行う
    /// </summary>
    public abstract class WindowServiceBase : IDisposable
    {

        #region Protected

        /// <summary>
        /// Window生成を担当するObject
        /// </summary>
        protected IWindowFactory WindowFactory
        {
            get { return this._WindowFactory; }
            set {

                if (this._WindowFactory == value) { return; }

                if (this._WindowFactory != null && value != null) {

                    throw new InvalidOperationException($"既に{nameof(this.WindowFactory)}は、登録済みです。");

                }

                var newValue = value;
                var oldValue = this._WindowFactory;

                this._WindowFactory = value;

                this.OnWindowFactoryChanged(new EventArgs<IWindowFactory>(newValue, oldValue));

            }
        }

        /// <summary>
        /// Window操作を担当するObject
        /// </summary>
        protected IWindowController WindowController
        {
            get { return this._WindowController; }
            set {

                if (this._WindowController == value) { return; }

                if (this._WindowController != null && value != null) {

                    throw new InvalidOperationException($"既に{nameof(this.WindowController)}は、登録済みです。");

                }

                var newValue = value;
                var oldValue = this._WindowController;

                this._WindowController = value;

                this.OnWindowControllerChanged(new EventArgs<IWindowController>(newValue, oldValue));

            }
        }

        /// <summary>
        /// 廃棄が必要なObjectを収集する
        /// </summary>
        protected CompositeDisposable Disposable { get; }

        /// <summary>
        /// WindowFactoryの更新通知
        /// </summary>
        protected event Action<EventArgs<IWindowFactory>> WindowFactoryChanged;
        /// <summary>
        /// WindowControllerの更新通知
        /// </summary>
        protected event Action<EventArgs<IWindowController>> WindowControllerChanged;

        #endregion

        /// <summary>
        /// 現在のWindow状態
        /// </summary>
        public virtual eWindowStateTypes WindowState {
            get { return this._WindowState; }
            protected set {

                if ( this._WindowState == value ) {

                    return;

                }

                this._WindowState = value;

                this.OnWindowStateChanged( this._WindowState );

            }
        }

        /// <summary>
        /// WindowStateの更新通知
        /// </summary>
        public event Action<eWindowStateTypes> WindowStateChanged;

        public WindowServiceBase()
        {
            this.Disposable = new CompositeDisposable();
            this._WindowState = eWindowStateTypes.Closed;
        }

        /// <summary>
        /// Windowを表示状態にする
        /// </summary>
        public abstract void Show();

        /// <summary>
        /// Windowを非表示状態にする
        /// </summary>
        public abstract void Hide();

        public virtual void Dispose()
        {
            this.Close();
            this.Disposable.Dispose();
        }


        public virtual bool SetWindowFactory(IWindowFactory windowFactory, bool throwException = true)
        {
            try {

                this.WindowFactory = windowFactory;
                return true;

            } catch (InvalidOperationException) {

                if (throwException) {

                    throw;

                } else {

                    return false;

                }

            }
        }

        public virtual bool SetWindowController(IWindowController windowController, bool throwException = true)
        {
            try {

                this.WindowController = windowController;
                return true;

            } catch (InvalidOperationException) {

                if (throwException) {

                    throw;

                } else {

                    return false;

                }

            }
        }



        protected virtual void OnWindowFactoryChanged(EventArgs<IWindowFactory> eventArgs)
        {
            this.WindowFactoryChanged?.Invoke(eventArgs);
        }
        protected virtual void OnWindowControllerChanged(EventArgs<IWindowController> eventArgs)
        {
            this.WindowControllerChanged?.Invoke(eventArgs);
        }
        protected virtual void OnWindowStateChanged( eWindowStateTypes windowState )
        {
            this.WindowStateChanged?.Invoke( windowState );
        }

        /// <summary>
        /// Windowを閉じる
        /// </summary>
        protected virtual void Close()
        {
            this.WindowController?.Close();
            this.WindowState = eWindowStateTypes.Closed;
        }

        private IWindowFactory _WindowFactory;
        private IWindowController _WindowController;

        private eWindowStateTypes _WindowState;

        protected struct EventArgs<T>
        {
            public T NewValue { get; }
            public T OldValue { get; }

            public EventArgs(T newValue, T oldValue)
            {
                this.NewValue = newValue;
                this.OldValue = oldValue;
            }
        }

    }
}
