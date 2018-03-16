using System.Collections.ObjectModel;
using DesktopApp.MultiUsers.Models;
using Reactive.Bindings;

namespace DesktopApp.Services
{
    public interface IUsersActivity
    {
        ReactivePropertySlim<IUser> SelectedUser { get; }
        ObservableCollection<IUser> Users { get; }

        void AddUser( string userName );
        void CountReset();
        void RemoveUser( IUser user );
        void Save();
        void Load();
    }
}