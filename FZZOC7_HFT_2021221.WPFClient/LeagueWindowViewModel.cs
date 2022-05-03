using FZZOC7_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FZZOC7_HFT_2021221.WPFClient
{
    public class LeagueWindowViewModel : ObservableRecipient
    {
        public RestCollection<League> Leagues { get; set; }
        private League selectedLeague;

        public League SelectedLeague
        {
            get { return selectedLeague; }
            set
            {
                if (value != null)
                {
                    selectedLeague = new League()
                    {
                       League_ID = value.League_ID,
                       League_Name = value.League_Name,
                       Nation = value.Nation,
                       CL_Places = value.CL_Places,
                       Clubs = value.Clubs
                    };
                    OnPropertyChanged();
                    (DeleteLeagueCommand as RelayCommand).NotifyCanExecuteChanged();

                }
            }
        }


        public ICommand CreateLeagueCommand { get; set; }
        public ICommand DeleteLeagueCommand { get; set; }
        public ICommand UpdateLeagueCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public LeagueWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Leagues = new RestCollection<League>("http://localhost:4894/", "league", "hub");
                CreateLeagueCommand = new RelayCommand(() =>
                {
                    Leagues.Add(new League()
                    {
                        League_Name = SelectedLeague.League_Name,
                        League_ID = SelectedLeague.League_ID,
                        Nation = SelectedLeague.Nation,
                        CL_Places = SelectedLeague.CL_Places,
                        Clubs = SelectedLeague.Clubs

                    });
                });

                UpdateLeagueCommand = new RelayCommand(() =>
                {
                    Leagues.Update(SelectedLeague);
                });

                DeleteLeagueCommand = new RelayCommand(() =>
                {
                    Leagues.Delete(SelectedLeague.League_ID);

                },
                () =>
                {
                    return SelectedLeague != null;
                });

                SelectedLeague = new League();
            }
        }


    }
}
