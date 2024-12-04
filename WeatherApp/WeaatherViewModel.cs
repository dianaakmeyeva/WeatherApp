using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using WeatherApp.Command;
using WeatherApp.Model;

namespace WeatherApp
{
    internal class WeaatherViewModel : INotifyPropertyChanged
    {
        public City SelectedCity 
        {
            get => _selectedCity;
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                }
                if (_selectedCity != null)
                {
                    OnPropertyChanged(nameof(SelectedCity));
                    GetCurrentConditions();
                }
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
               _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        public CurrentConditions CurrentConditions
        {
            get => _currentConditions;
            set
            {
                if (value == _currentConditions) return;
                _currentConditions = value;
                OnPropertyChanged(nameof(CurrentConditions));
            }
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;

            CurrentConditions = await Helper.GetCurrentAsync(SelectedCity.Key);
        }
        private string _query;
        private CurrentConditions _currentConditions;
        private City _selectedCity;
        public SearchCommand SearchCommand { get; set; }
        public WeaatherViewModel()
        {
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void MakeQuery()
        {
            var cities = await Helper.GetCities(_query);
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }  
        }

    }
}
