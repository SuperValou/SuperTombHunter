using System;
using Assets.Scripts.Players;
using Assets.Scripts.Tests;
using UnityEngine;
using Grid = Assets.Scripts.Grids.Grid;

namespace Assets.Scripts.Tiles
{
    public class Tile : MonoBehaviour
    {
        private bool _isInit = false;

        private Grid _grid;
        private readonly int _row;
        private readonly int _column;

        public TileType Type { get; private set; } = TileType.Empty;
        
        public TileState State { get; private set; }
        
        void Update()
        {
            if (!_isInit)
            {
                Debug.LogError($"{this} is not initialized!");
            }
        }

        public void Initialize(Grid grid, TileType tileType)
        {
            if (_isInit)
            {
                throw new InvalidOperationException($"{this} is already initialized.");
            }

            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            Type = tileType;
            State = TileState.Dropped;
            _isInit = true;
        }

        public void Hold()
        {
            if (State == TileState.Holded)
            {
                Debug.LogError($"'{this}' is already {TileState.Holded}, cannot {nameof(Hold)} it.");
                return;
            }

            State = TileState.Holded;
        }

        public void Drop(IDropper dropper)
        {
            if (State == TileState.Dropped)
            {
                Debug.LogError($"'{this}' is already {TileState.Dropped}, cannot {nameof(Drop)} it.");
                return;
            }

            State = TileState.Dropped;

            var scoredPoint = _grid.DropTile(this);
        }
        
        void OnMouseDown()
        {
            Hold();
        }

        void OnMouseDrag()
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(pos.x, pos.y, this.transform.position.z);
        }

        void OnMouseUpAsButton()
        {
            Drop(new DummyDropper());
        }
    }
}