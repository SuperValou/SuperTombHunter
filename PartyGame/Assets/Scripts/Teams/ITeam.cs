namespace Assets.Scripts.Teams
{
    public interface ITeam
    {
        int Score { get; }

        void ScorePoints(int points);

        void ResetScore();
    }
}