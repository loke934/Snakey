using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class SpawnGrid : MonoBehaviour
{
    [SerializeField] 
    private GameObject tilePrefab;
    [SerializeField] 
    private GameObject wallPrefab;
    [SerializeField, Range(5f, 20)] 
    private int gridSizeX = 15;
    [SerializeField, Range(5f, 20)] 
    private int gridSizeY = 15;

    private List<Vector2Int> gridPositionsList;
    public List<Vector2Int> GridPositionsList => gridPositionsList;

    private void SpawnTheGrid()
    {
        int sizeX = -gridSizeX / 2;
        int sizeY = -gridSizeY / 2;
        
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                GameObject tileToSpawn;
                if (x  == 0 || x == gridSizeX -1 || y == 0 || y == gridSizeY -1 ) 
                {
                    tileToSpawn = wallPrefab;
                }
                else
                {
                    tileToSpawn = tilePrefab;
                }
                Vector2Int spawnPosition = new Vector2Int(sizeX + x, sizeY + y);
                GameObject tile = Instantiate(tileToSpawn, (Vector2)spawnPosition, Quaternion.identity);
                tile.transform.SetParent(transform);
                if (tileToSpawn == tilePrefab)
                {
                    gridPositionsList.Add(spawnPosition);
                }
            }
        }
    }
    private void Awake()
    {
        gridPositionsList = new List<Vector2Int>();
        SpawnTheGrid();
    }
    //Todo CHANGE AND REMOVE WALL AND MAKE SO DIE IF OUTSIDE GRID INSTEAD
    //Not use static event
    // When grid is created get the grids pos in world pos.For whole grid? Or per tile?
    // Gridsize but not create it. class grid info world pos of a cell.
    // Then when I want to move, I check with an if-statement if the place I want to move to is inside the grid. if its inside bounderies of pretend grid.
    //Data oriented approach. Devide from visuals. class pure c sharp class, data and functions for grid. grid to world search for cell to spawn fruite. 
    // Like transform.pos + transform.forward is inside grid? Return true or false.
    //Todo Method that give grid pos in world pos. Don´t need to check if pos is in list. Calc in runtime and return bool. checks through if statement. Have all info already, calc instead of save. Return new pos with coords.
}
