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
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp1.Core;
using WpfApp1.MVVM.Model;
using WpfApp1.MVVM.View;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WpfApp1.MVVM.ViewModel
{
    internal class LoginViewModel : ObservableObject, IDataErrorInfo
    {
        Window mainWindow = System.Windows.Application.Current.MainWindow;



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
        public ICommand MaximizeRestoreCommand {  get; private set; }
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

        private UserControl _currentView;

        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        NavigationWindow win = new NavigationWindow();
        //public ICommand ShowLoginPageCommand { get; private set; }
        //public ICommand ShowRegisterPageCommand { get; private set; }


        public LoginViewModel() {

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            MaximizeRestoreCommand = new RelayCommand(MaximizeRestore);
            //CurrentView = new LoginView();
            //ShowLoginPageCommand = new RelayCommand(ShowLoginPage);
            //ShowRegisterPageCommand = new RelayCommand(ShowRegisterPage);
            ShowRegisterPageCommand = new RelayCommand(ShowRegisterPage);
            ShowLoginPageCommand = new RelayCommand(ShowLoginPage);
        }

        private void ShowLoginPage(object parameter)
        {
            win.Content = new LoginView();
        }

        private void ShowRegisterPage(object parameter)
        {
            win.Content = new RegisterView();
        }

        //public void ShowLoginView()
        //{
        //    CurrentView = new LoginView();
        //}

        //public void ShowRegisterView()
        //{
        //    CurrentView = new RegisterView();
        //}
        //public void ShowLoginView(object sender)
        //{
        //    MainFrame.Navigate(new LoginView());
        //}

        //public void ShowRegisterView(object sender)
        //{
        //    MainFrame.Navigate(new RegisterView());
        //}

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



            var mainViewModel = new MainViewModel();
            var mainWindow = new MainWindow();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
            System.Windows.Application.Current.MainWindow = mainWindow;
            this.mainWindow.Close();

            // Закройте окно авторизации

        }



        private void Register(object parameter)
        {
            // Логика для регистрации нового пользователя
            // Если успешно, открываем MainWindow
            var mainWindow = new MainWindow();
            mainWindow.Show();
            (parameter as Window)?.Close();
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(Email)]) && string.IsNullOrEmpty(this[nameof(Password)]);
        }

        private RelayCommand showRegisterView;
        private RelayCommand showLoginView;
        //public ICommand ShowRegisterViewCommand => showRegisterView ??= new RelayCommand(ShowRegisterView);
        //public ICommand ShowLoginViewCommand => showLoginView ??= new RelayCommand(ShowLoginView);

        private void PerformShowRegisterView(object commandParameter)
        {
        }

        private RelayCommand showRegisterPage;
        private RelayCommand showLoginPage;
        public ICommand ShowRegisterPageCommand;
        public ICommand ShowLoginPageCommand;

        //private void PerformShowRegisterPage(object commandParameter)
        //{
        //}
    }
}
