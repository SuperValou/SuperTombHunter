using Assets.Scripts;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private const int Size = 4;

    private readonly TileType[,] _internalGrid = new TileType[Size,Size];
    
    void Start()
    {
        ClearGrid();
    }
    
    void Update()
    {
        // TODO check who wins
    }
    
    /// <summary>
    /// When a player drop a tile on the grid
    /// </summary>
    public void SetTileAt(ITile tile)
    {
        if (!TileIsValid(tile))
        {
            return;
        }

        _internalGrid[tile.Row, tile.Column] = tile.Type;
    }

    /// <summary>
    /// Reset the grid
    /// </summary>
    public void ClearGrid()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                _internalGrid[i, j] = TileType.Empty;
            }
        }
    }

    /// <summary>
    /// Clear a tile on the grid
    /// </summary>
    private void ClearTileAt(int row, int column)
    {
        if (!CoordinatesAreValid(row, column))
        {
            return;
        }

        _internalGrid[row, column] = TileType.Empty;
    }

    private bool CoordinatesAreValid(int row, int column)
    {
        var areValid = row > 0 && row < Size && column > 0 && column < Size;
        if (!areValid)
        {
            Debug.LogError($"Tile at 'row {row} column {column}' is out of bound");
        }

        return areValid;
    }

    private bool TileIsValid(ITile tile)
    {
        if (tile == null)
        {
            Debug.LogError("Tile was null!");
            return false;
        }

        if (CoordinatesAreValid(tile.Row, tile.Column))
        {
            return true;
        }

        Debug.LogError($"{tile} is out of bound");
        return false;
    }

}
