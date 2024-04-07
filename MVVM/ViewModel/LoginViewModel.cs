using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp1.Core;
using WpfApp1.MVVM.Model;
using System.IO;
using WpfApp1.MVVM.View;
using WpfApp1.MVVM.Trackers;
using MessageBox = System.Windows.MessageBox;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.MVVM.ViewModel
{
    internal class LoginViewModel : ObservableObject, IDataErrorInfo
    {
        Window mainWindow = System.Windows.Application.Current.MainWindow;

        public event EventHandler<UserUpdatedEventArgs> UserUpdated;
        private void OnUserUpdated(bool success)
        {
            UserUpdated?.Invoke(this, new UserUpdatedEventArgs(success));
        }

        public ICommand MinimizeCommand { get; private set; }
        public ICommand MaximizeRestoreCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand ShowAuthorizeCommand { get; private set; }
        public ICommand UpdateUserCommand { get; set; }
        public ICommand ChooseAvatarCommand { get; set; }
        private ICommand showLoginViewCommand;
        private ICommand showRegisterViewCommand;
        private RelayCommand loginCommand;
        private RelayCommand registerCommand;

        private string _email = "1@example.com";
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _password = "your_password";
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _name = "your_name";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        private static ContactModel? curUser;
        public static ContactModel CurUser
        {
            get
            {
                return curUser;
            }
            set
            {
                curUser = value;
            }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged("Error");
            }
        }

        public LoginViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            MaximizeRestoreCommand = new RelayCommand(MaximizeRestore);

        }
        public LoginViewModel(ContactModel contact)
        {
            CurUser = contact;
            Email = CurUser.Email;
            Password = CurUser.Password;
            Image = CurUser.Image ?? new byte[0];
            Name = CurUser.UserName;
            ChooseAvatarCommand = new RelayCommand(ChooseAvatar);
            UpdateUserCommand = new RelayCommand(UpdateUser, CanRegister);
        }

        public void ToLogin(object sender)
        {
            LoginWindow vie = new LoginWindow();
            vie.Show();
            var cur = System.Windows.Application.Current.MainWindow;
            System.Windows.Application.Current.MainWindow = vie;
            cur.Close();
        }

        public void ToRegister(object sender)
        {
            RegisterView vie = new RegisterView();
            vie.Show();
            var cur = System.Windows.Application.Current.MainWindow;
            System.Windows.Application.Current.MainWindow = vie;
            cur.Close();
        }

        public void UpdateUser(object sender)
        {
            var factory = new AppDbContextFactory();
            var context = factory.CreateDbContext(null);
            var existingContact = context.dbContacts.FirstOrDefault(c => c.UserId == CurUser.UserId);
            if (existingContact != null)
            {
                existingContact.Email = Email;
                existingContact.UserName = Name;
                existingContact.Password = Password;
                existingContact.Image = AvatarImagePath != null ? File.ReadAllBytes(AvatarImagePath) : existingContact.Image;

                context.SaveChanges();
                OnUserUpdated(true);
            }
            else
            {
                OnUserUpdated(false);
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
                mainWindow.WindowState = WindowState.Normal;
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

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new RelayCommand(Login, CanLogin);
                }
                return loginCommand;
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                {
                    registerCommand = new RelayCommand(Register, CanRegister);
                }
                return registerCommand;
            }
        }

        private static string? _avatarImagePath;
        public string AvatarImagePath
        {
            get { return _avatarImagePath; }
            set
            {
                if (_avatarImagePath != value)
                {
                    _avatarImagePath = value;
                    OnPropertyChanged(nameof(AvatarImagePath));
                }
            }
        }

        private void ChooseAvatar(object parameter)
        {
            try
            {
                var openFileDialog = new System.Windows.Forms.OpenFileDialog
                {
                    Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp",
                    Title = "Выберите аватар"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    AvatarImagePath = openFileDialog.FileName;
                    if (AvatarImagePath == null)
                    {
                        AvatarImagePath = Path.Combine(MainViewModel.GetParentDirectory(3), "Icons/unknown.jpg");
                        Image = File.ReadAllBytes(AvatarImagePath);
                        return;
                    }
                    else
                    {
                        Image = File.ReadAllBytes(AvatarImagePath);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string this[string columnName]
        {
            get
            {
                string? error = null;
                switch (columnName)
                {
                    case nameof(Email):
                        if (string.IsNullOrEmpty(Email))
                            return "Email is required.";
                        else if (!Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                            return "Email is not valid. \nExample: nikon.chigoya@mail.ru";
                        break;

                    case nameof(Password):
                        if (string.IsNullOrEmpty(Password))
                            return "Password is required.";
                        else if (Password.Length < 6)
                            return "Password must be at least 6 characters long.";
                        break;
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name))
                            return "Name is required.";
                        else if (Name.Length < 3)
                            return "Name must be at least 3 characters long.";
                        break;
                }
                return null;
            }
        }

        private bool CanLogin(object parameter)
        {
            return IsValid();
        }

        private bool CanRegister(object parameter)
        {
            return IsValid();
        }

        private void Login(object parameter)
        {
            try
            {
                var context = new AppDbContextFactory();
                var dbContext = context.CreateDbContext(null);
                ContactModel model = dbContext.dbContacts.FirstOrDefault(u => u.Email == Email);

                if (model == null)
                {
                    Error = "Пользователя с таким Email не существует";
                    return;
                }

                if (model.Password != Password)
                {
                    Error = "Введен неверный пароль";
                    return;
                }

                var mainViewModel = new MainViewModel(model);
                var mainWindow = new MainWindow();
                mainWindow.DataContext = mainViewModel;
                mainWindow.Show();
                var cur = System.Windows.Application.Current.MainWindow;
                System.Windows.Application.Current.MainWindow = mainWindow;
                cur.Close();
            }
            catch (DbException)
            {
                // Обработка ошибок, связанных с базой данных
                Error = "Ошибка при работе с базой данных, не удается соединиться для авторизации!";
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                Error = "Произошла ошибка: " + ex.Message;
            }
        }


        private void Register(object parameter)
        {
            try
            {
                var context = new AppDbContextFactory();
                var dbContext = context.CreateDbContext(null);
                if (dbContext.dbContacts.Where(u => u.Email == Email).Count() >= 1)
                {
                    Error = "Пользователь с таким Email уже существует";
                    return;
                }
                ContactModel contact = new ContactModel
                {
                    Image = AvatarImagePath == null ? File.ReadAllBytes(Path.Combine(MainViewModel.GetParentDirectory(3), "Icons/unknown.jpg"))
                    : File.ReadAllBytes(AvatarImagePath),
                    Email = Email,
                    UserName = Name == "" ? "Unknown" : Name,
                    Password = Password,
                    RegistrationDate = DateTime.Now,
                };

                dbContext.dbContacts.Add(contact);
                dbContext.SaveChangesAsync(); // Ждем завершения асинхронной операции сохранения

                Error = "Пользователь добавлен";
                var loginWindow = new LoginWindow();
                var cur = System.Windows.Application.Current.MainWindow;
                System.Windows.Application.Current.MainWindow = loginWindow;
                cur.Close();
                loginWindow.Show();
            }
            catch (DbException)
            {
                Error = "Ошибка при регистрации, необходимо соединение";
            }
            catch (Exception ex)
            {
                Error = $"Произошла ошибка: {ex.Message}";
            }
        }


        public bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(Email)]) && string.IsNullOrEmpty(this[nameof(Password)]) && string.IsNullOrEmpty(this[nameof(Name)]);
        }

        public ICommand ShowLoginViewCommand
        {
            get
            {
                if (showLoginViewCommand == null)
                {
                    showLoginViewCommand = new RelayCommand(ToLogin);
                }
                return showLoginViewCommand;
            }
        }

        public ICommand ShowRegisterViewCommand
        {
            get
            {
                if (showRegisterViewCommand == null)
                {
                    showRegisterViewCommand = new RelayCommand(ToRegister);
                }
                return showRegisterViewCommand;
            }
        }
    }
}
