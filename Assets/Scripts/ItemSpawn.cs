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
        private LevelController levelController;
        [SerializeField] 
        private PlayerInput playerInput;

        private Grid Grid => levelController.Grid;

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

            // int index = Random.Range(0, positionsList.Count - 1);
            // Vector2Int spawnPosition = positionsList[index];
            // GameObject itemInScene = Instantiate(eatableItemPrefab, (Vector2)spawnPosition, Quaternion.identity);
            // itemInScene.transform.SetParent(transform);
            // itemInScene.GetComponent<ItemBehaviour>().OnItemEaten += SpawnEatableItem;
            // playerInput.IncreaseSpeed();
        }
        void Start()
        {
            SpawnEatableItem();
        }
    }
}
