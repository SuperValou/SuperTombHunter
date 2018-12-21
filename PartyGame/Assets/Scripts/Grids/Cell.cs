using System;
using Assets.Scripts.Tiles;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Grids
{
    public class Cell : MonoBehaviour
    {
        public int Row;

        public int Column;
        private Tile _tile;

        public Collider2D Collider { get; private set; }
        
        void Start()
        {
            Collider = this.GetComponent<Collider2D>();
            if (Collider == null)
            {
                Debug.LogError($"No {nameof(UnityEngine.Collider)} is attached to {this}.");
            }
        }

        public void SetTile(Tile tile)
        {
            if (tile == null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            if (_tile != null)
            {
                Debug.LogError($"Cannot set {tile.Type} at row {Row} column {Column} because it is already set to {tile.Type}");
                return;
            }

            _tile = tile;
            _tile.BeOnCell(this);
        }

        public void StartClear()
        {
            if (_tile == null) Debug.LogError($"Tile is null on cell [{Row},{Column}]. Cannot clear !");
            if (_tile != null)
            {
                _tile.StartDestroy();
            }
        }

        public override string ToString()
        {
            return $"[{this.name} Row {Row} Column {Column}]";
        }
    }
}