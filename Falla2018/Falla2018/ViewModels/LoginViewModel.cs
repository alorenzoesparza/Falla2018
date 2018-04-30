namespace Falla2018.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Atributos
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Propiedades
        public string Email { get; set; }
        public string Password
        {
            get
            {
                return this.password;
            }
                
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.Password)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }

            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.IsRunning)));
                }
            }
        }

        public bool IsRemembered { get; set; }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }

            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.IsEnabled)));
                }
            }
        }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
            this.IsRunning = false;
        }
        #endregion

        #region Comandos
        public ICommand LoginCommand
        { get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    "Debes introducir tu Email", 
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes introducir tu contraseña",
                    "Aceptar");
                this.Password = string.Empty;
                return;
            }

            // Activar el ActivityIndicator (IsRunning) y desactivar Botones
            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "antonio@ono.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email o password incorrectos",
                    "Aceptar");
                this.Password = string.Empty;
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                "Logueandose",
                "Aceptar");
        }

        public ICommand RegisterCommand { get; set; }
        #endregion
    }
}
