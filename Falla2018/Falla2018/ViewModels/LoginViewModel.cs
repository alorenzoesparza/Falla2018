namespace Falla2018.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Servicios
        private ApiService apiService;
        #endregion

        #region Atributos
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Propiedades
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered { get; set; }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            apiService = new ApiService();

            this.IsRemembered = true;
            this.IsEnabled = true;
            this.IsRunning = false;

            this.Email = "alorenzoesparza@ono.com";
            this.Password = "Antonio1.";
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

            var connection = await this.apiService.CheckConnection();
            
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                return;
            }

            var Token = await this.apiService.GetToken(
                "http://api.aacvalencia.es",
                this.Email,
                this.Password);

            this.IsRunning = false;
            this.IsEnabled = true;
            this.Email = string.Empty;
            this.Password = string.Empty;

            MainViewModel.GetInstance().Acts = new ActsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ActsPage());
        }

        public ICommand RegisterCommand { get; set; }
        #endregion
    }
}
