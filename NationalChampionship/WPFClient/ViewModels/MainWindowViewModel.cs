using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using NationalChampionship.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NationalChampionship.WPFClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Club> Clubs { get; set; }

        public MainWindowViewModel()
        {
            Clubs = new RestCollection<Club>("https://localhost:44374/", "Club");
        }
    }
}
