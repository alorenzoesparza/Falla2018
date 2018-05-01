namespace Falla2018.ViewModels
{
    using Services;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ActsViewModel : BaseViewModel
    {
        #region Servicios
        private ApiService apiService;
        #endregion

        #region Atributos
        private ObservableCollection<Act> acts;
        #endregion

        #region Propiedades
        public ObservableCollection<Act> Acts
        {
            get { return this.acts; }
            set { SetValue(ref this.acts, value); }
        }
        #endregion

        #region Constructores
        public ActsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadActs();
        }

        #endregion

        #region Metodos
        private async void LoadActs()
        {
            {
                //IsRefreshing = true;

                var conexion = await apiService.CheckConnection();
                if (!conexion.IsSuccess)
                {
                    //await dialogService.VerMensaje(
                    //    "Error",
                    //    conexion.Message);
                    return;
                }

                var mainViewModel = MainViewModel.GetInstance(); // Acceder al Singleton

                var response = await apiService.GetList<Act>(
                    string.Format("{0}", mainViewModel.BaseUrl),
                    string.Format("{0}/api", mainViewModel.ApiUrl),
                    "/Acts");

                if (!response.IsSuccess)
                {
                    //await dialogService.VerMensaje(
                    //    "Error",
                    //    conexion.Message);
                    //return;
                }

                var acts = (List<Act>)response.Result;
                this.Acts = new ObservableCollection<Act>(
                    acts.OrderByDescending(a => a.FechaActo));

                //IsRefreshing = false;
            }
        }
        #endregion
    }
}
