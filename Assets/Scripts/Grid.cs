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
        private GridCell[,] gridArray;
        
        public GridCell this[int x, int y] =>  gridArray[x, y];
        public int SizeX => sizeX;
        public int SizeY => sizeY;

        public Grid(int x, int y, int size)
        {
            sizeX = x;
            sizeY = y;
            cellSize = size;
            CreateGrid();
        }
        
        private void CreateGrid()
        {
            gridArray = new GridCell[sizeX, sizeY];
            
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = new GridCell(x * cellSize, y * cellSize);
                }
            }
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
