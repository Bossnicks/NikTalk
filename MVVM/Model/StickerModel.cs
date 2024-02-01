using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.MVVM.Model
{
    public class StickerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("StickerId", TypeName = "int")]
        public int StickerId { get; set; }
        [Column("Sticker", TypeName = "bytea")]
        public byte[] Sticker { get; set; } = new byte[0];
    }
}


//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System.Runtime.CompilerServices;

//namespace WpfApp1.MVVM.Model
//{
//    internal class StickerModel : INotifyPropertyChanged
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        private int stickerId;
//        public int StickerId
//        {
//            get { return stickerId; }
//            set
//            {
//                if (stickerId != value)
//                {
//                    stickerId = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        private byte[] stickerContent = new byte[0];
//        public byte[] Sticker
//        {
//            get { return stickerContent; }
//            set
//            {
//                if (stickerContent != value)
//                {
//                    stickerContent = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
