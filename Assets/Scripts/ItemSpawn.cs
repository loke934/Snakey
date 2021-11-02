using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] 
    private GameObject itemPrefab;
    [SerializeField] 
    private SpawnGrid gridSpawner;
    [SerializeField] 
    private PlayerInput playerInput;

    private List<Vector2Int> positions;
    private GameObject itemInScene;


    public void SpawnItem()
    {
        int index = Random.Range(0, positions.Count - 1);
        Vector2 spawnPosition = positions[index];
        itemInScene = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        itemInScene.transform.SetParent(transform);
        itemInScene.GetComponent<ItemBehaviour>().OnItemEaten += SpawnItem;
        playerInput.IncreaseSpeed();
    }

    void Start()
    {
        positions = gridSpawner.GridPositionsList;
        SpawnItem();
    }

    /*not working with unity events, spawning multiple or only once*/
}