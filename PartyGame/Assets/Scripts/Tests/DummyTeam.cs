using System;
using Assets.Scripts.Teams;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tests
{
    public class DummyTeam : ITeam
    {
        public int Score { get; private set; } = 0;

        public void ScorePoints(int points)
        {
            Score += points;
            Debug.Log("Points: " + Score);
        }

        public void ResetScore()
        {
            throw new NotImplementedException();
        }
    }
}