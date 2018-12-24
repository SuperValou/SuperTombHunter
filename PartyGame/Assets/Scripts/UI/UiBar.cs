using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.Teams;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UiBar : MonoBehaviour
    {
        public DoubleDigitsDisplay _timerDisplay;
        public DoubleDigitsDisplay _hotScoreDisplay;
        public DoubleDigitsDisplay _coldScoreDisplay;

        void Start()
        {
            _hotScoreDisplay.UpdateValue(0);
            _coldScoreDisplay.UpdateValue(0);
        }

        public void SetScore(int score, TeamSide side)
        {
            switch (side)
            {
                case TeamSide.Cold:
                    _coldScoreDisplay.UpdateValue(score);
                    break;

                case TeamSide.Hot:
                    _hotScoreDisplay.UpdateValue(score);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        public void DisplaySeconds(int secondsToDisplay)
        {
            _timerDisplay.UpdateValue(secondsToDisplay);
        }
    }
}