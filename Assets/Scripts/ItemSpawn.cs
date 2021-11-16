using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snakey
{
    public class ItemSpawn : MonoBehaviour
    {
        [Header("Bomb item options")]
        [SerializeField, Range(0, 10)] 
        private int bombInterval = 3;
        [SerializeField, Range(0f, 10f)] 
        private float destroyBombTime = 5f;
        
        [SerializeField] 
        private GameObject eatableItemPrefab;
        [SerializeField] 
        private GameObject bombItemPrefab;
        [SerializeField] 
        private GridSpawner gridSpawner;

        private int itemCount = 0;

        private Grid grid => gridSpawner.Grid;

        private Vector3 GetRandomPosition()
        {
            Vector2Int cell = new Vector2Int(Random.Range(0, grid.SizeX), Random.Range(0, grid.SizeY));
            while(grid.IsCellObstacle(cell))
            {
                cell = new Vector2Int(Random.Range(0, grid.SizeX), Random.Range(0, grid.SizeY));
            }
            return grid[cell.x, cell.y].WorldPositionOfCell;
        }
        
        public void SpawnEatableItem()
        {
            Vector3 position = GetRandomPosition();
            GameObject item = Instantiate(eatableItemPrefab, (Vector2)position, Quaternion.identity);
            item.transform.SetParent(transform);
            if (itemCount == bombInterval)
            {
                SpawnBomb();
                itemCount = 0;
            }
            item.GetComponent<ItemBehaviour>().OnItemEaten += SpawnEatableItem;
            itemCount++;
        }

        private void SpawnBomb()
        {
            Vector3 position = GetRandomPosition();
            GameObject bomb = Instantiate(bombItemPrefab, (Vector2)position, Quaternion.identity);
            bomb.transform.SetParent(transform);
            StartCoroutine(DestroyBombAfterSec(bomb));
        }

        private IEnumerator DestroyBombAfterSec(GameObject bomb)
        {
            yield return new WaitForSeconds(destroyBombTime);
            Destroy(bomb);
        }

        void Start()
        {
            SpawnEatableItem();
        }
    }
}
