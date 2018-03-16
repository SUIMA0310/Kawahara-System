using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using DesktopApp.Services;
using System.Reactive.Linq;
using System.Collections.ObjectModel;

namespace DesktopApp.MultiUsers.Models
{
    public class UsersActivity
    {
        private readonly IReactionHubProxy ReactionHubProxy;

        public UsersActivity( IReactionHubProxy reactionHubProxy )
        {
            this.ReactionHubProxy = reactionHubProxy;

            this.SelectedUser = new ReactivePropertySlim<IUser>();
            this.Users = new ReactiveCollection<IUser>();
        }

        public ReactivePropertySlim<IUser> SelectedUser { get; }
        public ObservableCollection<IUser> Users { get; }

        public Type UserType { get; set; }
        public Func<IUser> UserFactory { get; set; }

    }
}
