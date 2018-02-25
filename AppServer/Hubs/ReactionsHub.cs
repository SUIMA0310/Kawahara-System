using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using AppServer.Models;
using AppServer.Models.HubModels;

namespace AppServer.Hubs
{
    public class ReactionsHub : Hub
    {

        #region fields

        private ApplicationDbContext _Db;

        #endregion

        #region Constructors

        public ReactionsHub()
        {
        }
        public ReactionsHub(ApplicationDbContext db)
        {
            this._Db = db;
        }

        #endregion

        #region Properties

        public ApplicationDbContext Db {
            get {
                return this._Db ?? (this._Db = new ApplicationDbContext());
            }
        }

        #endregion

        /// <summary>
        /// Listenerに対し、Reactionを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="reactionType">送信するReactionの種類</param>
        /// <param name="coler">送信元を示す色情報</param>
        public void SendReaction(string presentationId, eReactionType reactionType, Color color)
        {
            this.Clients.Group( presentationId ).ReceiveReaction( reactionType, color );
        }


        /// <summary>
        /// Listenerに対し、Goodを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="color">送信元を示す色情報</param>
        public void SendGood(string presentationId, Color color)
        {
            this.SendReaction( presentationId, eReactionType.Good, color );
        }

        /// <summary>
        /// Listenerに対し、Niceを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="color">送信元を示す色情報</param>
        public void SendNice(string presentationId, Color color)
        {
            this.SendReaction( presentationId, eReactionType.Nice, color );
        }

        /// <summary>
        /// Listenerに対し、Funを送信する
        /// </summary>
        /// <param name="presentationId">送信先のPresentation</param>
        /// <param name="color">送信元を示す色情報</param>
        public void SendFun(string presentationId, Color color)
        {
            this.SendReaction( presentationId, eReactionType.Fun, color );
        }


        /// <summary>
        /// 自身をListenerとして追加する
        /// </summary>
        /// <param name="presentationId">受信するPresentationのId</param>
        /// <returns>追加に成功したか</returns>
        public async Task<Result> AddListener(string presentationId)
        {
            var presentation = await this.Db.Presentations.FindAsync( presentationId );
            if (presentation == null) {
                return new Result( eResultTypes.Failed, "Presentation not found." );
            }
            await this.Groups.Add( this.Context.ConnectionId, presentation.Id );
            return new Result( eResultTypes.Success );
        }

        /// <summary>
        /// 自身をLisnerから削除する
        /// </summary>
        /// <param name="presentationId">受信しているPresentationのId</param>
        /// <returns>削除に成功したか</returns>
        public async Task<Result> RemoveListener(string presentationId)
        {
            var presentation = await this.Db.Presentations.FindAsync( presentationId );
            if (presentation == null) {
                return new Result( eResultTypes.Failed, "Presentation not found." );
            }
            await this.Groups.Remove( this.Context.ConnectionId, presentation.Id );
            return new Result( eResultTypes.Success );
        }

    }
}