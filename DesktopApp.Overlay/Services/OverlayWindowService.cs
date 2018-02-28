using DesktopApp.Overlay.Core;
using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Services
{

    public class OverlayWindowService : WindowServiceBase
    {

        private bool IsWindowCreated;

        private IDisposable ObservableWindowInitialized;
        private IDisposable ObservableWindowClosed;
        private IDisposable ObservableHiddenChanged;

        /// <summary>
        /// Windowを可視状態にする
        /// </summary>
        public override void Show()
        {
            if (!this.IsWindowCreated) {

                //Windowが存在しないため作成
                this.CreateWindow();

            } else {

                //Windowを可視状態に変更
                this.WindowHideControl(false);

            }
        }
        /// <summary>
        /// Windowを不可視状態にする
        /// </summary>
        public override void Hide()
        {

            if (!this.IsWindowCreated) {

                //Windowが存在しないのでそのまま
                return;

            }

            this.WindowHideControl(true);

        }

        /// <summary>
        /// Windowを作成する
        /// </summary>
        private void CreateWindow()
        {

            if (this.WindowFactory != null) {

                this.WindowFactory.Create();
                this.WindowState = eWindowStateTypes.Initializeing;
                this.IsWindowCreated = true;

            } else {

                throw new InvalidOperationException($"{nameof(this.WindowFactory)}が指定されていません");

            }

        }

        /// <summary>
        /// Windowの可視状態を変更する
        /// </summary>
        /// <param name="hide"><see langword="true"/>=不可視, <see langword="false"/>=可視</param>
        private void WindowHideControl(bool hide)
        {

            if (this.WindowController != null) {

                this.WindowController.IsHidden = hide;

            } else {

                throw new InvalidOperationException($"{nameof(this.WindowController)}が指定されていません");

            }

        }

        protected override void OnWindowControllerChanged(EventArgs<IWindowController> eventArgs)
        {
            base.OnWindowControllerChanged(eventArgs);

            if (eventArgs.OldValue != null) {

                this.ObservableWindowInitialized?.Dispose();
                this.ObservableWindowInitialized = null;

                this.ObservableWindowClosed?.Dispose();
                this.ObservableWindowClosed = null;

                this.ObservableHiddenChanged?.Dispose();
                this.ObservableHiddenChanged = null;

                this.IsWindowCreated = false;
                this.WindowState = eWindowStateTypes.Closed;

            }

            if (eventArgs.NewValue != null) {

                this.ObservableWindowInitialized = Observable.FromEvent
                (
                    handler => eventArgs.NewValue.WindowInitialized += handler,
                    handler => eventArgs.NewValue.WindowInitialized -= handler
                )
                .Subscribe(_ => { this.WindowState = eWindowStateTypes.Shown; });


                this.ObservableWindowClosed = Observable.FromEvent
                (
                    handler => eventArgs.NewValue.WindowClosed += handler,
                    handler => eventArgs.NewValue.WindowClosed -= handler
                )
                .Subscribe(_ =>
                {
                    this.WindowState = eWindowStateTypes.Closed;
                    this.WindowController = null;
                });


                this.ObservableHiddenChanged = Observable.FromEvent<bool>
                (
                    handler => eventArgs.NewValue.HiddenChanged += handler,
                    handler => eventArgs.NewValue.HiddenChanged -= handler
                )
                .Subscribe(x =>
                {

                    if (x) {

                        this.WindowState = eWindowStateTypes.Hide;

                    } else {

                        this.WindowState = eWindowStateTypes.Shown;

                    }

                });

            }

        }

        public override void Dispose()
        {
            base.Dispose();

            this.ObservableWindowInitialized?.Dispose();
            this.ObservableWindowInitialized = null;

            this.ObservableWindowClosed?.Dispose();
            this.ObservableWindowClosed = null;

            this.ObservableHiddenChanged?.Dispose();
            this.ObservableHiddenChanged = null;

        }

    }
}
