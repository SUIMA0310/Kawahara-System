using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using DesktopApp.MultiUsers.Models;

namespace DesktopApp.Services
{
    public class UsersActivity : ServiceBace
    {
        private readonly IReactionHubProxy ReactionHubProxy;

        public UsersActivity( IReactionHubProxy reactionHubProxy )
        {
            this.ReactionHubProxy = reactionHubProxy;
            this.ReactionHubProxy
                .OnReceiveReaction()
                .Subscribe( x =>
                {
                    var user = this.SelectedUser.Value;
                    if ( user == null ) {
                        return;
                    }
                    lock ( user ) {
                        switch ( x.Item1 ) {
                            case Models.eReactionType.Good:
                                user.GoodCount++;
                                break;
                            case Models.eReactionType.Nice:
                                user.NiceCount++;
                                break;
                            case Models.eReactionType.Fun:
                                user.FunCount++;
                                break;
                        }
                    }
                } )
                .AddTo( this.Disposable );

            this.SelectedUser = new ReactivePropertySlim<IUser>();
            this.Users = new ReactiveCollection<IUser>();
        }

        public ReactivePropertySlim<IUser> SelectedUser { get; }
        public ObservableCollection<IUser> Users { get; }

        public void CountReset()
        {
            var user = this.SelectedUser.Value;
            lock ( user ) {
                user.GoodCount = 0;
                user.NiceCount = 0;
                user.FunCount  = 0;
            }
        }

        public void AddUser( string userName )
        {
            var user = this.CreateUser();

            if ( user == null ) {
                throw new Exception( $"{nameof( CreateUser )} can not return null." );
            }

            user.Name = userName;
            this.Users.Add( user );
        }

        public void RemoveUser( IUser user )
        {
            if ( this.SelectedUser.Value == user ) {
                this.SelectedUser.Value = null;
            }

            this.Users.Remove( user );
        }

        protected virtual IUser CreateUser()
        {
            return new User();
        }

    }
}
