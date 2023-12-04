using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FootballStats.Commands;
using FootballStats.Database;
using FootballStats.Models;
using ReactiveUI;

namespace FootballStats.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        Initialize();
        GetPlayerStatsCommand = new RelayCommand(async () =>
        {
            var playerId = GetPlayerIdFromFullName();
            var playerStats = await GetPlayerStatModelsAsync(playerId);
            PlayerStatModels = playerStats;
            PlayerStatViewModel = GetPlayerStatViewModels();
        });
    }

    private async void Initialize()
    {
        ButtonName = "Показать статистику";
        PlayerModels = await GetPlayerModelsAsync();
        PlayersViewModels = GetPlayersViewModel();
        FullnameList = GetFullnameList(PlayersViewModels);
        SeasonModels = await GetSeasonModelsAsync();
        SeasonViewModel = GetSeasonsViewModels();
        DataGridViewModel = new();
        PlayerStatViewModel = new();
    }

    private string _buttonName;
    public string ButtonName
    {
        get => _buttonName;
        set => this.RaiseAndSetIfChanged(ref _buttonName, value);
    }

    private List<SeasonModel> _seasonModels;
    public List<SeasonModel> SeasonModels
    {
        get => _seasonModels;
        set => this.RaiseAndSetIfChanged(ref _seasonModels, value);
    }

    private List<PlayerModel> _playerModels;
    private List<PlayerModel> PlayerModels
    {
        get => _playerModels;
        set => this.RaiseAndSetIfChanged(ref _playerModels, value);
    }

    private List<PlayerStatModel> _playerStatModels;
    public List<PlayerStatModel> PlayerStatModels
    {
        get => _playerStatModels;
        set => this.RaiseAndSetIfChanged(ref _playerStatModels, value);
    }

    private List<PlayerViewModel> _playersViewModels;
    public List<PlayerViewModel> PlayersViewModels
    {
        get => _playersViewModels;
        set => this.RaiseAndSetIfChanged(ref _playersViewModels, value);
    }

    private List<SeasonViewModel> _seasonViewModel;
    public List<SeasonViewModel> SeasonViewModel
    {
        get => _seasonViewModel;
        set => this.RaiseAndSetIfChanged(ref _seasonViewModel, value);
    }

    private List<PlayerStatViewModel> _playerStatViewModel;
    public List<PlayerStatViewModel> PlayerStatViewModel
    {
        get => _playerStatViewModel;
        set => this.RaiseAndSetIfChanged(ref _playerStatViewModel, value);
    }

    private string _selectedFullName;
    public string SelectedFullName
    {
        get => _selectedFullName;
        set => this.RaiseAndSetIfChanged(ref _selectedFullName, value);
    }

    private List<string> _fullnameList;
    public List<string> FullnameList
    {
        get => _fullnameList;
        set => this.RaiseAndSetIfChanged(ref _fullnameList, value);
    }

    private static List<string> GetFullnameList(List<PlayerViewModel> playerModels)
    {
        return playerModels.Select(playerModel => playerModel.Fullname).ToList();
    }
    private static async Task<List<SeasonModel>> GetSeasonModelsAsync()
    {
        await using var dbContext = new MyDbContext();
        var seasonModels = await new SeasonRepository().GetAllSeasonsAsync();
        return seasonModels;
    }
    private static async Task<List<PlayerModel>> GetPlayerModelsAsync()
    {
        await using var dbContext = new MyDbContext();
        var playerModels = await new PlayerRepository().GetAllPlayersAsync();
        return playerModels;
    }
    private static async Task<List<PlayerStatModel>> GetPlayerStatModelsAsync(int playerId)
    {
        await using var dbContext = new MyDbContext();
        var playerStatModels = await new PlayerStatRepository().GetPlayerStatsAsync(playerId);
        return playerStatModels;
    }
    private List<PlayerViewModel> GetPlayersViewModel()
    {
        var playerViewModelList = PlayerModels.Select(playerModel => new PlayerViewModel(playerModel)).ToList();
        return playerViewModelList;
    }
    private List<SeasonViewModel> GetSeasonsViewModels()
    {
        var seasonViewModelList = SeasonModels.Select(seasonModel => new SeasonViewModel(seasonModel)).ToList();
        return seasonViewModelList;
    }

    private List<PlayerStatViewModel> GetPlayerStatViewModels()
    {
        List<PlayerStatViewModel> playerStatsViewModelList = new();

        foreach (var playerStatModel in PlayerStatModels)
        {
            playerStatsViewModelList.AddRange(from seasonModel in SeasonModels
                where seasonModel.Id == playerStatModel.SeasonId
                select new PlayerStatViewModel(playerStatModel, seasonModel));
        }

        return playerStatsViewModelList;
    }
    private int GetPlayerIdFromFullName()
    {
        return PlayerModels.Where(playerModel => SelectedFullName == playerModel.Fullname).Select(playerModel => playerModel.Id).FirstOrDefault();
    }
    public ICommand GetPlayerStatsCommand { get; }
    private DataGridViewModel _dataGridViewModel;
    public DataGridViewModel DataGridViewModel
    {
        get => _dataGridViewModel;
        private set => this.RaiseAndSetIfChanged(ref _dataGridViewModel, value);
    }
}
