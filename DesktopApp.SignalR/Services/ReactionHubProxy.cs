using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using DesktopApp.Helpers;
using DesktopApp.Models;

namespace DesktopApp.Services
{
    /// <summary>
    /// ReactionHub のProxyクラス
    /// </summary>
    public class ReactionHubProxy : HubProxyService, IReactionHubProxy
    {
        public string PresentationID { get; set; }

        public ReactionHubProxy(ILoggerFacade logger, IConnectionService connection) : base(logger, connection) { }

        protected override string HubName => Properties.Resources.HubName;

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
        public Task SendReaction(eReactionType reactionType, Color color) 
            => this.Proxy.Invoke(this.PresentationID, reactionType, color);
        /// <summary>
        /// Listenerに対し、Reactionを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="reactionType">送信するReactionの種類</param>
        /// <param name="coler">送信元を示す色情報</param>
        public Task SendReaction(string presentationId, eReactionType reactionType, Color color)
            => this.Proxy.Invoke(presentationId, reactionType, color);

        /// <summary>
        /// Listenerに対し、Goodを送信する
        /// </summary>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendGood(Color color)
            => this.Proxy.Invoke(this.PresentationID, color);
        /// <summary>
        /// Listenerに対し、Goodを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendGood(string presentationId, Color color)
            => this.Proxy.Invoke(presentationId, color);

        /// <summary>
        /// Listenerに対し、Niceを送信する
        /// </summary>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendNice(Color color)
            => this.Proxy.Invoke(this.PresentationID, color);
        /// <summary>
        /// Listenerに対し、Niceを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendNice(string presentationId, Color color)
            => this.Proxy.Invoke(presentationId, color);

        /// <summary>
        /// Listenerに対し、Funを送信する
        /// </summary>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendFun(Color color)
            => this.Proxy.Invoke(this.PresentationID, color);
        /// <summary>
        /// Listenerに対し、Funを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="color">送信元を示す色情報</param>
        public Task SendFun(string presentationId, Color color)
            => this.Proxy.Invoke(presentationId, color);

        /// <summary>
        /// 自身をListenerとして追加する
        /// </summary>
        /// <returns>追加に成功したか</returns>
        public Task<Result> AddListener()
            => this.Proxy.Invoke<Result>(this.PresentationID);
        /// <summary>
        /// 自身をListenerとして追加する
        /// </summary>
        /// <param name="presentationId">受信するPresentationのId</param>
        /// <returns>追加に成功したか</returns>
        public Task<Result> AddListener(string presentationId)
            => this.Proxy.Invoke<Result>(presentationId);

        /// <summary>
        /// 自身をLisnerから削除する
        /// </summary>
        /// <returns>削除に成功したか</returns>
        public Task<Result> RemoveListener()
            => this.Proxy.Invoke<Result>(this.PresentationID);
        /// <summary>
        /// 自身をLisnerから削除する
        /// </summary>
        /// <param name="presentationId">受信しているPresentationのId</param>
        /// <returns>削除に成功したか</returns>
        public Task<Result> RemoveListener(string presentationId)
            => this.Proxy.Invoke<Result>(presentationId);

    }
}
