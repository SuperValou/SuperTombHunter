using Assets.Scripts.Players;
using Assets.Scripts.Teams;

namespace Assets.Scripts.Tests
{
    public class DummyDropper : IDropper
    {
        public ITeam Team { get; } = new DummyTeam();
    }
}