namespace Falla2018.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class ActItemViewModel : Act
    {
        #region Comandos
        public ICommand SelectActCommand
        {
            get
            {
                return new RelayCommand(SelectAct);
            }
        }
        #endregion

        #region Metodos
        private async void SelectAct()
        {
            MainViewModel.GetInstance().Act = new ActViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new ActView());
        }
        #endregion
    }
}
