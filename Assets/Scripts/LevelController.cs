using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class LevelController : MonoBehaviour //Change name to grid spawner??
    {
        [Header("Grid options")]
        [SerializeField, Range(5f, 20)] 
        private int gridSizeX = 15;
        [SerializeField, Range(5f, 20)] 
        private int gridSizeY = 15;
        [SerializeField] 
        private GameObject tilePrefab;
        
        private int cellSize = 1;//Make serialized field for option?
        private Grid grid;
        private List<GameObject> tileList; //Keep? See if I want to use later

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

        // private void Awake()
        // {
        //     var renderer = tilePrefab.GetComponent<SpriteRenderer>();
        //     int cellSize = renderer.sprite.texture.height / (int)renderer.sprite.pixelsPerUnit;
        //     grid = new Grid(gridSizeX, gridSizeY, cellSize);
        //     tileList = new List<GameObject>();
        //
        //     for (int y = 0; y < gridSizeY; y++)
        //     {
        //         for (int x = 0; x < gridSizeX; x++)
        //         {
        //             Vector3 spawnPos = grid[x, y].WorldPositionOfCell;
        //             GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
        //             tile.name = $"X:{x} Y:{y}";
        //             tile.transform.SetParent(transform);
        //             tileList.Add(tile);
        //         }
        //     }
        //     
        // }
    }
}

