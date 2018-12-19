using System.Diagnostics;
using UnityEngine;
using Grid = Assets.Scripts.Grids.Grid;

namespace Assets.Scripts.UI
{
    public class GlobalTimer : MonoBehaviour
    {
        public int maxTime = 90;

        public UiBar uiBar;
        public Grid grid;

        private Stopwatch _stopwatch = new Stopwatch();
        
        void Start()
        {
            _stopwatch.Start();
        }
        
        void Update()
        {
            int timeToDisplay = maxTime - (int) _stopwatch.Elapsed.TotalSeconds;

            if (timeToDisplay <= 0)
            {
                timeToDisplay = 0;
                grid.Enabled = false;
            }

            uiBar.DisplaySeconds(timeToDisplay);
        }

        public void Restart()
        {
            _stopwatch.Restart();
        }
    }
}