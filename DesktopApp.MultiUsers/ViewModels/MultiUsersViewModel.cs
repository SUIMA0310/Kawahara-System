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
        private readonly IUsersStore UserStore;
        private readonly IThemeService ThemeService;
        private readonly IStatusService StatusService;

        public MultiUsersViewModel( IUsersActivity usersActivity, 
                                    IUsersStore usersStore, 
                                    IThemeService themeService, 
                                    IStatusService statusService )
        {
            this.UsersActivity = usersActivity;
            this.UserStore     = usersStore   ;
            this.ThemeService  = themeService ;
            this.StatusService = statusService;

            this.NewUserName = new ReactiveProperty<string>();
            this.SelectedUser = this.UsersActivity
                                    .SelectedUser
                                    .ToReactivePropertyAsSynchronized( x => x.Value )
                                    .AddTo( this.Disposable );
            this.Users = this.UsersActivity
                             .Users
                             .ToReadOnlyReactiveCollection()
                             .AddTo( this.Disposable );
            this.FilePath = this.UserStore
                                .ToReactivePropertyAsSynchronized( x => x.FilePath )
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
            this.ResetUserCount = this.SelectedUser
                                       .Select( x => x != null )
                                       .ToReactiveCommand()
                                       .AddTo( this.Disposable );
            this.SaveCommand = this.FilePath
                                   .Select( x => !string.IsNullOrWhiteSpace( x ) )
                                   .ToReactiveCommand()
                                   .AddTo( this.Disposable );
            this.LoadCommand = this.FilePath
                                   .Select( x => !string.IsNullOrWhiteSpace( x ) )
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
                    this.SelectedUser.Value = null;
                } );
            this.UnselectCommand
                .Subscribe( _ =>
                {
                    this.SelectedUser.Value = null;
                } );
            this.ResetUserCount
                .Subscribe( _ =>
                {
                    this.UsersActivity.CountReset();
                } );
            this.SaveCommand
                .Subscribe( _ =>
                {
                    this.UsersActivity.Save();
                } );
            this.LoadCommand
                .Subscribe( _ =>
                {
                    this.UsersActivity.Load();
                } );

            this.SelectedUser
                .Select( x => x != null )
                .Subscribe( x =>
                {
                    this.ThemeService.IsBusy = x;
                } )
                .AddTo( this.Disposable );
            this.SelectedUser
                .Select( x => x != null ? $"発表者:{x.Name}" : "準備完了" )
                .Subscribe( x =>
                {
                    this.StatusService.SetStatus( x );
                } )
                .AddTo( this.Disposable );

            this.Title.Value = "Multi Users";
        }

        public ReactiveProperty<string> NewUserName { get; }

        public ReactiveProperty<IUser> SelectedUser { get; }
        public ReadOnlyReactiveCollection<IUser> Users { get; }

        public ReactiveCommand AddUserCommand { get; }
        public ReactiveCommand RemoveUserCommand { get; }

        public ReactiveCommand UnselectCommand { get; }

        public ReactiveCommand ResetUserCount { get; }

        public ReactiveProperty<string> FilePath { get; }
        public ReactiveCommand SaveCommand { get; }
        public ReactiveCommand LoadCommand { get; }

    }
}
