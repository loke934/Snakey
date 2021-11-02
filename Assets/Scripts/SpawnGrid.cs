using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class SpawnGrid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField, Range(5f, 20)] private int gridSizeX = 15;
    [SerializeField, Range(5f, 20)] private int gridSizeY = 15;

    private List<Vector2Int> _gridPositionsList;

    public List<Vector2Int> GridPositionsList
    {
        get
        {
            return _gridPositionsList;
        }
    }

    private void SpawnTheGrid()
    {
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                GameObject tileToSpawn;
                if (x  == 0 || x == gridSizeX -1 || y == 0 || y == gridSizeY -1 ) //edges of grid should have walls
                {
                    tileToSpawn = wallPrefab;
                }
                else
                {
                    tileToSpawn = tilePrefab;
                }
                Vector2 spawnPosition = new Vector2((int)-gridSizeX/2 + x, (int)-gridSizeY/2 + y);
                GameObject tile = Instantiate(tileToSpawn, spawnPosition, Quaternion.identity);
                tile.transform.SetParent(transform);

                Vector2Int positionToAdd = new Vector2Int((int)-gridSizeX/2 + x,(int)-gridSizeY/2 + y);//use above how??
                if (tileToSpawn == tilePrefab)
                {
                    _gridPositionsList.Add(positionToAdd);
                }
            }
        }
    }
    private void Awake()
    {
        _gridPositionsList = new List<Vector2Int>();
        SpawnTheGrid();
    }

}
