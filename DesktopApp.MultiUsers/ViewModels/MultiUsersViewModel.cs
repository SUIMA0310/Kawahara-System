using DesktopApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using DesktopApp.MultiUsers.Models;

namespace DesktopApp.ViewModels
{
    public class MultiUsersViewModel : ViewModelBase
    {
        private readonly IUsersActivity UsersActivity;

        public MultiUsersViewModel( IUsersActivity usersActivity )
        {
            this.UsersActivity = usersActivity;

            this.NewUserName = new ReactiveProperty<string>();
            this.SelectedUser = this.UsersActivity
                                    .SelectedUser
                                    .ToReactivePropertyAsSynchronized( x => x.Value )
                                    .AddTo( this.Disposable );
            this.Users = this.UsersActivity
                             .Users
                             .ToReadOnlyReactiveCollection()
                             .AddTo( this.Disposable );

            this.AddUserCommand = this.NewUserName
                                      .Select( x => !string.IsNullOrWhiteSpace( x ) )
                                      .ToReactiveCommand()
                                      .AddTo( this.Disposable );
            this.RemoveUserCommand = this.SelectedUser
                                         .Select( x => x != null )
                                         .ToReactiveCommand()
                                         .AddTo( this.Disposable );
            this.UnselectCommand = this.SelectedUser
                                       .Select( x => x != null )
                                       .ToReactiveCommand()
                                       .AddTo( this.Disposable );

            this.AddUserCommand
                .Subscribe( _ =>
                {
                    this.UsersActivity.AddUser( this.NewUserName.Value );
                    this.NewUserName.Value = string.Empty;
                } );
            this.RemoveUserCommand
                .Subscribe( _ =>
                {
                    this.UsersActivity.RemoveUser( this.SelectedUser.Value );
                } );

            this.Title.Value = "Multi Users";
        }

        public ReactiveProperty<string> NewUserName { get; }

        public ReactiveProperty<IUser> SelectedUser { get; }
        public ReadOnlyReactiveCollection<IUser> Users { get; }

        public ReactiveCommand AddUserCommand { get; }
        public ReactiveCommand RemoveUserCommand { get; }

        public ReactiveCommand UnselectCommand { get; }

    }
}
