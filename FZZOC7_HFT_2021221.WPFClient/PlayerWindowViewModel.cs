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
    public class PlayerWindowViewModel : ObservableRecipient
    {
        public RestCollection<Player> Players { get; set; }
        private Player selectedPlayer;

        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set 
            {
                if (value != null)
                {
                    selectedPlayer = new Player()
                    {
                        Player_Name = value.Player_Name,
                        Player_ID = value.Player_ID,
                        Nationality = value.Nationality,
                        Wage = value.Wage,
                        Position = value.Position,
                        Club = value.Club,
                        Club_ID = value.Club_ID
                    };
                    OnPropertyChanged();
                    (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();

                }
            }
        }


        public ICommand CreatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public PlayerWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Players = new RestCollection<Player>("http://localhost:4894/", "player","hub");
                CreatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Add(new Player()
                    {
                       Player_Name = SelectedPlayer.Player_Name,
                       Nationality = SelectedPlayer.Nationality,
                       Position = SelectedPlayer.Nationality,
                       Club = SelectedPlayer.Club,
                       Wage = SelectedPlayer.Wage,
                       Club_ID = SelectedPlayer.Club_ID

                        
                    });
                });

                UpdatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Update(SelectedPlayer);
                });

                DeletePlayerCommand = new RelayCommand(() =>
                {
                    Players.Delete(SelectedPlayer.Player_ID);

                },
                () =>
                {
                    return SelectedPlayer != null;
                });

                SelectedPlayer = new Player();
            }
        }


    }
}
