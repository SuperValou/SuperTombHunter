using Assets.Scripts.Teams;

namespace Assets.Scripts.Tests
{
    public class DummyTeam : ITeam
    {
        public int Score { get; } = 0;

        public void ScorePoints(int points)
        {
        }

        public void ResetScore()
        {
        }
    }
}