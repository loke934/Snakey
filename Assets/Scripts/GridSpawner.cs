using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Snakey
{
    public class GridSpawner : MonoBehaviour
    {
        [Header("Grid options")] 
        [SerializeField, Range(5f, 20)]
        private int gridSizeX = 15;
        [SerializeField, Range(5f, 20)] 
        private int gridSizeY = 15;
        [SerializeField] 
        private GameObject groundPrefab;
        [SerializeField] 
        private GameObject wallPrefab;
        [Header("Load level from text options")]
        [SerializeField] 
        private TextAsset level;
        [SerializeField] 
        private int copyFromIndex = 2;

        private int cellSize = 1;
        private Grid grid;
        private List<GameObject> tileList; //Keep? See if I want to use later for some function?//2d array if keep

        public Grid Grid => grid;

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
                    tileList.Add(tile);
                }
            }
        }

        private void Awake()
        {
            grid = new Grid(level, copyFromIndex, cellSize);
            //grid = new Grid(gridSizeX, gridSizeY, cellSize);
            tileList = new List<GameObject>();
            MakeGridVisual();
        }
    }
}