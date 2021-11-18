using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Snakey
{
    public class CreateGrid : MonoBehaviour
    {
        [Header("Load level from text")]
        [SerializeField] 
        private TextAsset level;
        [SerializeField] 
        private int copyFromIndex = 2;
        [Header("Grid options")] 
        [SerializeField, Range(5f, 20)]
        private int gridSizeX = 15;
        [SerializeField, Range(5f, 15)] 
        private int gridSizeY = 12;
        [Header("Prefabs")] 
        [SerializeField] 
        private GameObject groundPrefab;
        [SerializeField] 
        private GameObject wallPrefab;

        private bool isTextLevel;
        private bool isInspectorLevel;
        private int cellSize = 1;
        private Grid grid;
        
        public Grid Grid => grid;
        public bool IsTextLevel
        {
            set => isTextLevel = value;
        }
        public bool IsInspectorLevel
        {
            set => isInspectorLevel = value;
        }

        public void CreateTheGrid()
        {
            if (isTextLevel)
            {
                grid = new Grid(level, copyFromIndex, cellSize);
            }
            else if(isInspectorLevel)
            {
                grid = new Grid(gridSizeX, gridSizeY, cellSize);
            }
            else
            {
                grid = new Grid(gridSizeX, gridSizeY, cellSize);
            }
            MakeGridVisual();
        }
        
        private void MakeGridVisual()
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    Vector3 spawnPosition = grid[x, y].WorldPositionOfCell;
                    GameObject cellPrefab = grid[x, y].CellType == CellType.Walkable ? groundPrefab : wallPrefab;
                    GameObject tile = Instantiate(cellPrefab, spawnPosition, Quaternion.identity);
                    tile.transform.SetParent(transform);
                }
            }
        }
    }
}