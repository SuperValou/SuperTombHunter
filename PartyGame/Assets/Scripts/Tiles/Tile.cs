using System;
using Assets.Scripts.Players;
using Assets.Scripts.Tests;
using UnityEngine;
using Grid = Assets.Scripts.Grids.Grid;

namespace Assets.Scripts.Tiles
{
    public class Tile : MonoBehaviour
    {
        public TileType Type;

        private bool _isInit = false;
        private Grid _grid;
        private readonly int _row;
        private readonly int _column;

        public TileState State { get; private set; }
        
        void Update()
        {
            if (!_isInit)
            {
                Debug.LogError($"{this} is not initialized!");
            }
        }

        public void Initialize(Grid grid)
        {
            if (_isInit)
            {
                throw new InvalidOperationException($"{this} is already initialized.");
            }

            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            State = TileState.Grabbable;
            _isInit = true;
        }

        public void Hold(Transform newParent)
        {
            if (State == TileState.Held)
            {
                Debug.LogError($"'{this}' is already {TileState.Held}, cannot {nameof(Hold)} it.");
                return;
            }

            State = TileState.Held;
        }

        public void Drop(IDropper dropper)
        {
            if (State == TileState.Grabbable)
            {
                Debug.LogError($"'{this}' is already {TileState.Grabbable}, cannot {nameof(Drop)} it.");
                return;
            }

            State = TileState.Grabbable;

            var scoredPoint = _grid.DropTile(this);
            if (scoredPoint == 0)
            {
                return;
            }

            dropper.Team.ScorePoints(scoredPoint);
        }
        
        void OnMouseDown()
        {
            Hold(gameObject.transform.parent);
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