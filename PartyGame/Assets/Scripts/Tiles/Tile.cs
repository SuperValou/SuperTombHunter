using System;
using UnityEngine;
using Assets.Scripts.Grids;
using Assets.Scripts.Players;
using Assets.Scripts.Tests;
using Grid = Assets.Scripts.Grids.Grid;

namespace Assets.Scripts.Tiles
{
    public class Tile : MonoBehaviour
    {
        public TileType Type;

        private bool _isInit = false;
        private Grid _grid;

        private int _row;
        private int _column;

        public TileState State { get; private set; }

        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
        void Update()
        {
            if (!_isInit)
            {
                Debug.LogError($"{this} is not initialized!");
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Destroyed"))
            {
                _grid.DestroyComplete(_row, _column);
                Destroy(gameObject);
            }
        }

        public void Initialize(Grid grid)
        {
            if (_isInit)
            {
                throw new InvalidOperationException($"{this} is already initialized.");
            }

            if (grid == null) throw new ArgumentNullException(nameof(grid));
            else _grid = grid;
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

        public bool TryDrop(Player dropper)
        {
            if (State == TileState.Grabbable)
            {
                Debug.LogError($"'{this}' is already {TileState.Grabbable}, cannot {nameof(Drop)} it.");
                return false;
            }

            var scoredPoint = _grid.DropTile(this, dropper.DropTileLocation.position);
            if (scoredPoint == -1)
            {
                return false;
            }

            Drop();

            if (scoredPoint > 0)
            {
                dropper.Team.ScorePoints(scoredPoint);
            }

            return true;
        }

        private void Drop()
        {
            State = TileState.Sealed;

            var sprite = this.GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                sprite.sortingLayerName = "TileOnFloor";
            }
        }

        public void BeOnCell(Cell _cell)
        {
            transform.position = _cell.transform.position;
            _row = _cell.Row;
            _column = _cell.Column;
        }

        public void StartDestroy()
        {
            animator.SetBool("destroyed", true);
        }
    }
}