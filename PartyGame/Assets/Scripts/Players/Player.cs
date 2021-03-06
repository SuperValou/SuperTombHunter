﻿using System;
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
    private MoveController moveController;

    public Transform HeldTileLocation;
    public Transform DropTileLocation;

    private Tile _grabbableTile;
    
    public Tile _heldTile;
    
    public ITeam Team { get; set; }


    public void Initialize(Grid grid, SoundsManager soundManager)
    {
        _grid = grid;
        _soundsManager = soundManager;
    }

    void Awake()
    {
        moveController = GetComponent<MoveController>();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.collider;
        if (other.tag == "Player")
        {
            Player otherPlayer = other.GetComponent<Player>();
            if (moveController.IsDashing && _heldTile == null && otherPlayer._heldTile)
            {
                _heldTile = otherPlayer._heldTile;
                otherPlayer._heldTile = null;
            }
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
            var position = new Vector3(HeldTileLocation.position.x, HeldTileLocation.position.y, 0);
            _heldTile.gameObject.transform.position = position;
        }
    }

    public void GrabDropAction()
    {
        if (_heldTile == null && _grabbableTile != null && _grabbableTile.State == TileState.Grabbable)
        {
            Grab();
            return;
        }

        if (_heldTile != null && _grid.CanDropHere(_heldTile.transform.position))
        {
            Drop();
            return;
        }
    }

    private void Grab()
    {
        _grabbableTile.Hold(HeldTileLocation);
        _heldTile = _grabbableTile;
        _grabbableTile = null;
        _soundsManager.Play(SoundName.TakeTile);
    }

    private void Drop()
    {
        bool dropped = _heldTile.TryDrop(this);
        if (dropped)
        {
            _heldTile = null;
            _soundsManager.Play(SoundName.DropTile);
        }
    }
}