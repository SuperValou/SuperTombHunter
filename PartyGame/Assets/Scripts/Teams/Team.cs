using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Teams
{
    public class Team : MonoBehaviour, ITeam
    {
        public TeamSide TeamSide;

        public Text ScoreLabel;

        public int Score { get; private set; }

        public void ScorePoints(int points)
        {
            Score += points;
            ScoreLabel.text = Score.ToString();
        }

        public void ResetScore()
        {
            Score = 0;
            ScoreLabel.text = Score.ToString();
        }
    }
}