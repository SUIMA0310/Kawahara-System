using System.ComponentModel;
using System.Runtime.CompilerServices;
using Reactive.Bindings;

namespace DesktopApp.MultiUsers.Models
{
    public class User : IUser, INotifyPropertyChanged
    {
        public User()
        {
        }

        /// <summary>
        /// プロパティ変更通知Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// ユーザ名
        /// </summary>
        public string Name
        {
            get => this._Name;
            set => this.SetProperty( ref this._Name, value );
        }

        /// <summary>
        /// Good回数
        /// </summary>
        public uint GoodCount
        {
            get => this._GoodCount;
            set => this.SetProperty( ref this._GoodCount, value );
        }

        /// <summary>
        /// Nice回数
        /// </summary>
        public uint NiceCount
        {
            get => this._NiceCount;
            set => this.SetProperty( ref this._NiceCount, value );
        }

        /// <summary>
        /// Fun回数
        /// </summary>
        public uint FunCount
        {
            get => this._FunCount;
            set => this.SetProperty( ref this._FunCount, value );
        }


        private string _Name     ;
        private uint   _GoodCount;
        private uint   _NiceCount;
        private uint   _FunCount ;


        private void SetProperty<T>( ref T backField, T value, [CallerMemberName]string propertyName = "" )
        {
            if ( (backField == null && value == null) || (backField != null && backField.Equals( value )) ) {
                return;
            }
            backField = value;
            this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

    }
}