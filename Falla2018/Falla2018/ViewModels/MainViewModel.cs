namespace Falla2018.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public ActsViewModel Events { get; set; }
        #endregion

        #region Propiedades
        public string BaseUrl { get; set; }
        public string ApiUrl { get; set; }
        #endregion

        #region Constructores
        public MainViewModel()
        {
            instance = this;

            BaseUrl = "http://antoniole.com/";
            ApiUrl = "/FallaMovilApi";
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
