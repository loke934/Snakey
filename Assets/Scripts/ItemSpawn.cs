using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField]private SpawnGrid _gridSpawner;

    private List<Vector2Int> _positions;

    public void ItemEaten()
    {
        SpawnItem();
    }
    private void SpawnItem()
    {
        _positions = _gridSpawner.ItemsCanSpawnPositionList;
        int index = Random.Range(0, _positions.Count-1);
        Vector2Int pos = _positions[index];
        Vector2 spawnPosition = new Vector2(pos.x, pos.y);
        Instantiate(item, spawnPosition,Quaternion.identity).transform.SetParent(transform);
        
    }
    void Start()
    {
       SpawnItem();
    }

}
