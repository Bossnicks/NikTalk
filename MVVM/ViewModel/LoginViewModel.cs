using Microsoft.VisualBasic.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp1.Core;
using WpfApp1.MVVM.Model;
using System.IO;
using WpfApp1.MVVM.View;
using WpfApp1.MVVM.Trackers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp1.MVVM.ViewModel
{
    internal class LoginViewModel : ObservableObject, IDataErrorInfo
    {
        Window mainWindow = System.Windows.Application.Current.MainWindow;


        public event EventHandler<UserUpdatedEventArgs> UserUpdated;

        // ... остальной код ...

        private void OnUserUpdated(bool success)
        {
            UserUpdated?.Invoke(this, new UserUpdatedEventArgs(success));
        }

        //public System.Windows.Controls.UserControl CurrentView
        //{
        //    get { return _currentView; }
        //    set
        //    {
        //        _currentView = value;
        //        OnPropertyChanged(nameof(CurrentView));
        //    }
        //}


        public Frame MainFrame { get; set; }
        public ICommand MinimizeCommand { get; private set; }
        public ICommand MaximizeRestoreCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand ShowAuthorizeCommand { get; private set; }
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
        private static ContactModel curUser;
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

        //private UserControl _currentView;

        //public UserControl CurrentView
        //{
        //    get { return _currentView; }
        //    set
        //    {
        //        _currentView = value;
        //        OnPropertyChanged(nameof(CurrentView));
        //    }
        //}

        public ICommand ChooseAvatarCommand { get; set; }
        public LoginViewModel()
        {
            //MainViewModel.BeginData();
            MainViewModel.AddStickersFromDirectoryAsync();
            MainViewModel.GetParentDirectory(3);

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            MaximizeRestoreCommand = new RelayCommand(MaximizeRestore);
            //CurrentView = new LoginView();
            //ShowLoginPageCommand = new RelayCommand(ShowLoginPage);
            //ShowRegisterPageCommand = new RelayCommand(ShowRegisterPage);
            ChooseAvatarCommand = new RelayCommand(ChooseAvatar);
            //ShowLoginPageCommand = new RelayCommand(ToLogin, _ => true);
            //ShowRegisterPageCommand = new RelayCommand(ToRegister, _=> true);

        }
        public ICommand UpdateUserCommand { get; set; }
        public LoginViewModel(ContactModel contact)
        {
            CurUser = contact;
            Email = CurUser.Email;
            Password = CurUser.Password;
            Image = CurUser.Image ?? new byte[0];
            Name = CurUser.UserName;

            ChooseAvatarCommand = new RelayCommand(ChooseAvatar);
            UpdateUserCommand = new RelayCommand(UpdateUser);
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
                // Обновляем свойства пользователя
                existingContact.Email = Email;
                existingContact.UserName = Name;
                existingContact.Password = Password;
                existingContact.Image = AvatarImagePath == null ? File.ReadAllBytes(AvatarImagePath) : null;

                // Другие свойства, которые вы хотите обновить

                context.SaveChanges(); // Сохраняем изменения в базе данных

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

        private static string _avatarImagePath; // Переменная для хранения пути к выбранному аватару



        // Инициализация команд




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
            var openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp",
                Title = "Выберите аватар"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AvatarImagePath = openFileDialog.FileName;
            }
            Image = File.ReadAllBytes(AvatarImagePath);
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;
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

            // Закройте окно авторизации

        }



        private void Register(object parameter)
        {
            var context = new AppDbContextFactory();
            var dbContext = context.CreateDbContext(null);
            ContactModel contact = new ContactModel
            {
                Image = AvatarImagePath == null ? File.ReadAllBytes(AvatarImagePath) : null,
                Email = Email,
                UserName = Name == null ? "crfe" : Name,
                Password = Password,
                RegistrationDate = DateTime.Now,
            };
            dbContext.dbContacts.Add(contact);
            dbContext.SaveChangesAsync();
            Error = "Пользователь добавлен";
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            System.Windows.Application.Current.MainWindow = loginWindow;
            this.mainWindow.Close();

        }



        public bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(Email)]) && string.IsNullOrEmpty(this[nameof(Password)]);
        }

        private RelayCommand showRegisterView;
        private RelayCommand showLoginView;


        private void PerformShowRegisterView(object commandParameter)
        {
        }

        private RelayCommand showRegisterPage;
        private RelayCommand showLoginPage;
        public RelayCommand ShowRegisterPageCommand;
        public RelayCommand ShowLoginPageCommand;

        private ICommand showLoginViewCommand;
        private ICommand showRegisterViewCommand;

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
        //private void PerformShowRegisterPage(object commandParameter)
        //{
        //}
    }
}
