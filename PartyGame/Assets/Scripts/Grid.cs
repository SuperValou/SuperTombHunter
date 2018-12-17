using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Grid : MonoBehaviour
    {
        private const int Size = 4;

        private readonly Tile[,] _internalGrid = new Tile[Size,Size];

        public List<Cell> Cells { get; private set; }

        void Start()
        {
            LoadCells();
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

        void Update()
        {
            // TODO check who wins
        }
    
        /// <summary>
        /// When a player drop a tile on the grid
        /// </summary>
        public void SetTile(Tile tile)
        {
            if (!TryGetCoordinates(tile, out var row, out var column))
            {
                return;
            }

            _internalGrid[row, column] = tile;
        }

        private bool TryGetCoordinates(Tile tile, out int row, out int column)
        {
             // TODO
            throw new System.NotImplementedException();
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
