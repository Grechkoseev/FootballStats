using FootballStats.Models;

namespace FootballStats.ViewModels;

public class PlayerStatViewModel : ViewModelBase
{
    private PlayerStatModel _playerStat;
    private SeasonModel _season;
    public PlayerStatViewModel(PlayerStatModel playerStat, SeasonModel season)
    {
        _playerStat = playerStat;
        _season = season;
    }

    public string Season => $"{_season.Start_season}-{_season.End_season} {_season.Year}";
    public string Fullname => $"{_playerStat.Lastname} {_playerStat.Firstname} {_playerStat.Middlename}";
    public int Games => _playerStat.Games;
    public int Goals => _playerStat.Goals;
    public int PenaltyGoals => _playerStat.PenaltyGoals;
    public float AverageGoals
    {
        get
        {
            if (_playerStat.Games != 0)
            {
                return _playerStat.Goals / _playerStat.Games;
            }

            return 0;
        }
    }
    public int Assists => _playerStat.Assists;
    public float AverageAssists
    {
        get
        {
            if (_playerStat.Games != 0)
            {
                return _playerStat.Assists / _playerStat.Games;
            }

            return 0;
        }
    }
    public int YellowCards => _playerStat.YellowCards;
    public int RedCards => _playerStat.RedCards;
    public int ManOfTheMatch => _playerStat.ManOfTheMatch;
}