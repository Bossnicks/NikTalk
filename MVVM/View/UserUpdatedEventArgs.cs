using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MVVM.Trackers
{
    public class UserUpdatedEventArgs : EventArgs
    {
        public bool Success { get; }

        public UserUpdatedEventArgs(bool success)
        {
            Success = success;
        }
    }

}
