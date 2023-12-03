using FootballStats.Models;
using ReactiveUI;

namespace FootballStats.ViewModels;

public class PlayerViewModel : ViewModelBase
{
    private PlayerModel _player;
    public PlayerViewModel(PlayerModel player)
    {
        _player = player;
    }

    public int Id
    {
        get => _player.Id;
        set
        {
            _player.Id = value;
            this.RaisePropertyChanged();
        }
    }

    public string Fullname => _player.Fullname;
}