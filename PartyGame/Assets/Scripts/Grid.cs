using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Grid : MonoBehaviour
    {
        private const int DefaultPosition = -1;
        private const int Size = 4;

        private readonly Tile[,] _internalGrid = new Tile[Size,Size];

        public List<Cell> Cells { get; private set; }

        void Start()
        {
            LoadCells();
        }

        private void DebugGrid()
        {
            var builder = new StringBuilder();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var tile = _internalGrid[i, j];
                    builder.Append(tile?.Type ?? TileType.Empty);
                    builder.Append(" | ");
                }

                builder.AppendLine();
                builder.AppendLine("--------------------------------------");

            }

            Debug.Log(builder.ToString());
        }


        private void LoadCells()
        {
            Cells = GetComponentsInChildren<Cell>().ToList();

            if (Cells.Count != Size * Size)
            {
                throw new ArgumentException($"{nameof(Grid)} has {Cells.Count} cells instead of {Size * Size}.");
            }

            for (int i = 0; i < Cells.Count; i++)
            {
                var currentCell = Cells[i];

                for (int j = i + 1; j < Cells.Count; j++)
                {
                    var otherCell = Cells[j];

                    if (currentCell.Row == otherCell.Row && currentCell.Column == otherCell.Column)
                    {
                        throw new ArgumentException($"The {nameof(Grid)} has incorrect cells: {string.Join(", ", Cells)}.");
                    }
                }
            }
        }
        
        /// <summary>
        /// When a player drop a tile on the grid, return the number of scored points
        /// </summary>
        public int DropTile(Tile tile)
        {
            if (!TryGetCoordinates(tile, out var row, out var column))
            {
                return 0;
            }

            _internalGrid[row, column] = tile;
            DebugGrid();

            // TODO check who wins
            return 0;
        }

        private bool TryGetCoordinates(Tile tile, out int row, out int column)
        {
            row = DefaultPosition;
            column = DefaultPosition;
            var correspondingCell = this.Cells.FirstOrDefault(c => c.Collider.bounds.Contains(tile.transform.position));
            if (correspondingCell == null)
            {
                return false;
            }

            row = correspondingCell.Row;
            column = correspondingCell.Column;

            return true;
        }
        
        /// <summary>
        /// Clear a tile on the grid
        /// </summary>
        private void ClearCellAt(int row, int column)
        {
            if (!CoordinatesAreValid(row, column))
            {
                return;
            }

            // TODO
        }

        private bool CoordinatesAreValid(int row, int column)
        {
            var areValid = row > 0 && row < Size && column > 0 && column < Size;
            if (!areValid)
            {
                Debug.LogError($"Cell at 'row {row} column {column}' is out of bound");
            }

            return areValid;
        }
    }
}
