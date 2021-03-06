﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tiles;
using Assets.Scripts.AudioManagement;
using UnityEngine;

namespace Assets.Scripts.Grids
{
    public class Grid : MonoBehaviour
    {
        private const int DefaultPosition = -1;
        private const int Size = 4;

        private readonly TileType[,] _internalGrid = new TileType[Size,Size];

        public SoundsManager _soundsManager;

        private readonly Dictionary<int, Dictionary<int, Cell>> _cells = new Dictionary<int, Dictionary<int, Cell>>();

        public bool Enabled { get; set; } = true;

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
                    var tileType = _internalGrid[i, j];
                    builder.Append(tileType);
                    builder.Append(" | ");
                }

                builder.AppendLine();
                builder.AppendLine("--------------------------------------");

            }

            Debug.Log(builder.ToString());
        }


        private void LoadCells()
        {
            var cells = GetComponentsInChildren<Cell>().ToList();

            if (cells.Count != Size * Size)
            {
                throw new ArgumentException($"{nameof(Grid)} has {cells.Count} cells instead of {Size * Size}.");
            }

            foreach (var cell in cells)
            {
                if (!_cells.ContainsKey(cell.Row))
                {
                    _cells.Add(cell.Row, new Dictionary<int, Cell>());
                }

                var row = _cells[cell.Row];

                if (row.ContainsKey(cell.Column))
                {
                    DebugGrid();
                    throw new ArgumentException($"The {nameof(Grid)} already contains a cell at row {cell.Row} col {cell.Column}.");
                }

                row[cell.Column] = cell;
            }
        }
        
        public bool CanDropHere(Vector3 position)
        {
            int row;
            int column;

            if (Enabled && TryGetCoordinates(position, out row, out column))
            {
                return _internalGrid[row, column] == TileType.Empty;
            }

            return false;
        }

        private bool TryGetCoordinates(Vector3 position, out int row, out int column)
        {
            row = DefaultPosition;
            column = DefaultPosition;

            var allCells = _cells.Values.SelectMany(v => v.Values).ToList();
            Cell correspondingCell = null;
            foreach (var cell in allCells)
            {
                if (cell.Collider.bounds.Contains(position))
                {
                    correspondingCell = cell;
                    break;
                }
            }

            if (correspondingCell == null)
            {
                return false;
            }

            if (!CoordinatesAreValid(correspondingCell.Row, correspondingCell.Column))
            {
                return false;
            }

            row = correspondingCell.Row;
            column = correspondingCell.Column;

            return true;
        }

        private bool CoordinatesAreValid(int row, int column)
        {
            var areValid = row >= 0 && row < Size && column >= 0 && column < Size;
            if (!areValid)
            {
                Debug.LogError($"Cell at 'row {row} column {column}' is out of bound");
            }

            return areValid;
        }
        
        /// <summary>
        /// When a player drop a tile on the grid, return the number of scored points
        /// </summary>
        public int DropTile(Tile tile, Vector3 _dropPosition)
        {
            if (!Enabled)
            {
                return -1;
            }

            int row;
            int column;
            if (!TryGetCoordinates(_dropPosition, out row, out column))
            {
                return -1;
            }

            if (_internalGrid[row, column] != TileType.Empty) return -1;

            Debug.Log($"{tile.Type} dropping at row {row} col {column}");

            if (tile.Type == TileType.Empty)
            {
                Debug.LogWarning("Is it normal that the tile is Empty?");
            }

            _internalGrid[row, column] = tile.Type;
            _cells[row][column].SetTile(tile);

            var completedLines = UpdateGrid();
            DebugGrid();

            if (completedLines > 0)
            {
                _soundsManager.Play(SoundName.ScorePoint);
            }

            int scoredPoints = completedLines == 0 ? 1 : completedLines * 4;
            return scoredPoints;
        }

        private int UpdateGrid()
        {
            List<Tuple<int,int>> cellToClear = new List<Tuple<int, int>>();

            // Slash diagonal, from down left to up right /
            var downLeftType = _internalGrid[0, 0];
            if (downLeftType != TileType.Empty)
            {
                bool slashDiagIsFull = true;

                for (int i = 1; i < Size; i++)
                {
                    slashDiagIsFull &= downLeftType == _internalGrid[i, i];
                    if (!slashDiagIsFull)
                    {
                        break;
                    }
                }

                if (slashDiagIsFull)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        cellToClear.Add(new Tuple<int, int>(i, i));
                    }
                }
            }

            // Antislash diagonal, from up left to down right \
            var upLeftType = _internalGrid[0, Size - 1];
            if (upLeftType != TileType.Empty)
            {
                bool antislashDiagIsFull = true;

                for (int i = 1; i < Size; i++)
                {
                    antislashDiagIsFull &= upLeftType == _internalGrid[i, Size - 1 - i];
                    if (!antislashDiagIsFull)
                    {
                        break;
                    }
                }

                if (antislashDiagIsFull)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        cellToClear.Add(new Tuple<int, int>(i, Size - 1 - i));
                    }
                }
            }

            // Rows --
            for (int rowIndex = 0; rowIndex < Size; rowIndex++)
            {
                var leftType = _internalGrid[rowIndex, 0];
                if (leftType != TileType.Empty)
                {
                    bool lineIsFull = true;
                    for (int j = 1; j < Size; j++)
                    {
                        lineIsFull &= leftType == _internalGrid[rowIndex, j];
                        if (!lineIsFull)
                        {
                            break;
                        }
                    }

                    if (lineIsFull)
                    {
                        for (int j = 0; j < Size; j++)
                        {
                            cellToClear.Add(new Tuple<int, int>(rowIndex, j));
                        }
                    }
                }
            }

            // Columns |
            for (int columnIndex = 0; columnIndex < Size; columnIndex++)
            {
                var topType = _internalGrid[0, columnIndex];
                if (topType != TileType.Empty)
                {
                    bool columnIsFull = true;
                    for (int i = 1; i < Size; i++)
                    {
                        columnIsFull &= topType == _internalGrid[i, columnIndex];
                        if (!columnIsFull)
                        {
                            break;
                        }
                    }

                    if (columnIsFull)
                    {
                        for (int i = 0; i < Size; i++)
                        {
                            cellToClear.Add(new Tuple<int, int>(i, columnIndex));
                        }
                    }
                }
            }

            // clear scoring tiles
            foreach (var tuple in cellToClear)
            {
                ClearCellAt(tuple.Item1, tuple.Item2);
            }

            return cellToClear.Count / Size;
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
            
            _cells[row][column].StartClear();
        }
        
        public void DestroyComplete(int row, int column)
        {
            if (!CoordinatesAreValid(row, column))
            {
                return;
            }
            
            _internalGrid[row, column] = TileType.Empty;
        }

        void Update()
        {
            if (GridFull()) EmptyGrid();
        }

        private bool GridFull()
        {
            foreach (var tile in _internalGrid)
            {
                if (tile == TileType.Empty) return false;
            }

            return true;
        }

        private void EmptyGrid()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                _cells[i][i].StartClear();
                _cells[i][Size - 1 - i].StartClear();
            }
        }
    }
}
