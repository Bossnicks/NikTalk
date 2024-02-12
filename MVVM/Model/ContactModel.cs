using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Numerics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.MVVM.Model
{
    public class ContactModel : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId", TypeName = "int")]
        public int UserId { get; set; }
        [Column("UserName", TypeName = "text")]
        public string UserName { get; set; }
        [Column("Image", TypeName = "bytea")]
        public byte[]? Image { get; set; }
        [Column("Email", TypeName = "text")]
        public string Email { get; set; }
        [Column("Password", TypeName = "text")]
        public string Password { get; set; }
        [Column("RegistrationDate", TypeName = "timestamp")]
        public DateTime RegistrationDate { get; set; }
        private MessageModel _lastMessage;
        [NotMapped]
        public MessageModel LastMessage
        {
            get { return _lastMessage; }
            set
            {
                if (_lastMessage != value)
                {
                    _lastMessage = value;
                    OnPropertyChanged(nameof(LastMessage));
                }
            }
        }
        private string _lastMessageView;
        [NotMapped]
        public string LastMessageView
        {
            get { return _lastMessageView; }
            set
            {
                if (_lastMessageView != value)
                {
                    _lastMessageView = value;
                    OnPropertyChanged(nameof(LastMessageView));
                }
            }
        }
        //public ICollection<MessageModel> Messages { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }

}


















//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Runtime.CompilerServices;
//using System.Runtime.Remoting.Messaging;

//namespace WpfApp1.MVVM.Model
//{
//    internal class ContactModel : INotifyPropertyChanged
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int userId;
//        public int UserId
//        {
//            get { return userId; }
//            set
//            {
//                userId = value;
//                OnPropertyChanged(nameof(UserId));
//            }
//        }

//        public string userName;
//        public string UserName
//        {
//            get { return userName; }
//            set
//            {
//                userName = value;
//                OnPropertyChanged(nameof(UserName));
//            }
//        }

//        private byte[] image = new byte[0];
//        public byte[] Image
//        {
//            get { return image; }
//            set
//            {
//                if (image != value)
//                {
//                    image = value;
//                    OnPropertyChanged(nameof(Image));
//                }
//            }
//        }

//        private string email;
//        public string Email
//        {
//            get { return email; }
//            set
//            {
//                if (email != value)
//                {
//                    email = value;
//                    OnPropertyChanged(nameof(Email));
//                }
//            }
//        }

//        private string password;
//        public string Password
//        {
//            get { return password; }
//            set
//            {
//                if (password != value)
//                {
//                    password = value;
//                    OnPropertyChanged(nameof(Password));
//                }
//            }
//        }

//        private DateTime registrationDate;
//        public DateTime RegistrationDate
//        {
//            get { return registrationDate; }
//            set
//            {
//                if (registrationDate != value)
//                {
//                    registrationDate = value;
//                    OnPropertyChanged(nameof(RegistrationDate));
//                }
//            }
//        }


//        //public virtual ICollection<MessageModel> ChatMessages { get; set; }


//        public event PropertyChangedEventHandler PropertyChanged;
//        public void OnPropertyChanged([CallerMemberName] string prop = "")
//        {
//            if (PropertyChanged != null)
//                PropertyChanged(this, new PropertyChangedEventArgs(prop));
//        }
//    }
//}
