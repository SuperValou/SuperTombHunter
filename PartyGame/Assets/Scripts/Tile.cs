using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tile : MonoBehaviour, ITile
    {
        private Grid _grid;

        public int Row { get; private set; }

        public int Column { get; private set; }

        public TileType Type { get; private set; } = TileType.Empty;

        public bool CanBeHolded { get; private set; }

        public TileState State { get; private set; }

        public void Initialize(Grid grid, TileType tileType)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            Type = tileType;
            State = TileState.Dropped;
        }

        public void Hold()
        {
            State = TileState.Holded;
        }

        private void Update()
        {
            if (_grid == null)
            {
                Debug.LogError("No grid assigned to the tile!");
            }
        }
    }
}