using System.Diagnostics;
using Assets.Scripts.Teams;
using UnityEngine;
using UnityEngine.SceneManagement;
using Grid = Assets.Scripts.Grids.Grid;

namespace Assets.Scripts.UI
{
    public class GlobalTimer : MonoBehaviour
    {
        public int maxTime = 90;

        private int _currentTime;

        public UiBar uiBar;
        public Grid grid;
        public TeamManager teamManager;

        private Stopwatch _stopwatch = new Stopwatch();
        
        void Start()
        {
            _currentTime = maxTime;
            _stopwatch.Start();
        }
        
        void Update()
        {
            int timeToDisplay = maxTime - (int) _stopwatch.Elapsed.TotalSeconds;

            if (timeToDisplay <= 0)
            {
                timeToDisplay = 0;
                grid.Enabled = false;
                teamManager.SetWinner();
                Invoke(nameof(LoadVictoryScreen), 2);
            }

            if (_currentTime != timeToDisplay)
            {
                uiBar.DisplaySeconds(timeToDisplay);
                _currentTime = timeToDisplay;
            }
        }

        public void LoadVictoryScreen()
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

        public void Restart()
        {
            _stopwatch.Restart();
        }
    }
}