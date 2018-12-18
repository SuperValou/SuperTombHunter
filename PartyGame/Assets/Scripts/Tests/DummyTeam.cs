using System;
using Assets.Scripts.Teams;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tests
{
    public class DummyTeam : MonoBehaviour, ITeam
    {
        public Text ScoreDebugLabel;

        public int Score { get; private set; } = 0;

        public void ScorePoints(int points)
        {
            Score += points;
            ScoreDebugLabel.text = Score.ToString();
        }

        public void ResetScore()
        {
            throw new NotImplementedException();
        }
    }
}