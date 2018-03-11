using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace DesktopApp.Services
{
    /// <summary>
    /// Windowの状態管理を行う
    /// </summary>
    public abstract class WindowServiceBase<TFactory, TController>
        : IDisposable, IWindowService
        where TFactory : IWindowFactory
        where TController : IWindowController
    {
        #region Protected

        /// <summary>
        /// Window生成を担当するObject
        /// </summary>
        protected TFactory WindowFactory
        {
            get { return this._WindowFactory; }
            set {
                if ( this._WindowFactory?.Equals( value ) ?? value == null ) { return; }

                if ( this._WindowFactory != null && value != null ) {
                    throw new InvalidOperationException( $"既に{nameof( this.WindowFactory )}は、登録済みです。" );
                }

                var newValue = value;
                var oldValue = this._WindowFactory;

                this._WindowFactory = value;

                this.OnWindowFactoryChanged( new EventArgs<IWindowFactory>( newValue, oldValue ) );
            }
        }

        /// <summary>
        /// Window操作を担当するObject
        /// </summary>
        protected TController WindowController
        {
            get { return this._WindowController; }
            set {
                if ( this._WindowController?.Equals( value ) ?? value == null ) { return; }

                if ( this._WindowController != null && value != null ) {
                    throw new InvalidOperationException( $"既に{nameof( this.WindowController )}は、登録済みです。" );
                }

                var newValue = value;
                var oldValue = this._WindowController;

                this._WindowController = value;

                this.OnWindowControllerChanged( new EventArgs<IWindowController>( newValue, oldValue ) );
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

        #endregion Protected

        /// <summary>
        /// 現在のWindow状態
        /// </summary>
        public virtual eWindowStateTypes WindowState
        {
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
        /// Windowを可視状態にする
        /// </summary>
        public virtual void Show()
        {
            if ( !this.IsWindowCreated ) {
                //Windowが存在しないため作成
                this.CreateWindow();
            } else {
                //Windowを可視状態に変更
                this.WindowHideControl( false );
            }
        }

        /// <summary>
        /// Windowを不可視状態にする
        /// </summary>
        public virtual void Hide()
        {
            if ( !this.IsWindowCreated ) {
                //Windowが存在しないのでそのまま
                return;
            }

            this.WindowHideControl( true );
        }

        public virtual void Dispose()
        {
            this.Close();
            this.Disposable.Dispose();

            this.ObservableWindowInitialized?.Dispose();
            this.ObservableWindowInitialized = null;

            this.ObservableWindowClosed?.Dispose();
            this.ObservableWindowClosed = null;

            this.ObservableHiddenChanged?.Dispose();
            this.ObservableHiddenChanged = null;
        }

        public virtual bool SetWindowFactory( IWindowFactory windowFactory, bool throwException = true )
        {
            if ( windowFactory is TFactory factory ) {
                try {
                    this.WindowFactory = factory;
                    return true;
                } catch ( InvalidOperationException ) {
                    if ( throwException ) {
                        throw;
                    }
                }
            } else if ( windowFactory == null ) {
                this.WindowFactory = default( TFactory );
                return true;
            } else {
                if ( throwException ) {
                    throw new InvalidCastException( nameof( TFactory ) );
                }
            }

            return false;
        }

        public virtual bool SetWindowController( IWindowController windowController, bool throwException = true )
        {
            if ( windowController is TController controller ) {
                try {
                    this.WindowController = controller;
                    return true;
                } catch ( InvalidOperationException ) {
                    if ( throwException ) {
                        throw;
                    }
                }
            } else if ( windowController == null ) {
                this.WindowController = default( TController );
                return true;
            } else {
                if ( throwException ) {
                    throw new InvalidCastException( nameof( TController ) );
                }
            }

            return false;
        }

        /// <summary>
        /// Windowを作成する
        /// </summary>
        private void CreateWindow()
        {
            if ( this.WindowFactory != null ) {
                this.IsWindowCreated = true;
                this.WindowState = eWindowStateTypes.Initializeing;
                this.WindowFactory.Create();
            } else {
                throw new InvalidOperationException( $"{nameof( this.WindowFactory )}が指定されていません" );
            }
        }

        /// <summary>
        /// Windowの可視状態を変更する
        /// </summary>
        /// <param name="hide"><see langword="true"/>=不可視, <see langword="false"/>=可視</param>
        private void WindowHideControl( bool hide )
        {
            if ( this.WindowController == null ) {
                throw new InvalidOperationException( $"{nameof( this.WindowController )}が指定されていません" );
            }

            if ( this.WindowState == eWindowStateTypes.Initializeing ) {
                throw new InvalidOperationException( $"Window初期化中は操作できません" );
            }

            this.WindowController.Opacity = hide ? 0.0 : 1.0;
            this.WindowState = hide ? eWindowStateTypes.Hide : eWindowStateTypes.Shown;
        }

        protected virtual void OnWindowFactoryChanged( EventArgs<IWindowFactory> eventArgs )
        {
            this.WindowFactoryChanged?.Invoke( eventArgs );
        }

        protected virtual void OnWindowControllerChanged( EventArgs<IWindowController> eventArgs )
        {
            this.WindowControllerChanged?.Invoke( eventArgs );

            if ( eventArgs.OldValue != null ) {
                this.ObservableWindowInitialized?.Dispose();
                this.ObservableWindowInitialized = null;

                this.ObservableWindowClosed?.Dispose();
                this.ObservableWindowClosed = null;

                this.ObservableHiddenChanged?.Dispose();
                this.ObservableHiddenChanged = null;

                this.IsWindowCreated = false;
                this.WindowState = eWindowStateTypes.Closed;
            }

            if ( eventArgs.NewValue != null ) {
                this.ObservableWindowInitialized = Observable.FromEventPattern
                (
                    handler => eventArgs.NewValue.Initialized += handler,
                    handler => eventArgs.NewValue.Initialized -= handler
                )
                .Subscribe( _ => { this.WindowState = eWindowStateTypes.Shown; } );

                this.ObservableWindowClosed = Observable.FromEventPattern
                (
                    handler => eventArgs.NewValue.Closed += handler,
                    handler => eventArgs.NewValue.Closed -= handler
                )
                .Subscribe( _ =>
                 {
                     this.WindowState = eWindowStateTypes.Closed;
                     this.WindowController = default( TController );
                 } );
            }
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

        private TFactory _WindowFactory;
        private TController _WindowController;

        private eWindowStateTypes _WindowState;

        private bool IsWindowCreated;

        private IDisposable ObservableWindowInitialized;
        private IDisposable ObservableWindowClosed;
        private IDisposable ObservableHiddenChanged;

        protected struct EventArgs<T>
        {
            public T NewValue { get; }
            public T OldValue { get; }

            public EventArgs( T newValue, T oldValue )
            {
                this.NewValue = newValue;
                this.OldValue = oldValue;
            }
        }
    }
}