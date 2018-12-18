using System;
using UnityEngine;

using Assets.Scripts.Grids;
using Assets.Scripts.Teams;
using Assets.Scripts.Tiles;
using Assets.Scripts.Players;
using Assets.Scripts.AudioManagement;

public class Player : MonoBehaviour, IDropper
{
    public Transform TileHoldPoint;
    public SoundsManager soundsManager;

    private Tile GrabbableTile;
    private GameObject GrabbableTileGo;

    private Tile HeldTile;
    private GameObject HeldTileGo;

    private Cell CellUnderneath;
    private GameObject CellUnderneathGo;

    public ITeam Team
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    private void Start()
    {
        GrabbableTile = null;
        GrabbableTileGo = null;

        HeldTile = null;
        HeldTileGo = null;

        CellUnderneath = null;
        CellUnderneathGo = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tile")
        {
            Tile tile = other.GetComponent<Tile>();
            if (tile.State == TileState.Dropped)
            {
                GrabbableTile = tile;
                GrabbableTileGo = other.gameObject;
                Debug.Log("Tile " + GrabbableTileGo.name + " is grabable");
            }
        }

        // might need to check a raycast instead of collider
        if (other.tag == "Cell")
        {
            Cell cell = other.GetComponent<Cell>();
            if (cell.Empty)
            {
                CellUnderneath = cell;
                CellUnderneathGo = other.gameObject;
                Debug.Log("On top of empty cell " + CellUnderneathGo.name);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (GrabbableTileGo == other.gameObject)
        {
            Debug.Log("Tile " + GrabbableTileGo.name + " is no longer grabable");
            GrabbableTile = null;
            GrabbableTileGo = null;
        }

        if (CellUnderneathGo == other.gameObject)
        {
            CellUnderneath = null;
            CellUnderneathGo = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (GrabbableTileGo != null)
            Gizmos.DrawLine(transform.position, GrabbableTileGo.transform.position);

        Gizmos.color = Color.red;

        if (CellUnderneathGo != null)
            Gizmos.DrawLine(transform.position, CellUnderneathGo.transform.position);
    }

    void FixedUpdate()
    {
        if (HeldTileGo != null)
        {
            HeldTileGo.transform.position = TileHoldPoint.position;
        }
    }

    public void GrabDropAction()
    {
        if (HeldTile != null && CellUnderneath != null)
        {
            UnstickOfPlayer();
            soundsManager.Play(SoundName.DropTile);
        }

        if (HeldTile == null && GrabbableTile)
        {
            StickTileToPlayer();
            soundsManager.Play(SoundName.TakeTile);
        }
    }

    private void StickTileToPlayer()
    {
        GrabbableTile.Hold(TileHoldPoint);
        HeldTile = GrabbableTile;
        HeldTileGo = GrabbableTileGo;
        GrabbableTile = null;
        GrabbableTileGo = null;
    }

    private void UnstickOfPlayer()
    {
        HeldTile.Drop(this);
        HeldTile = null;
        HeldTileGo = null;
    }
}