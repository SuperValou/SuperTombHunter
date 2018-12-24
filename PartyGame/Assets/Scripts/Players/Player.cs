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

    public Transform _heldTileLocation;

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

    void Update()
    {
        if (_heldTile != null)
        {
            var position = new Vector3(_heldTileLocation.position.x, _heldTileLocation.position.y, 0);
            _heldTile.gameObject.transform.position = position;
        }
    }

    public void GrabDropAction()
    {
        if (_heldTile == null && _grabbableTile != null)
        {
            Grab();
            _soundsManager.Play(SoundName.TakeTile);
            return;
        }

        if (_heldTile != null && _grid.CanDropHere(_heldTile.transform.position))
        {
            Drop();
            _soundsManager.Play(SoundName.DropTile);
            return;
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
        bool dropped = _heldTile.TryDrop(this);
        if (dropped) _heldTile = null;
    }
}