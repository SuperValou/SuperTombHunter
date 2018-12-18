using System;
using UnityEngine;

using Assets.Scripts.Grids;
using Assets.Scripts.Teams;
using Assets.Scripts.Tiles;
using Assets.Scripts.Players;
using Assets.Scripts.AudioManagement;
using Grid = Assets.Scripts.Grids.Grid;

public class Player : MonoBehaviour, IDropper
{
    public Transform heldTileLocation;
    public Grid grid;
    public SoundsManager soundsManager;

    private Tile _grabbableTile;
    
    private Tile _heldTile;
    
    public ITeam Team { get; set; }

    void Start()
    {
        if (grid == null)
        {
            Debug.LogError("No grid attached to " + nameof(Player));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_heldTile == null && other.tag == "Tile")
        {
            Tile tile = other.GetComponent<Tile>();
            if (tile.State == TileState.Grabbable)
            {
                _grabbableTile = tile;
                Debug.Log($"Tile {tile} is grabable");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == _grabbableTile.gameObject)
        {
            Debug.Log($"Tile {_grabbableTile} is not grabable anymore");
            _grabbableTile = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (_grabbableTile != null)
            Gizmos.DrawLine(transform.position, _grabbableTile.transform.position);
    }

    void FixedUpdate()
    {
        if (_grabbableTile != null)
        {
            _grabbableTile.gameObject.transform.position = heldTileLocation.position;
        }
    }

    public void GrabDropAction()
    {
        if (_heldTile == null && _grabbableTile != null)
        {
            Grab();
            soundsManager.Play(SoundName.DropTile);
        }

        if (_heldTile != null && grid.CanDropHere(this.transform.position))
        {
            Drop();
            soundsManager.Play(SoundName.TakeTile);
        }
    }

    private void Grab()
    {
        _grabbableTile.Hold(heldTileLocation);
        _heldTile = _grabbableTile;
        _grabbableTile = null;
    }

    private void Drop()
    {
        _heldTile.transform.position = this.transform.position; // put the tile at the feet of the player
        _heldTile.Drop(this);
        _heldTile = null;
    }
}