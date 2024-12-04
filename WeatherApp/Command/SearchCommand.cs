using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.Command
{
    internal class SearchCommand : ICommand
    {
        public WeaatherViewModel ViewModel { get; set; }
        public event EventHandler? CanExecuteChanged;

        public SearchCommand(WeaatherViewModel weaatherViewModel)
        {
            ViewModel = weaatherViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            var query = parameter as string;
            return !string.IsNullOrWhiteSpace(query);
        }

        public void Execute(object? parameter)
        {
            ViewModel.MakeQuery();
        }
    }
}
