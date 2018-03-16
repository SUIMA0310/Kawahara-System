using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace DesktopApp.MultiUsers.Models
{
    public class UsersStore : IUsersStore
    {

        #region INotifyPropertyChanged 実装

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged( [CallerMemberName]string PropertyName = "" )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( PropertyName ) );
        }

        #endregion

        public string FilePath
        {
            get => Properties.Settings.Default.FilePath;
            set {
                if ( this.FilePath != value ) {
                    Properties.Settings.Default.FilePath = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IEnumerable<T> Load<T>() where T : IUser
        {
            if ( !File.Exists( this.FilePath ) ) {
                return null;
            }

            using ( var fs = new StreamReader( this.FilePath ) ) {
                var csv = new CsvReader( fs );
                return csv.GetRecords<T>().ToArray();
            }
        }

        public void Save( IEnumerable<IUser> data )
        {
            string dic = Path.GetDirectoryName( this.FilePath );
            if ( !Directory.Exists( dic ) ) {
                Directory.CreateDirectory( dic );
            }
            using ( var fs = new StreamWriter( this.FilePath, false ) ) {
                var csv = new CsvWriter( fs );
                csv.WriteRecords( data );
            }
        }

    }
}
