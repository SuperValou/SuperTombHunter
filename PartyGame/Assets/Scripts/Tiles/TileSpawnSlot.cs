using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class TileSpawnSlot : MonoBehaviour
    {
        public bool IsAvailable => SpawnedTile == null;

        public Tile SpawnedTile { get; private set; }

        void Start()
        {
            var col2D = this.GetComponent<Collider2D>();
            if (col2D == null)
            {
                Debug.LogError("No collider on timespawnslot!");
                return;
            }

            if (!col2D.isTrigger)
            {
                Debug.LogError("Collider on timespawnslot is not a Trigger");
                return;
            }
        }
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag != "Tile")
            {
                return;
            }

            Tile tile = other.GetComponent<Tile>();
            
            if (SpawnedTile == tile)
            {
                SpawnedTile = null;
            }
        }

        public void SetTile(Tile tile)
        {
            SpawnedTile = tile;
        }
    }
}
