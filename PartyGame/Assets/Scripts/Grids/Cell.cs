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

        public bool Empty
        {
            get
            {
                return _tile == null;
            }
        }

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
        }

        public void Clear()
        {
            if (_tile != null)
            {
                Destroy(_tile.gameObject);
            }
        }

        public override string ToString()
        {
            return "[" + this.name + " Row " + Row + " Col " + Column + "]";
        }
    }
}