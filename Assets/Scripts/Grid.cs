using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class Grid
    {
        private int sizeX;
        private int sizeY;
        private int cellSize;
        private float halfGridX;
        private float halfGridY;
        private GridCell[,] gridArray;
        private Dictionary<char, CellType> cellTypeLookUp = new Dictionary<char, CellType>
        {
            {'X', CellType.Obstacle},
            {'_', CellType.Walkable}
        };

        public GridCell this[int x, int y] => gridArray[x, y];
        public int SizeX => sizeX;
        public int SizeY => sizeY;

        public Grid(TextAsset levelTextAsset, int fromSourceIndex, int size)
        {
            string[] levelText = levelTextAsset.text.Split(
                new string[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None
            );
            sizeX = int.Parse(levelText[0]);
            sizeY = int.Parse(levelText[1]);
            cellSize = size;
            halfGridX = (sizeX * 0.5f) * size;
            halfGridY = (sizeY * 0.5f) * size;
            string[] typesOfCell = new string [levelText.Length - 2];
            Array.Copy(levelText, fromSourceIndex, 
                typesOfCell, 0, levelText.Length - 2);
            CreateGrid(typesOfCell);
        }

        public Grid(int x, int y, int size)
        {
            sizeX = x;
            sizeY = y;
            halfGridX = (x * 0.5f) * size;
            halfGridY = (y * 0.5f) * size;
            cellSize = size;
            CreateGrid();
        }

        private void CreateGrid(string[] cellTypes = null)
        {
            gridArray = new GridCell[sizeX, sizeY];
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    //CellType type = cellTypes == null ? CellType.Walkable : GetCellType(cellTypes[x][y]); Readable?
                    CellType type; 
                    if (cellTypes == null)
                    {
                        type = CellType.Walkable;
                    }
                    else
                    {
                        type = GetCellType(cellTypes[(cellTypes.Length- 1) -y][x]); // make into variable how did this become a jagged array?
                    }
                    gridArray[x, y] = new GridCell((x * cellSize) - halfGridX, (y * cellSize) - halfGridY, type);
                }
            }
        }

        private CellType GetCellType(char letter)
        {
            if (cellTypeLookUp.TryGetValue(letter, out CellType type))
            {
                return type;
            }
            throw new Exception($"Invalid cell type {letter}");
        }
        
        public bool IsCellObstacle(Vector2Int cell)
        {
            CellType cellType = gridArray[cell.x, cell.y].CellType;
            if (cellType == CellType.Obstacle)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfInsideGrid(int x, int y)
        {
            if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
            {
                return false;
            }
            return true;
        }
    }
}