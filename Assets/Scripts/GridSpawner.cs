using System;
using System.Collections;
using System.Collections.Generic;
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
        private GameObject tilePrefab;
        
        private int cellSize = 1;
        private Grid grid;
        
        private List<GameObject> tileList; //Keep? See if I want to use later for some function?//2d array if keep

        public Grid Grid => grid;

        private void MakeGridVisual()
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int x = 0; x < gridSizeX; x++)
                {
                    Vector3 spawnPosition = grid[x, y].WorldPositionOfCell;
                    GameObject tile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                    tile.transform.SetParent(transform);
                    tileList.Add(tile);
                }
            }
        }
        private void Awake()
        {
            grid = new Grid(gridSizeX, gridSizeY, cellSize);
            tileList = new List<GameObject>();
            MakeGridVisual();
        }
    }
}

