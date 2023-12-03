using FootballStats.Models;

namespace FootballStats.ViewModels;

public class SeasonViewModel : ViewModelBase
{
    private SeasonModel _season;
    public SeasonViewModel(SeasonModel season)
    {
        _season = season;
    }

    public string Name => $"{_season.Start_season}-{_season.End_season} {_season.Year}";
}