﻿using WpfApp1.MVVM.Model;
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
using Microsoft.VisualBasic;
using NAudio;
using NAudio.Wave;
using System.Windows.Forms;
using WpfApp1.MVVM.View;
using WpfApp1.MVVM.Trackers;
using System.Data.Common;
//using System.Windows.Forms;

namespace WpfApp1.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public ObservableCollection<StickerModel> Stickers { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public ObservableCollection<MessageModel> LastMessages { get; set; }
        public ObservableCollection<MessageModel> Messages { get; set; }
        public MessageModel LastMessageView => Messages?.Any() == true ? Messages.Last() : null;
        public ObservableCollection<ContactModel> UnknownContacts { get; set; }

        public ICommand SendPictureCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ShowDialogMessagesCommand { get; set; }
        public ICommand PlayOrDownloadAudioCommand { get; set; }
        public ICommand OpenProfileEditCommand { get; private set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeRestoreCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand UnchooseDialogCommand { get; set; }
        public ICommand SendVoiceMessageCommand { get; set; }
        public ICommand SendStickerCommand { get; set; }
        public ICommand StartEditProfileCommand { get; set; }
        public ICommand SearchUsersAndDialogsCommand { get; set; }

        public static ContactModel authenticatedUser;
        public ContactModel AuthenticatedUser
        {
            get { return authenticatedUser; }
            set
            {
                if (authenticatedUser != value)
                {
                    authenticatedUser = value;
                }
                OnPropertyChanged(nameof(AuthenticatedUser));
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
            }
        }

        private StickerModel _selectedSticker;
        public StickerModel SelectedSticker
        {
            get { return _selectedSticker; }
            set
            {
                _selectedSticker = value;
                OnPropertyChanged("SelectedSticker");
            }
        }

        private bool _isEditing = false;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    OnPropertyChanged(nameof(IsEditing));
                });
            }
        }

        public MainViewModel()
        {

        }


        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                IsEditing = false;
                Message = String.Empty;
                OnPropertyChanged("SelectedContact");
                ShowDialogMessages();
            }
        }

        private string _textForSearch;
        public string TextForSearch
        {
            get { return _textForSearch; }
            set
            {
                _textForSearch = value;
                if (value == "")
                {
                    LoadUsersWithDialogs(AuthenticatedUser.UserId);
                    UnknownContacts.Clear();
                    OnPropertyChanged("UnknownContacts");
                }
                OnPropertyChanged(nameof(TextForSearch));
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

        private MessageModel lastMessage;
        public MessageModel LastMessage
        {
            get { return lastMessage; }
            set
            {
                if (lastMessage != value)
                {
                    lastMessage = value;
                    OnPropertyChanged(nameof(LastMessage));
                }
            }
        }

        Window mainWindow = System.Windows.Application.Current.MainWindow;
        public MainViewModel(ContactModel AuthUser)
        {
            try
            {
                AuthenticatedUser = AuthUser;
                UnknownContacts = new ObservableCollection<ContactModel>();
                Stickers = new ObservableCollection<StickerModel>();
                Messages = new ObservableCollection<MessageModel>();
                Contacts = new ObservableCollection<ContactModel>();
                LastMessages = new ObservableCollection<MessageModel>();
                StartEditProfileCommand = new RelayCommand(StartEditProfile);
                SendStickerCommand = new RelayCommand(SendStickerAsync, _ => SelectedContact != null);
                PlayOrDownloadAudioCommand = new RelayCommand(PlayOrDownloadAudioAsync);
                SendVoiceMessageCommand = new RelayCommand(SendVoiceMessageAsync, _ => SelectedContact != null);
                DeleteCommand = new RelayCommand(DeleteMessageAsync);
                SaveCommand = new RelayCommand(SaveMessageAsync);
                EditCommand = new RelayCommand(EditMessageAsync);
                SearchUsersAndDialogsCommand = new RelayCommand(SearchUsersAndDialogs);
                SendPictureCommand = new RelayCommand(SendImageMessageAsync, _ => SelectedContact != null);
                SendCommand = new RelayCommand(SendTextMessageAsync, _ => !String.IsNullOrEmpty(Message) && !IsEditing);
                MinimizeCommand = new RelayCommand(Minimize);
                MaximizeRestoreCommand = new RelayCommand(MaximizeRestore);
                CloseCommand = new RelayCommand(Close);
                LoadStickersAsync();
                LoadUsersWithDialogs(AuthenticatedUser.UserId);
                timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += async (sender, e) => await LoadLatestMessageAsync();
                timer.Start();
                LoadLatestMessageAsync();
            }
            catch
            {

            }
        }

        public static string GetParentDirectory(int levelsUp)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string? parentDirectory = currentDirectory;
                for (int i = 0; i < levelsUp; i++)
                {
                    parentDirectory = Directory.GetParent(parentDirectory)?.FullName;
                    if (parentDirectory == null)
                    {
                        break;
                    }
                }
                return parentDirectory;
            }
            catch
            {
                return null;
            }
        }

        public ContactModel GetAuthenticatedUser(int userId)
        {
            try
            {
                var factory = new AppDbContextFactory();
                using (var context = factory.CreateDbContext(null))
                {
                    return context.dbContacts.FirstOrDefault(u => u.UserId == userId);
                }
            }
            catch
            {
                return null;
            }
        }

        public void SearchUsersAndDialogs(object sender)
        {
            try
            {
                Contacts = new ObservableCollection<ContactModel>(Contacts.Where(m => m.UserName.Contains(TextForSearch)));
                LoadUsersWithoutDialogs(AuthenticatedUser.UserId, TextForSearch);
            }
            catch
            {

            }
        }


        public void StartEditProfile(object sender)
        {
            try
            {
                System.Windows.Application.Current.MainWindow.IsEnabled = false;
                Profile profile = new Profile();
                LoginViewModel viewModel = new LoginViewModel(AuthenticatedUser);
                profile.DataContext = viewModel;

                viewModel.UserUpdated += (s, e) =>
                {
                    if (e.Success)
                    {
                        AuthenticatedUser = GetAuthenticatedUser(AuthenticatedUser.UserId);
                        OnPropertyChanged(nameof(AuthenticatedUser));
                    }
                    profile.Close();

                    System.Windows.Application.Current.MainWindow.IsEnabled = true;
                };

                profile.Show();
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Редактирование профиля не может быть завершено из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }



        public static async Task AddStickersFromDirectoryAsync()
        {
            try
            {
                var factory = new AppDbContextFactory();
                var context = factory.CreateDbContext(null);
                string directoryPath = MainViewModel.GetParentDirectory(3);
                var stickersDirectory = Path.Combine(directoryPath, "Stickers");

                if (!Directory.Exists(stickersDirectory))
                {
                    System.Windows.MessageBox.Show($"Директория '{stickersDirectory}' не существует.");
                    return;
                }

                foreach (var filePath in Directory.GetFiles(stickersDirectory))
                {
                    var sticker = new StickerModel
                    {
                        Sticker = File.ReadAllBytes(filePath)
                    };

                    context.dbStickers.Add(sticker);
                }

                await context.SaveChangesAsync();
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Стикеры не могут быть загружены из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }

        public static void BeginData()
        {
            try
            {
                int j = 1;
                var context = new AppDbContextFactory();
                var dbContext = context.CreateDbContext(null);
                string imagePath = @"C:\Users\HP\Desktop\RES";
                string[] imageFiles = Directory.GetFiles(imagePath);
                foreach (string imageFile in imageFiles)
                {
                    var user = new ContactModel
                    {
                        UserId = j,
                        UserName = System.IO.Path.GetFileNameWithoutExtension(imageFile),
                        Email = $"{System.IO.Path.GetFileNameWithoutExtension(imageFile)}@example.com",
                        Password = "your_password",  // Установите актуальный пароль
                        RegistrationDate = DateTime.Now,
                        Image = File.ReadAllBytes(imageFile)  // Считываем содержимое файла как массив байтов
                    };
                    dbContext.dbContacts.Add(user);
                    j++;
                }
                dbContext.SaveChanges();
                for (int i = 1; i <= imageFiles.Length; i++)
                {
                    var message = new MessageModel
                    {
                        SenderId = 1 + imageFiles.Length - i,
                        ReceiverId = i,
                        TypeOfMessage = "Image",  // Установите актуальный пароль
                        SentAt = DateTime.Now,
                        Message = File.ReadAllBytes(imageFiles[i - 1]),  // Считываем содержимое файла как массив байтов
                        IsRead = false,
                    };
                    dbContext.dbMessages.Add(message);
                }
                dbContext.SaveChanges();
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Начальные данные не могут быть подгружены из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }

        public async Task LoadLatestMessageAsync()
        {
            try
            {
                MessageModel message;
                foreach (var contact in Contacts)
                {
                    if (contact != null)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {

                            message = GetLatestMessage(contact.UserId, AuthenticatedUser.UserId);
                            if (message?.TypeOfMessage == "Image")
                            {
                                contact.LastMessage = message;
                                contact.LastMessageView = "Image";
                            }
                            else if (message?.TypeOfMessage == "Text")
                            {
                                contact.LastMessage = message;
                                contact.LastMessageView = ConvertersIn.ConvertByteArrayToString(message.Message);
                            }
                            else if (message?.TypeOfMessage == "Audio")
                            {
                                contact.LastMessage = message;
                                contact.LastMessageView = "Audio";
                            }
                            else if (message?.TypeOfMessage == "Sticker")
                            {
                                contact.LastMessage = message;
                                contact.LastMessageView = "Sticker";
                            }
                        });
                    }
                }
                Contacts = new ObservableCollection<ContactModel>(Contacts.OrderBy(m => m.LastMessage?.SentAt).Reverse());
                OnPropertyChanged(nameof(Contacts));
                if (SelectedContact != null)
                {
                    if (SelectedContact.LastMessage != null)
                    {
                        if (SelectedContact.LastMessage.MessageId != LastMessageView?.MessageId)
                        {
                            Messages.Clear();
                            await ShowDialogMessages();
                        }


                    }
                }
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщения не могут быть подгружены из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }

        private MessageModel GetLatestMessage(int contactId, int AuthenticatedUserId)
        {
            try
            {
                var context = new AppDbContextFactory();
                var dbContext = context.CreateDbContext(null);
                var result = dbContext.dbMessages.Where(msg => (AuthenticatedUserId == msg.SenderId && contactId == msg.ReceiverId) ||
                (contactId == msg.SenderId && AuthenticatedUserId == msg.ReceiverId))
                .OrderByDescending(msg => msg.SentAt)
                .FirstOrDefault();
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщения не могут быть подгружены из-за отсутствия связи с бд, проверьте соединение с сетью");
                return null;
            }
            catch
            {
                return null;
            }
        }

        private void LoadUsersWithDialogs(int AuthenticatedUserId)
        {
            try
            {
                Contacts.Clear();
                var factory = new AppDbContextFactory();
                var context = factory.CreateDbContext(null);
                var contacts = context.dbContacts
                    .Where(u => u.UserId != AuthenticatedUserId &&
                                (context.dbMessages.Any(m => m.SenderId == AuthenticatedUserId && (m.ReceiverId == u.UserId || m.SenderId == u.UserId)) ||
                                 context.dbMessages.Any(m => m.ReceiverId == AuthenticatedUserId && (m.SenderId == u.UserId || m.ReceiverId == u.UserId))))

                    .Select(u => new ContactModel
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        Image = u.Image,
                        Email = u.Email,
                    })
                    .ToList();
                foreach (var contact in contacts)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        Contacts.Add(contact);
                    });
                }
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Пользователи не могут быть подгружены из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }

        private void LoadUsersWithoutDialogs(int authenticatedUserId, string searchQuery)
        {
            try
            {
                UnknownContacts.Clear();
                var factory = new AppDbContextFactory();
                var context = factory.CreateDbContext(null);

                var contactsWithoutDialogs = context.dbContacts
                    .Where(u => u.UserId != authenticatedUserId &&
                                !context.dbMessages.Any(m => (m.SenderId == authenticatedUserId || m.ReceiverId == authenticatedUserId) &&
                                                              (m.SenderId == u.UserId || m.ReceiverId == u.UserId)))
                    .Where(u => u.UserName.Contains(searchQuery))
                    .Select(u => new ContactModel
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        Image = u.Image,
                        Email = u.Email,
                    })
                    .ToList();


                foreach (var contact in contactsWithoutDialogs)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        UnknownContacts.Add(contact);
                    });
                }
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Пользователи не могут быть подгружены из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }

        private void FromUnknownToKnown()
        {
            try
            {
                ContactModel sel = SelectedContact;
                if (!Contacts.Contains(SelectedContact))
                {
                    Contacts.Add(SelectedContact);
                    UnknownContacts.Remove(SelectedContact);
                    LoadUsersWithDialogs(AuthenticatedUser.UserId);
                    OnPropertyChanged(nameof(Contacts));
                    OnPropertyChanged(nameof(UnknownContacts));
                    SelectedContact = sel;

                }
            }
            catch
            {

            }
        }


        private void FromKnownToUnknown()
        {
            try
            {
                if (Contacts.Contains(SelectedContact))
                {
                    SelectedContact.LastMessageView = null;
                    SelectedContact.LastMessage = null;
                    UnknownContacts.Add(SelectedContact);
                    Contacts.Remove(SelectedContact);
                    OnPropertyChanged(nameof(Contacts));
                    OnPropertyChanged(nameof(UnknownContacts));
                    SelectedContact = null;
                }
            }
            catch
            {

            }
        }

        public async void LoadStickersAsync()
        {
            try
            {
                Stickers.Clear();
                var factory = new AppDbContextFactory();
                using (var context = factory.CreateDbContext(null))
                {
                    var stickersFromDb = await context.dbStickers.ToListAsync();

                    foreach (var sticker in stickersFromDb)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            Stickers.Add(sticker);
                        });
                    }
                }
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Стикеры не могут быть подгружены из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }

        }

        public async Task ShowDialogMessages()
        {
            try
            {
                if (SelectedContact != null)
                {
                    Messages.Clear();

                    var factory = new AppDbContextFactory();
                    var context = factory.CreateDbContext(null);

                    var messages = await context.dbMessages
                        .Where(m => ((m.SenderId == AuthenticatedUser.UserId && m.ReceiverId == SelectedContact.UserId) ||
                                     (m.SenderId == SelectedContact.UserId && m.ReceiverId == AuthenticatedUser.UserId)))
                        .OrderBy(m => m.SentAt)
                        .Select(m => new
                        {
                            Message = m,
                            Image = m.SenderId == AuthenticatedUser.UserId ? authenticatedUser.Image :
                            SelectedContact.Image,
                            UserName = m.SenderId == AuthenticatedUser.UserId ? authenticatedUser.UserName :
                            SelectedContact.UserName,
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

                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            Messages.Add(messageModel);
                        });
                    }
                }
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщения не могут быть подгружены из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

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
            System.Windows.Application.Current.Shutdown();
        }


        public async void SendTextMessageAsync(object sender)
        {
            try
            {
                var message = new MessageModel()
                {
                    SenderId = AuthenticatedUser.UserId,
                    ReceiverId = SelectedContact.UserId,
                    TypeOfMessage = "Text",
                    SentAt = DateTime.Now,
                    IsRead = false,
                    Message = Encoding.UTF8.GetBytes(Message),
                    UserName = AuthenticatedUser.UserName ?? "Unknown",
                    Image = AuthenticatedUser.Image ?? new byte[0]
                };


                Messages.Add(message);
                Message = "";

                var factory = new AppDbContextFactory();
                var context = factory.CreateDbContext(null);
                context.dbMessages.Add(message);
                await context.SaveChangesAsync();
                FromUnknownToKnown();
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщение не может быть отправлено из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }

        }

        public async void SendImageMessageAsync(object sender)
        {
            try
            {
                if (SelectedContact != null)
                {
                    var openFileDialog = new Microsoft.Win32.OpenFileDialog();
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
                FromUnknownToKnown();
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщение не может быть отправлено из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

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
            try
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
                if (Messages.Count == 0 || Messages.Count == 1)
                {
                    Messages.Clear();
                    FromKnownToUnknown();
                }
                else
                {
                    await ShowDialogMessages();
                }
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщение не может быть удалено из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }

        }
        private MessageModel _editingMessage;

        public void EditMessageAsync(object sender)
        {
            try
            {
                if (sender is MessageModel)
                {
                    IsEditing = true;

                    _editingMessage = sender as MessageModel;
                    Message = Encoding.UTF8.GetString(_editingMessage.Message);
                }
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщение не может быть обновлено из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }

        public async void SaveMessageAsync(object sender)
        {
            try
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
                            System.Windows.MessageBox.Show("Пустое сообщение не может быть отправлено!");
                            IsEditing = false;
                            return;
                        }

                        await context.SaveChangesAsync();
                    }
                }
                await ShowDialogMessages();
                IsEditing = false;
                Message = String.Empty;
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщение не может быть обновлено из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }

        public async void SendStickerAsync(object sender)
        {
            try
            {
                if (sender is StickerModel)
                {

                    AppDbContextFactory factory = new AppDbContextFactory();
                    AppDbContext context = factory.CreateDbContext(null);
                    StickerModel? elem = sender as StickerModel;
                    StickerModel? stickerFromDb = await context.dbStickers.FindAsync(elem?.StickerId);

                    if (stickerFromDb != null)
                    {
                        // Создаем новое сообщение на основе данных стикера
                        MessageModel newMessage = new MessageModel
                        {
                            SenderId = authenticatedUser.UserId,
                            ReceiverId = SelectedContact.UserId,
                            SentAt = DateTime.Now,
                            TypeOfMessage = "Sticker",
                            Message = stickerFromDb.Sticker, // Предполагается, что у стикера есть свойство Sticker, содержащее изображение
                            IsRead = false
                        };

                        // Добавляем новое сообщение в контекст базы данных
                        context.dbMessages.Add(newMessage);

                        // Сохраняем изменения
                        await context.SaveChangesAsync();
                    }
                }
                FromUnknownToKnown();
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщение не может быть отправлено из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }

        private bool _isListening = false;

        public bool IsListening
        {
            get { return _isListening; }
            set
            {
                _isListening = value;
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    OnPropertyChanged(nameof(IsListening));
                });
            }
        }

        private bool _isRecording = false;

        public bool IsRecording
        {
            get { return _isRecording; }
            set
            {
                _isRecording = value;
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    OnPropertyChanged(nameof(IsRecording));
                });
            }
        }


        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private MemoryStream audioStream;

        private async Task RecordAudioAsync(string audioFileName)
        {
            try
            {
                float gain = 10.0f;
                waveIn = new WaveInEvent();

                waveIn.DataAvailable += (s, e) =>
                {
                    for (int i = 0; i < e.BytesRecorded / 2; i++)
                    {
                        short sample = BitConverter.ToInt16(e.Buffer, i * 2);
                        sample = (short)(sample * gain);
                        BitConverter.GetBytes(sample).CopyTo(e.Buffer, i * 2);
                    }

                    audioStream.Write(e.Buffer, 0, e.BytesRecorded);
                    writer?.Write(e.Buffer, 0, e.BytesRecorded);
                };

                waveIn.RecordingStopped += (s, e) =>
                {
                    writer?.Dispose();
                    waveIn.Dispose();
                };

                waveIn.StartRecording();

                await Task.Run(() =>
                {
                    while (IsRecording) { }
                });

                waveIn.StopRecording();

                WaveFormat waveFormat = new WaveFormat(8120, 16, 1);
                writer = new WaveFileWriter(audioFileName, waveFormat);
                writer.Write(audioStream.GetBuffer(), 0, (int)audioStream.Length);
                writer.Dispose();
            }
            catch
            {

            }
        }


        private async void PlayOrDownloadAudioAsync(object sender)
        {
            try
            {
                if (sender is MessageModel)
                {
                    AppDbContextFactory factory = new AppDbContextFactory();
                    AppDbContext context = factory.CreateDbContext(null);
                    MessageModel? elem = sender as MessageModel;
                    MessageModel? messageFromDb = await context.dbMessages.FindAsync(elem?.MessageId);
                    if (messageFromDb != null)
                    {
                        string audioFileName = $"audio_{messageFromDb.SentAt:yyyyMMdd_HHmmss}.wav";
                        if (!File.Exists(audioFileName))
                        {
                            await DownloadAudioAsync(audioFileName, messageFromDb.SentAt);
                        }
                        await PlayAudioAsync(audioFileName);
                    }


                }
            }
            catch { }
        }

        private async Task DownloadAudioAsync(string audioFileName, DateTime audioSentAt)
        {
            try
            {
                // Получаем сообщение из базы данных
                var factory = new AppDbContextFactory();
                var context = factory.CreateDbContext(null);

                var message = await context.dbMessages
                    .Where(m => m.SentAt == audioSentAt)
                    .FirstOrDefaultAsync();

                if (message != null && message.Message != null)
                {
                    File.WriteAllBytes(audioFileName, message.Message);
                }
            }
            catch { }
        }

        private async Task PlayAudioAsync(string audioFileName)
        {
            try
            {
                if (File.Exists(audioFileName))
                {
                    // Используем NAudio для воспроизведения аудио
                    using (var audioFileReader = new AudioFileReader(audioFileName))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFileReader);
                        outputDevice.Play();

                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            await Task.Delay(1);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public async void SendVoiceMessageAsync(object sender)
        {
            try
            {
                if (IsRecording)
                {
                    IsRecording = false;
                    return;
                }

                DateTime audioSentAt = DateTime.Now;

                if (SelectedContact != null)
                {
                    IsRecording = true;
                    string audioFileName = $"audio_{audioSentAt:yyyyMMdd_HHmmss}.wav";
                    audioStream = new MemoryStream();

                    await RecordAudioAsync(audioFileName);

                    IsRecording = false;

                    var factory = new AppDbContextFactory();
                    var context = factory.CreateDbContext(null);

                    var message = new MessageModel
                    {
                        SenderId = authenticatedUser.UserId,
                        ReceiverId = SelectedContact.UserId,
                        SentAt = audioSentAt,
                        TypeOfMessage = "Audio",
                        Message = File.ReadAllBytes(audioFileName),
                        IsRead = false,
                    };

                    context.dbMessages.Add(message);
                    await context.SaveChangesAsync();
                }
                FromUnknownToKnown();
            }
            catch (DbException)
            {
                System.Windows.MessageBox.Show("Сообщение не может быть отправлено из-за отсутствия связи с бд, проверьте соединение с сетью");
            }
            catch
            {

            }
        }
    }
}

