using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.MVVM.Model;

namespace WpfApp1.MVVM.ViewModel
{
    internal class ContactViewModel : INotifyPropertyChanged
    {
        //private MessageModel _lastMessage;

        //public MessageModel LastMessage
        //{
        //    get { return _lastMessage; }
        //    set
        //    {
        //        _lastMessage = value;
        //        OnPropertyChanged(nameof(LastMessage));
        //        this.LastMessage = value;
        //    }
        //}

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
