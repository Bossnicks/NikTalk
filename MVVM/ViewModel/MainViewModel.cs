using WpfApp1.MVVM.Model;
using WpfApp1.Converters;
using System.Collections.ObjectModel;
using WpfApp1.Core;
using System.Windows.Input;
using System.IO;
using System.Windows;
using System.Data;
using System.Windows.Threading;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Collections;
using Microsoft.Win32;

namespace WpfApp1.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        //public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public ObservableCollection<MessageModel> LastMessages { get; set; }
        public ObservableCollection<MessageModel> Messages { get; set; }
        public MessageModel LastMessageView => Messages?.Any() == true ? Messages.Last() : null;



        /* Commands */
        public ICommand SendPictureCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ShowDialogMessagesCommand { get; set; }

        private static int authenticatedUserId = 1;
        private static ContactModel authenticatedUser = GetUserByEmailAndPasswordAsync("1@example.com", "your_password");
        public ContactModel AuthenticatedUser
        {
            get { return authenticatedUser; }
            set
            {
                if (authenticatedUser != value)
                {
                    authenticatedUser = value;
                    OnPropertyChanged(nameof(AuthenticatedUser));
                }
            }
        }

        private DispatcherTimer timer;
        private MessageModel _selectedMessage;

        public MessageModel SelectedMessage
        {
            get { return _selectedMessage; }
            set
            {
                _selectedMessage = value;
                OnPropertyChanged("SelectedMessage");
                //ShowDialogMessages();
            }
        }

        private bool _isEditing = false;

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    OnPropertyChanged(nameof(IsEditing));
                });
                //ShowDialogMessages();
            }
        }



        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                IsEditing = false;
                Message = String.Empty;
                OnPropertyChanged("SelectedContact");
                ShowDialogMessages();

            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeRestoreCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand UnchooseDialogCommand { get; set; }

        Window mainWindow = Application.Current.MainWindow;

        public MainViewModel()
        {
            //try
            //{
            //    //BeginData();
            //}
            //catch { }
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();
            LastMessages = new ObservableCollection<MessageModel>();

            //ShowDialogMessagesCommand = new RelayCommand(ShowDialogMessages);

            //SendCommand = new RelayCommand(Minimize, _ => !String.IsNullOrEmpty(Message));
            DeleteCommand = new RelayCommand(DeleteMessageAsync);
            SaveCommand = new RelayCommand(SaveMessageAsync);
            EditCommand = new RelayCommand(EditMessageAsync);
            SendPictureCommand = new RelayCommand(SendImageMessageAsync, _ => SelectedContact != null);
            SendCommand = new RelayCommand(SendTextMessageAsync, _ => !String.IsNullOrEmpty(Message) && !IsEditing);
            MinimizeCommand = new RelayCommand(Minimize);
            MaximizeRestoreCommand = new RelayCommand(MaximizeRestore);
            CloseCommand = new RelayCommand(Close);
            LoadUsersWithDialogs(authenticatedUserId);
            //timer = new System.Windows.Threading.DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1); // Интервал в 3 секунды (можете изменить по необходимости)
            //timer.Tick += async (sender, e) => await LoadLatestMessageAsync();
            //timer.Start();
            LoadLatestMessageAsync();
            //_ = LoadLatestMessageAsync();
        }


        public static ContactModel GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var context = new AppDbContextFactory();
            var dbContext = context.CreateDbContext(null);
            return dbContext.dbContacts
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        private void BeginData()
        {
            var context = new AppDbContextFactory();
            var dbContext = context.CreateDbContext(null);
            string imagePath = @"C:\Users\HP\Desktop\RES";
            string[] imageFiles = Directory.GetFiles(imagePath);
            foreach (string imageFile in imageFiles)
            {
                var user = new ContactModel
                {
                    UserName = System.IO.Path.GetFileNameWithoutExtension(imageFile),
                    Email = $"{System.IO.Path.GetFileNameWithoutExtension(imageFile)}@example.com",
                    Password = "your_password",  // Установите актуальный пароль
                    RegistrationDate = DateTime.Now,
                    Image = File.ReadAllBytes(imageFile)  // Считываем содержимое файла как массив байтов
                };
                dbContext.dbContacts.Add(user);
            }
            dbContext.SaveChanges();
            for (int i = 1; i <= imageFiles.Length; i++)
            {
                var message = new MessageModel
                {
                    SenderId = 1 + imageFiles.Length - i,
                    ReceiverId = i,
                    TypeOfMessage = "картинка",  // Установите актуальный пароль
                    SentAt = DateTime.Now,
                    Message = File.ReadAllBytes(imageFiles[i - 1]),  // Считываем содержимое файла как массив байтов
                    IsRead = false,
                };
                dbContext.dbMessages.Add(message);
            }
            dbContext.SaveChanges();
            for (int i = 0; i < imageFiles.Length; i++)
            {
                var sticker = new StickerModel
                {
                    Sticker = File.ReadAllBytes(imageFiles[i])  // Считываем содержимое файла как массив байтов
                };
                dbContext.dbStickers.Add(sticker);
            }
            dbContext.SaveChanges();
        }

        public async Task LoadLatestMessageAsync()
        {
            MessageModel message;
            foreach (var contact in Contacts)
            {
                if (contact != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {

                        message = GetLatestMessage(contact.UserId, authenticatedUserId);
                        if (message.TypeOfMessage == "Image")
                        {
                            contact.LastMessageView = "Image";
                        }
                        else if (message.TypeOfMessage == "Text")
                        {
                            contact.LastMessageView = ConvertersIn.ConvertByteArrayToString(message.Message);
                        }
                    });
                }
            }

            if (SelectedContact != null && SelectedContact.LastMessage.MessageId != LastMessageView.MessageId)
            {
                await ShowDialogMessages();
            }
        }

        private MessageModel GetLatestMessage(int contactId, int authenticatedUserId)
        {
            var context = new AppDbContextFactory();
            var dbContext = context.CreateDbContext(null);
            var result = dbContext.dbMessages.Where(msg => (authenticatedUserId == msg.SenderId && contactId == msg.ReceiverId) ||
            (contactId == msg.SenderId && authenticatedUserId == msg.ReceiverId))
            .OrderByDescending(msg => msg.SentAt)
            .FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        private void LoadUsersWithDialogs(int authenticatedUserId)
        {
            var factory = new AppDbContextFactory();
            var context = factory.CreateDbContext(null);
            var contacts = context.dbContacts
                .Where(u => u.UserId != authenticatedUserId &&
                            (context.dbMessages.Any(m => m.SenderId == authenticatedUserId && (m.ReceiverId == u.UserId || m.SenderId == u.UserId)) ||
                             context.dbMessages.Any(m => m.ReceiverId == authenticatedUserId && (m.SenderId == u.UserId || m.ReceiverId == u.UserId))))
                .Select(u => new ContactModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Image = u.Image,
                    Email = u.Email,
                })
                .ToList();
            Contacts.Clear();
            foreach (var contact in contacts)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Contacts.Add(contact);
                });
            }
        }




        public async Task ShowDialogMessages()
        {
            if (SelectedContact != null)
            {
                Messages.Clear();

                var factory = new AppDbContextFactory();
                var context = factory.CreateDbContext(null);

                // Получаем имя текущего пользователя

                // Получаем сообщения с дополнительной информацией об отправителе
                var messages = await context.dbMessages
                    .Where(m => ((m.SenderId == authenticatedUserId && m.ReceiverId == SelectedContact.UserId) ||
                                 (m.SenderId == SelectedContact.UserId && m.ReceiverId == authenticatedUserId)))
                    .OrderBy(m => m.SentAt)
                    .Select(m => new
                    {
                        Message = m,
                        Image = m.SenderId == authenticatedUserId ? authenticatedUser.Image :
                        SelectedContact.Image,
                        UserName = m.SenderId == authenticatedUserId ? authenticatedUser.UserName :
                        SelectedContact.UserName
                    })
                    .ToListAsync();

                foreach (var item in messages)
                {
                    var messageModel = new MessageModel
                    {
                        MessageId = item.Message.MessageId,
                        SenderId = item.Message.SenderId,
                        ReceiverId = item.Message.ReceiverId,
                        TypeOfMessage = item.Message.TypeOfMessage,
                        Message = item.Message.Message,
                        SentAt = item.Message.SentAt,
                        IsRead = item.Message.IsRead,
                        UserName = item.UserName ?? "Unknown",
                        Image = item.Image ?? new byte[0]
                    };

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Messages.Add(messageModel);
                    });
                }
            }
        }



        public void Minimize(object sender)
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        public void MaximizeRestore(object sender)
        {
            if (mainWindow.WindowState == WindowState.Normal)
            {
                mainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                mainWindow.WindowState = WindowState.Normal;
            }
        }

        public void Close(object sender)
        {
            Application.Current.Shutdown();
        }


        public async void SendTextMessageAsync(object sender)
        {
            var message = new MessageModel()
            {
                SenderId = authenticatedUser.UserId,
                ReceiverId = SelectedContact.UserId,
                TypeOfMessage = "Text",
                SentAt = DateTime.Now,
                IsRead = false,
                Message = Encoding.UTF8.GetBytes(Message),
                UserName = authenticatedUser.UserName ?? "Unknown",
                Image = AuthenticatedUser.Image ?? new byte[0]
            };

            // Преобразование текста в массив байтов и сохранение в свойство Message

            Messages.Add(message);
            Message = "";

            var factory = new AppDbContextFactory();
            var context = factory.CreateDbContext(null);
            context.dbMessages.Add(message);
            await context.SaveChangesAsync();
        }

        public async void SendImageMessageAsync(object sender)
        {
            if (SelectedContact != null)
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jfif;*.jpeg;*.jpg)|*.png;*.jfif;*.jpeg;*.jpg";

                if (openFileDialog.ShowDialog() == true)
                {
                    var imagePath = openFileDialog.FileName;
                    var imageBytes = GetImageBytes(imagePath);

                    var message = new MessageModel()
                    {
                        SenderId = authenticatedUser.UserId,
                        ReceiverId = SelectedContact.UserId,
                        TypeOfMessage = "Image",
                        SentAt = DateTime.Now,
                        IsRead = false,
                        Message = imageBytes,
                        UserName = authenticatedUser.UserName ?? "Unknown",
                        Image = authenticatedUser.Image ?? new byte[0]
                    };

                    Messages.Add(message);

                    AppDbContextFactory factory = new AppDbContextFactory();
                    AppDbContext context = factory.CreateDbContext(null);
                    context.dbMessages.Add(message);
                    await context.SaveChangesAsync();

                }
            }
        }

        private byte[] GetImageBytes(string imagePath)
        {
            try
            {
                return System.IO.File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading image: {ex.Message}");
                return null;
            }
        }


        public async void DeleteMessageAsync(object sender)
        {
            if (sender is MessageModel)
            {
                AppDbContextFactory factory = new AppDbContextFactory();
                AppDbContext context = factory.CreateDbContext(null);
                MessageModel? elem = sender as MessageModel;
                MessageModel? messageFromDb = await context.dbMessages.FindAsync(elem?.MessageId);
                if (messageFromDb != null)
                {
                    // Удаляем объект из базы данных
                    context.dbMessages.Remove(messageFromDb);
                    await context.SaveChangesAsync();
                }

            }
            ShowDialogMessages();
        }
        private MessageModel _editingMessage;

        public async void EditMessageAsync(object sender)
        {
            if (sender is MessageModel)
            {
                IsEditing = true;

                _editingMessage = sender as MessageModel;
                Message = Encoding.UTF8.GetString(_editingMessage.Message);
            }
        }

        public async void SaveMessageAsync(object sender)
        {
            if (_editingMessage != null)
            {
                AppDbContextFactory factory = new AppDbContextFactory();
                AppDbContext context = factory.CreateDbContext(null);

                MessageModel? messageFromDb = await context.dbMessages.FindAsync(_editingMessage.MessageId);
                if (messageFromDb != null)
                {
                    // Обновляем текст сообщения
                    messageFromDb.Message = Encoding.UTF8.GetBytes(Message);
                    if (Message == string.Empty)
                    {
                        MessageBox.Show("Пустое сообщение не может быть отправлено!");
                        IsEditing = false;
                        return;
                    }

                    await context.SaveChangesAsync();
                }
            }
            await ShowDialogMessages();
            Message = String.Empty;
        }

        //public async void UpdateTextMessageAsync(object sender)
        //{
        //    if (SelectedMessage != null)
        //    {
        //        Message = SelectedMessage.Message;
        //        // Обновление текста сообщения
        //        SelectedMessage.Message = newMessageText;

        //        using (var context = new AppDbContext())
        //        {
        //            // Помечаем объект как измененный
        //            context.Entry(selectedMessage).State = EntityState.Modified;

        //            await context.SaveChangesAsync();
        //        }
        //    }
        //}


    }



}
