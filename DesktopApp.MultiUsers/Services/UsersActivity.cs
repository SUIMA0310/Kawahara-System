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
    public class UsersActivity : ServiceBace, IUsersActivity
    {
        private readonly IReactionHubProxy ReactionHubProxy;
        private readonly IUsersStore UsersStore;

        public UsersActivity( IReactionHubProxy reactionHubProxy, IUsersStore usersStore )
        {
            this.ReactionHubProxy = reactionHubProxy;
            this.UsersStore = usersStore;

            this.ReactionHubProxy.Connected += () =>
            {
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

            };
                
            this.SelectedUser = new ReactivePropertySlim<IUser>();
            this.Users = new ReactiveCollection<IUser>();

            this.Load();
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

        public void Save()
        {
            this.UsersStore.Save( this.Users );
        }

        public void Load()
        {
            var users = this.UsersStore.Load<User>();
            if ( users != null ) {
                this.Users.Clear();
                this.Users.AddRange( users );
            }
        }

        protected virtual IUser CreateUser()
        {
            return new User();
        }

        public override void Dispose()
        {
            this.Save();
            base.Dispose();
        }

    }
}
