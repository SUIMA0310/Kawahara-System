using System.Collections.Generic;
using System.ComponentModel;

namespace DesktopApp.MultiUsers.Models
{
    public interface IUsersStore : INotifyPropertyChanged
    {
        string FilePath { get; set; }

        IEnumerable<T> Load<T>() where T : IUser;
        void Save( IEnumerable<IUser> data );
    }
}