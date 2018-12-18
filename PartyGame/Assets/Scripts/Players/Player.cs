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
    private Grid _grid;
    private SoundsManager _soundsManager;

    private Transform _heldTileLocation;

    private Tile _grabbableTile;
    
    private Tile _heldTile;
    
    public ITeam Team { get; set; }


    public void Initialize(Grid grid, SoundsManager soundManager)
    {
        _grid = grid;
        _soundsManager = soundManager;
    }

    void Start()
    {
        if (_grid == null)
        {
            Debug.LogError("No grid attached to " + nameof(Player));
        }

        _heldTileLocation = this.GetComponentInChildren<Transform>()?.transform;
        if (_heldTileLocation == null)
        {
            Debug.LogWarning("No Held point was found in the player. Falling back to player position!");
            _heldTileLocation = this.transform;
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
        if (_grabbableTile?.gameObject == null)
        {
            return;
        }

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
        if (_heldTile != null)
        {
            _heldTile.gameObject.transform.position = _heldTileLocation.position;
        }
    }

    public void GrabDropAction()
    {
        if (_heldTile == null && _grabbableTile != null)
        {
            Grab();
            _soundsManager.Play(SoundName.DropTile);
        }

        if (_heldTile != null && _grid.CanDropHere(this.transform.position))
        {
            Drop();
            _soundsManager.Play(SoundName.TakeTile);
        }
    }

    private void Grab()
    {
        _grabbableTile.Hold(_heldTileLocation);
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