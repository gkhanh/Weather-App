using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Weather_App.Models;
using Weather_App.ServiceHandler;

namespace Weather_App.ViewModels
{
    public class ViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        WeatherService _weatherServices = new WeatherService();

        //Creating Properties for Model and for the String Property of the “city” (string parameter)
        private MainModels _mainmodel;// for xaml binding
        public MainModels MainModels
        {
            get { return _mainmodel; }
            set
            {
                _mainmodel = value;
                IconImageString = "http://openweathermap.org/img/w/" + _mainmodel.Weather[0].WeatherIcon + ".png";// fetch weather icon image
                OnPropertyChanged();
            }
        }
        private string _city;// for entry binding and for method parameter value
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                Task.Run ( async () => {await InitializeGetWeatherAsync();} );
                OnPropertyChanged();
            }
        }
        private string _iconImageString;// for weather icon image string binding
        public string IconImageString
        {
            get { return _iconImageString; }
            set
            {
                _iconImageString = value;
                OnPropertyChanged();
            }
        }
        private bool _isBusy;// for showing loader when the task is initializing
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        // Returning Weather Details from WeatherServices
        private async Task InitializeGetWeatherAsync()
        {
            try
            {
                IsBusy = true;// set the ui property "IsRunning" to true(loading) in Xaml ActivityIndicator Control
               MainModels = await _weatherServices.GetWeatherDetails(_city);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
