using Assets.Scripts.Tiles;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Tile grabbableTile;
    private GameObject grabbableTileGo;
    private bool HoldingTile;

    private void Start()
    {
        grabbableTile = null;
        grabbableTileGo = null;
        HoldingTile = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tile")
        {
            Tile tile = other.GetComponent<Tile>();
            if (tile.State == TileState.Dropped)
            {
                grabbableTile = tile;
                grabbableTileGo = other.gameObject;
                Debug.Log("Tile " + grabbableTileGo.name + " is grabable");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (grabbableTileGo == other.gameObject)
        {
            Debug.Log("Tile " + grabbableTileGo.name + " is no longer grabable");
            grabbableTile = null;
            grabbableTileGo = null;
        }
    }
    public void GrabNearestTile()
    {
        if (HoldingTile) return;

        if (grabbableTile)
        {
            grabbableTile.Hold();
            StickTileToPlayer();
        }
    }

    private void StickTileToPlayer()
    {
        grabbableTileGo.transform.parent = gameObject.transform;
    }
}