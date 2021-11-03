using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] 
    private GameObject eatableItemPrefab;
    [SerializeField] 
    private SpawnGrid gridSpawner;
    [SerializeField] 
    private PlayerInput playerInput;

    private List<Vector2Int> positionsList;

    public void SpawnEatableItem()
    {
        int index = Random.Range(0, positionsList.Count - 1);
        Vector2Int spawnPosition = positionsList[index];
        GameObject itemInScene = Instantiate(eatableItemPrefab, (Vector2)spawnPosition, Quaternion.identity);
        itemInScene.transform.SetParent(transform);
        itemInScene.GetComponent<ItemBehaviour>().OnItemEaten += SpawnEatableItem;
        playerInput.IncreaseSpeed();
    }
    void Start()
    {
        positionsList = gridSpawner.GridPositionsList;
        SpawnEatableItem();
    }
}