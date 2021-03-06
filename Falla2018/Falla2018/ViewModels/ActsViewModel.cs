﻿namespace Falla2018.ViewModels
{
    using Services;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class ActsViewModel : BaseViewModel
    {
        #region Servicios
        private ApiService apiService;
        #endregion

        #region Atributos
        private ObservableCollection<ActItemViewModel> acts;
        private bool isRefreshing;
        private string filter;
        private List<Act> actsList;
        #endregion

        #region Propiedades
        public ObservableCollection<ActItemViewModel> Acts
        {
            get { return this.acts; }
            set { SetValue(ref this.acts, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                SetValue(ref this.isRefreshing, value);
                this.Search();
            }
        }

        public string Filter
        {
            get { return this.filter; }
            set { SetValue(ref this.filter, value); }
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

                    await Application.Current.MainPage.Navigation.PopAsync();
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
                    await Application.Current.MainPage.Navigation.PopAsync();
                    return;
                }

                this.actsList = (List<Act>)response.Result;
                this.Acts = new ObservableCollection<ActItemViewModel>(
                    this.ToActItemViewModel(this.actsList));

                //IsRefreshing = false;
            }
        }
        #endregion

        #region Metodos
        private IEnumerable<ActItemViewModel> ToActItemViewModel(List<Act> actsList)
        {
            return this.actsList.Select(a => new ActItemViewModel
            {
                ActoOficial = a.ActoOficial,
                Descripcion = a.Descripcion,
                FechaActo = a.FechaActo,
                HoraActo = a.HoraActo,
                IdAct = a.IdAct,
                Imagen = a.Imagen,
                Imagen500 = a.Imagen500,
                PagInicio = a.PagInicio,
                Precio = a.Precio,
                PrecioInfantiles = a.PrecioInfantiles,
                Titular = a.Titular,
                YaEfectuado = a.YaEfectuado,
            });
        }
        #endregion

        #region Comandos
        public ICommand SearchCommand
        { get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(filter))
            {
                this.Acts = new ObservableCollection<ActItemViewModel>(
                    this.ToActItemViewModel(this.actsList));
            }
            else
            {
                this.Acts = new ObservableCollection<ActItemViewModel>(
                    this.ToActItemViewModel(this.actsList)
                    .Where(a => a.Titular.ToLower().Contains(Filter.ToLower()))
                    .OrderByDescending(a => a.FechaActo));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadActs);
            }
        }
        #endregion
    }
}
