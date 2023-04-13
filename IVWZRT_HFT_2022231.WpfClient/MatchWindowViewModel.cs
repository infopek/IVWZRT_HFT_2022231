using CommunityToolkit.Mvvm.Input;
using IVWZRT_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;

namespace IVWZRT_HFT_2022231.WpfClient
{
    public class MatchWindowViewModel : ObservableRecipient
    {
        public MatchWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Matches = new RestCollection<Match>("http://localhost:64082/", "Match", "hub");
                CreateMatchCommand = new RelayCommand(() =>
                {
                    Matches.Add(new Match()
                    {
                        GameMode = SelectedMatch.GameMode,
                        Map = SelectedMatch.Map,
                        Length = SelectedMatch.Length,
                    });
                });

                UpdateMatchCommand = new RelayCommand(() =>
                {
                    Matches.Update(SelectedMatch);
                });

                DeleteMatchCommand = new RelayCommand(() =>
                {
                    Matches.Delete(SelectedMatch.MatchId);
                },
                () =>
                {
                    return SelectedMatch != null;
                });

                SelectedMatch = new Match()
                {
                    MatchId = -1,
                    Length = 0,
                    GameMode = "trios",
                    Map = "olympus"
                };
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

        public ICommand CreateMatchCommand { get; set; }
        public ICommand DeleteMatchCommand { get; set; }
        public ICommand UpdateMatchCommand { get; set; }

        public RestCollection<Match> Matches { get; set; }

        public Match SelectedMatch
        {
            get { return _selectedMatch; }
            set
            {
                if (value != null)
                {
                    _selectedMatch = new Match()
                    {
                        MatchId = value.MatchId,
                        Length = value.Length,
                        GameMode = value.GameMode,
                        Map = value.Map
                    };
                    OnPropertyChanged();
                    (DeleteMatchCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Match _selectedMatch;
    }
}
