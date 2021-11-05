using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snakey
{
    public class ItemSpawn : MonoBehaviour
    {
        [SerializeField] 
        private GameObject eatableItemPrefab;
        [SerializeField] 
        private GridSpawner gridSpawner;

        private Grid Grid => gridSpawner.Grid;

        private Vector3 GetRandomPosition()
        {
            Vector2Int cell = new Vector2Int(Random.Range(0, Grid.SizeX), Random.Range(0, Grid.SizeY));
            return Grid[cell.x, cell.y].WorldPositionOfCell;
        }
        public void SpawnEatableItem()
        {
            Vector3 position = GetRandomPosition();
            GameObject item = Instantiate(eatableItemPrefab, (Vector2)position, Quaternion.identity);
            item.transform.SetParent(transform);
            item.GetComponent<ItemBehaviour>().OnItemEaten += SpawnEatableItem;
        }
        void Start()
        {
            SpawnEatableItem();
        }
    }
}
