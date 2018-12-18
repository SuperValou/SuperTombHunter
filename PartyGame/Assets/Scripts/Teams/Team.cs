using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Teams
{
    public class Team : MonoBehaviour, ITeam
    {
        public TeamSide TeamSide;

        public UiBar UiBar;

        public int Score { get; private set; }

        public void ScorePoints(int points)
        {
            Score += points;
            UiBar.SetScore(Score, TeamSide);
        }

        public void ResetScore()
        {
            Score = 0;
            UiBar.SetScore(Score, TeamSide);
        }
    }
}