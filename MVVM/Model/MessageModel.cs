using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.MVVM.Model
{
    public class MessageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MessageId", TypeName = "int")]
        public int MessageId { get; set; }
        [Column("SenderId", TypeName = "int")]
        public int SenderId { get; set; }
        [Column("ReceiverId", TypeName = "int")]
        public int ReceiverId { get; set; }
        [Column("TypeOfMessage", TypeName = "text")]
        public string TypeOfMessage { get; set; }
        [Column("Message", TypeName = "bytea")]
        public byte[]? Message { get; set; }
        [Column("SentAt", TypeName = "timestamp")]
        public DateTime SentAt { get; set; }
        [Column("IsRead", TypeName = "bool")]
        public bool IsRead { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public byte[] Image { get; set; }
        //public int? UserId { get; set; }
        //public MessageModel Messages { get; set; }
    }
}



//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System.Runtime.CompilerServices;

//namespace WpfApp1.MVVM.Model
//{
//    internal class MessageModel : INotifyPropertyChanged
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        private int messageId;
//        public int MessageId
//        {
//            get { return messageId; }
//            set
//            {
//                if (messageId != value)
//                {
//                    messageId = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        private int senderId;
//        public int SenderId
//        {
//            get { return senderId; }
//            set
//            {
//                if (senderId != value)
//                {
//                    senderId = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        private int receiverId;
//        public int ReceiverId
//        {
//            get { return receiverId; }
//            set
//            {
//                if (receiverId != value)
//                {
//                    receiverId = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        private string typeOfMessage;
//        public string TypeOfMessage
//        {
//            get { return typeOfMessage; }
//            set
//            {
//                if (typeOfMessage != value)
//                {
//                    typeOfMessage = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        private byte[] messageContent = new byte[0];
//        public byte[] Message
//        {
//            get { return messageContent; }
//            set
//            {
//                if (messageContent != value)
//                {
//                    messageContent = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        private DateTime sentAt;
//        public DateTime SentAt
//        {
//            get { return sentAt; }
//            set
//            {
//                if (sentAt != value)
//                {
//                    sentAt = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        private bool isRead;
//        public bool IsRead
//        {
//            get { return isRead; }
//            set
//            {
//                if (isRead != value)
//                {
//                    isRead = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        //public virtual ContactModel Sender { get; set; }

//        //public virtual ContactModel Receiver { get; set; }

//        public event PropertyChangedEventHandler PropertyChanged;
//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
