using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class TileSpawnSlot : MonoBehaviour
    {
        private Collider2D _collider2D;

        public Transform SpawnLocation;

        public bool IsAvailable => SpawnedTile == null;

        public Tile SpawnedTile { get; private set; }

        void Start()
        {
            _collider2D = this.GetComponent<Collider2D>();
            if (_collider2D == null)
            {
                Debug.LogError("No collider on timespawnslot!");
                return;
            }

            if (!_collider2D.isTrigger)
            {
                Debug.LogError("Collider on timespawnslot is not a Trigger");
                return;
            }
        }

        void Update()
        {
            if (SpawnedTile == null)
            {
                return;
            }

            if (_collider2D.bounds.Contains(SpawnedTile.transform.position))
            {
                return;
            }

            Debug.Log($"{SpawnedTile} left {this}");
            SpawnedTile = null;
        }

        public void SetTile(Tile tile)
        {
            Debug.Log("Oh, a tile: " + tile);
            SpawnedTile = tile;
        }
    }
}
