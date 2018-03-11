using System;
using System.Threading.Tasks;

using DesktopApp.Helpers;
using DesktopApp.Models;

using Prism.Logging;

namespace DesktopApp.Services
{
    /// <summary>
    /// ReactionHub のProxyクラス
    /// </summary>
    public class ReactionHubProxy : HubProxyService, IReactionHubProxy
    {
        public string PresentationID
        {
            get => this._PresentationID;
            set {
                if ( this._PresentationID == value ) { return; }
                string oldValue = this._PresentationID;
                this._PresentationID = value;
                this.OnPresentationIDChanged( value, oldValue );
            }
        }

        public ReactionHubProxy( ILoggerFacade logger, IConnectionService connection ) : base( logger, connection )
        {
        }

        protected override string HubName => Properties.Resources.HubName;

        private string _PresentationID;
        private bool _Listen;

        public event Action<string, string> PresentationIDChanged;

        protected virtual void OnPresentationIDChanged( string newValue, string oldValue )
        {
            if ( oldValue != null && this._Listen ) {
                this.RemoveListener( oldValue );
            }

            if ( newValue != null && this._Listen ) {
                this.AddListener();
            }

            this.PresentationIDChanged?.Invoke( newValue, oldValue );
        }

        /// <summary>
        /// Reactionの受信
        /// </summary>
        /// <returns>受信ストリーム</returns>
        public IObservable<(eReactionType, Color)> OnReceiveReaction()
            => this.Proxy.On<eReactionType, Color>();

        /// <summary>
        /// Listenerに対し、Reactionを送信する
        /// </summary>
        /// <param name="reactionType">送信するReactionの種類</param>
        /// <param name="coler">送信元を示す色情報</param>
        public Task SendReaction( eReactionType reactionType, Color color )
            => this.Proxy.Invoke( "SendReaction", this.PresentationID, reactionType, color );

        /// <summary>
        /// Listenerに対し、Reactionを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="reactionType">送信するReactionの種類</param>
        /// <param name="coler">送信元を示す色情報</param>
        public Task SendReaction( string presentationId, eReactionType reactionType, Color color )
            => this.Proxy.Invoke( "SendReaction", presentationId, reactionType, color );

        /// <summary>
        /// Listenerに対し、Goodを送信する
        /// </summary>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendGood( Color color )
            => this.Proxy.Invoke( "SendGood", this.PresentationID, color );

        /// <summary>
        /// Listenerに対し、Goodを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendGood( string presentationId, Color color )
            => this.Proxy.Invoke( "SendGood", presentationId, color );

        /// <summary>
        /// Listenerに対し、Niceを送信する
        /// </summary>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendNice( Color color )
            => this.Proxy.Invoke( "SendNice", this.PresentationID, color );

        /// <summary>
        /// Listenerに対し、Niceを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendNice( string presentationId, Color color )
            => this.Proxy.Invoke( "SendNice", presentationId, color );

        /// <summary>
        /// Listenerに対し、Funを送信する
        /// </summary>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendFun( Color color )
            => this.Proxy.Invoke( "SendFun", this.PresentationID, color );

        /// <summary>
        /// Listenerに対し、Funを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendFun( string presentationId, Color color )
            => this.Proxy.Invoke( "SendFun", presentationId, color );

        /// <summary>
        /// 自身をListenerとして追加する
        /// </summary>
        /// <returns>追加に成功したか</returns>
        public Task<Result> AddListener()
        {
            var ret = this.Proxy.Invoke<Result>("AddListener", this.PresentationID);
            ret.ContinueWith( ( x ) => this._Listen = x.Result.ResultTypes == eResultTypes.Success );
            return ret;
        }

        /// <summary>
        /// 自身をListenerとして追加する
        /// </summary>
        /// <param name="presentationId">受信するPresentationのId</param>
        /// <returns>追加に成功したか</returns>
        public Task<Result> AddListener( string presentationId )
            => this.Proxy.Invoke<Result>( "AddListener", presentationId );

        /// <summary>
        /// 自身をLisnerから削除する
        /// </summary>
        /// <returns>削除に成功したか</returns>
        public Task<Result> RemoveListener()
        {
            this._Listen = false;
            return this.Proxy.Invoke<Result>( "RemoveListener", this.PresentationID );
        }

        /// <summary>
        /// 自身をLisnerから削除する
        /// </summary>
        /// <param name="presentationId">受信しているPresentationのId</param>
        /// <returns>削除に成功したか</returns>
        public Task<Result> RemoveListener( string presentationId )
            => this.Proxy.Invoke<Result>( "RemoveListener", presentationId );
    }
}