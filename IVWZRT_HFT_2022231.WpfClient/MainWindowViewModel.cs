using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IVWZRT_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IVWZRT_HFT_2022231.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Players = new RestCollection<Player>("http://localhost:64082/", "player", "hub");
                CreatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Add(new Player()
                    {
                        UserName = SelectedPlayer.UserName,
                        Age = SelectedPlayer.Age,

                        Rank = SelectedPlayer.Rank,
                        NumGames = SelectedPlayer.NumGames,
                        TotalKills = SelectedPlayer.TotalKills,
                        TotalDeaths = SelectedPlayer.TotalDeaths
                    });
                });

                UpdatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Update(SelectedPlayer);
                });

                DeletePlayerCommand = new RelayCommand(() =>
                {
                    Players.Delete(SelectedPlayer.PlayerId);
                },
                () =>
                {
                    return SelectedPlayer != null;
                });
                SelectedPlayer = new Player();
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ICommand CreatePlayerCommand { get; set; }    
        public ICommand DeletePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }

        public RestCollection<Player> Players { get; set; }

        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
            set 
            {
                if (value != null)
                {
                    _selectedPlayer = new Player()
                    {
                        PlayerId = value.PlayerId,
                        UserName = value.UserName,
                        Age = value.Age,

                        Rank = value.Rank,
                        NumGames = value.NumGames,
                        TotalKills = value.TotalKills,
                        TotalDeaths = value.TotalDeaths
                    };
                    OnPropertyChanged();
                    (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Player _selectedPlayer;
    }
}
