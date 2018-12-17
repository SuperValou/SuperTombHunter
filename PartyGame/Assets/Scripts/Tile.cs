using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Tile : MonoBehaviour, ITile
{
    private Grid _grid;

    public void SetGrid(Grid grid)
    {

    }

    public int Row { get; }

    public int Column { get; }

    public TileType Type { get; set; }

    public TileState State { get; set; }

    public bool CanBeHolded { get; set; }
}
