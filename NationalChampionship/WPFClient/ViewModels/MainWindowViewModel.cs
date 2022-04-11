using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using NationalChampionship.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NationalChampionship.WPFClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Club> Clubs { get; set; }
        public RestCollection<Manager> Managers { get; set; }
        public RestCollection<Player> Players { get; set; }
        public List<PlayerPosition> PlayerPositions { get; set; }
        public List<PreferredFoot> PreferredFeet { get; set; }

        private Club selectedClub;
        public Club SelectedClub
        {
            get => selectedClub;
            set
            {
                if (value != null)
                {
                    selectedClub = new Club()
                    {
                        ClubId = value.ClubId,
                        ClubName = value.ClubName,
                        ClubColour = value.ClubColour,
                        ClubCity = value.ClubCity,
                        ClubFounded = value.ClubFounded,
                        Stadium = value.Stadium,
                        Manager = value.Manager,
                        Players = value.Players,
                        Users = value.Users
                    };
                    OnPropertyChanged();
                    (UpdateClubCommand as RelayCommand).NotifyCanExecuteChanged();
                    (DeleteClubCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Manager selectedManager;
        public Manager SelectedManager
        {
            get => selectedManager;
            set
            {
                if (value != null)
                {
                    selectedManager = new Manager()
                    {
                        ManagerId = value.ManagerId,
                        ManagerName = value.ManagerName,
                        CountryCode = value.CountryCode,
                        ManagerCountry = value.ManagerCountry,
                        ManagerBirthdate = value.ManagerBirthdate,
                        ManagerStartYear = value.ManagerStartYear,
                        PreferredFormation = value.PreferredFormation,
                        WonChampionship = value.WonChampionship,
                        ClubId = value.ClubId,
                        Club = value.Club
                    };
                    OnPropertyChanged();
                    (UpdateManagerCommand as RelayCommand).NotifyCanExecuteChanged();
                    (DeleteManagerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Player selectedPlayer;
        public Player SelectedPlayer
        {
            get => selectedPlayer;
            set
            {
                if (value != null)
                {
                    selectedPlayer = new Player()
                    {
                        PlayerId = value.PlayerId,
                        PlayerName = value.PlayerName,
                        CountryCode = value.CountryCode,
                        PlayerCountry = value.PlayerCountry,
                        PlayerBirthdate = value.PlayerBirthdate,
                        PlayerPosition = value.PlayerPosition,
                        ShirtNumber = value.ShirtNumber,
                        Height = value.Height,
                        PreferredFoot = value.PreferredFoot,
                        PlayerValue = value.PlayerValue,
                        Captain = value.Captain,
                        ClubId = value.ClubId,
                        Club = value.Club
                    };
                    OnPropertyChanged();
                    (UpdatePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                    (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Club newClub;
        public Club NewClub
        {
            get => newClub;
            set => SetProperty(ref newClub, value);
        }

        private Manager newManager;
        public Manager NewManager
        {
            get => newManager;
            set => SetProperty(ref newManager, value);
        }

        private Player newPlayer;
        public Player NewPlayer
        {
            get => newPlayer;
            set => SetProperty(ref newPlayer, value);
        }

        public ICommand AddClubCommand { get; set; }
        public ICommand AddManagerCommand { get; set; }
        public ICommand AddPlayerCommand { get; set; }
        public ICommand DeleteClubCommand { get; set; }
        public ICommand DeleteManagerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }
        public ICommand UpdateClubCommand { get; set; }
        public ICommand UpdateManagerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }

        public int IdGenerator()
        {
            var ids = Players.Select(x => x.PlayerId);
            Random random = new Random();
            int id;
            do
            {
                id = random.Next(1000, 100000);
            } while (ids.Contains(id));
            return id;
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Clubs = new RestCollection<Club>("https://localhost:44374/", "Club", "hub");
                Managers = new RestCollection<Manager>("https://localhost:44374/", "Manager", "hub");
                Players = new RestCollection<Player>("https://localhost:44374/", "Player", "hub");
                PlayerPositions = new List<PlayerPosition>();
                PlayerPositions = Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>().ToList();
                PreferredFeet = new List<PreferredFoot>();
                PreferredFeet = Enum.GetValues(typeof(PreferredFoot)).Cast<PreferredFoot>().ToList();

                AddClubCommand = new RelayCommand(
                    () =>
                    {
                        Clubs.Add(new Club()
                        {
                            ClubName = NewClub.ClubName,
                            ClubColour = NewClub.ClubColour,
                            ClubCity = NewClub.ClubCity,
                            ClubFounded = NewClub.ClubFounded,
                            Stadium = NewClub.Stadium,
                            Manager = NewClub.Manager,
                            Players = NewClub.Players,
                            Users = NewClub.Users
                        });
                        MessageBox.Show("Successful addition!");
                    });
                AddManagerCommand = new RelayCommand(
                    () =>
                    {
                        Managers.AddTo(new Manager()
                        {
                            ManagerName = NewManager.ManagerName,
                            CountryCode = NewManager.CountryCode,
                            ManagerCountry = NewManager.ManagerCountry,
                            ManagerBirthdate = NewManager.ManagerBirthdate,
                            ManagerStartYear = NewManager.ManagerStartYear,
                            PreferredFormation = NewManager.PreferredFormation,
                            WonChampionship = NewManager.WonChampionship,
                            ClubId = NewManager.Club.ClubId,
                            Club = NewManager.Club
                        }, NewManager.Club.ClubId);
                        MessageBox.Show("Successful addition!");
                    });
                AddPlayerCommand = new RelayCommand(
                    () =>
                    {
                        Players.AddTo(new Player()
                        {
                            PlayerId = IdGenerator(),
                            PlayerName = NewPlayer.PlayerName,
                            CountryCode = NewPlayer.CountryCode,
                            PlayerCountry = NewPlayer.PlayerCountry,
                            PlayerBirthdate = NewPlayer.PlayerBirthdate,
                            PlayerPosition = NewPlayer.PlayerPosition,
                            ShirtNumber = NewPlayer.ShirtNumber,
                            Height = NewPlayer.Height,
                            PreferredFoot = NewPlayer.PreferredFoot,
                            PlayerValue = NewPlayer.PlayerValue,
                            Captain = NewPlayer.Captain,
                            ClubId = NewPlayer.ClubId,
                            Club = NewPlayer.Club
                        }, NewPlayer.Club.ClubId);
                        MessageBox.Show("Successful addition!");
                    });
                UpdateClubCommand = new RelayCommand(
                    () =>
                    {
                        Clubs.Update(SelectedClub, SelectedClub.ClubId);
                        MessageBox.Show("Successful modification!");
                    }, () => SelectedClub != null);
                UpdateManagerCommand = new RelayCommand(
                    () =>
                    {
                        Managers.Update(SelectedManager, SelectedManager.ManagerId);
                        MessageBox.Show("Successful modification!");
                    }, () => SelectedManager != null);
                UpdatePlayerCommand = new RelayCommand(
                    () =>
                    {
                        Players.Update(SelectedPlayer, SelectedPlayer.PlayerId);
                    }, () => SelectedPlayer != null);
                DeleteClubCommand = new RelayCommand(
                    () =>
                    {
                        Clubs.Delete(SelectedClub.ClubId);
                        MessageBox.Show("Successful deletion!");
                    }, () => SelectedClub != null);
                DeleteManagerCommand = new RelayCommand(
                    () =>
                    {
                        Managers.Delete(SelectedManager.ManagerId);
                        MessageBox.Show("Successful deletion!");
                    }, () => SelectedManager != null);
                DeletePlayerCommand = new RelayCommand(
                    () =>
                    {
                        Players.Delete(SelectedPlayer.PlayerId);
                        MessageBox.Show("Successful deletion!");
                    }, () => SelectedPlayer != null);

                NewClub = new Club();
                NewManager = new Manager();
                NewPlayer = new Player();
            }
        }
    }
}
