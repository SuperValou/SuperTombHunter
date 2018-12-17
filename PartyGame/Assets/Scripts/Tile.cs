using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tile : MonoBehaviour, ITile
    {
        private bool _isInit = false;

        private Grid _grid;

        public int Row { get; private set; }

        public int Column { get; private set; }

        public TileType Type { get; private set; } = TileType.Empty;

        public bool CanBeHolded { get; private set; }

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

        public void Drop()
        {
            if (State == TileState.Dropped)
            {
                Debug.LogError($"'{this}' is already {TileState.Dropped}, cannot {nameof(Drop)} it.");
                return;
            }

            State = TileState.Dropped;
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
            Drop();
        }
    }
}