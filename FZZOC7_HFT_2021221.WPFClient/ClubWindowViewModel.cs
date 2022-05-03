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
    public class ClubWindowViewModel : ObservableRecipient
    {
        public RestCollection<Club> Clubs { get; set; }
        private Club selectedClub;

        public Club SelectedClub
        {
            get { return selectedClub; }
            set
            {
                if (value != null)
                {
                    selectedClub = new Club()
                    {
                        Club_Name = value.Club_Name,
                        Club_ID = value.Club_ID,
                        Owner = value.Owner,
                        Manager = value.Manager,
                        League = value.League,
                        League_ID = value.League_ID,
                        Players = value.Players

                    };
                    OnPropertyChanged();
                    (DeleteClubCommand as RelayCommand).NotifyCanExecuteChanged();
                    (CreateClubCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateClubCommand as RelayCommand).NotifyCanExecuteChanged();

                }
            }
        }


        public ICommand CreateClubCommand { get; set; }
        public ICommand DeleteClubCommand { get; set; }
        public ICommand UpdateClubCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ClubWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Clubs = new RestCollection<Club>("http://localhost:4894/", "club", "hub");
                CreateClubCommand = new RelayCommand(() =>
                {
                    Clubs.Add(new Club()
                    {
                        Club_Name = SelectedClub.Club_Name,
                        Club_ID = SelectedClub.Club_ID,
                        Owner = SelectedClub.Owner,
                        Manager = SelectedClub.Manager,
                        League = SelectedClub.League,
                        League_ID = SelectedClub.League_ID,
                        Players = SelectedClub.Players

                    });
                });

                UpdateClubCommand = new RelayCommand(() =>
                {
                    Clubs.Update(SelectedClub);
                });

                DeleteClubCommand = new RelayCommand(() =>
                {
                    Clubs.Delete(SelectedClub.Club_ID);

                },
                () =>
                {
                    return SelectedClub != null;
                });

                SelectedClub = new Club();
            }
        }


    }
}
