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
        [SerializeField, Range(0f, 30f)] 
        private float destroyBombTime = 5f;
        
        [SerializeField] 
        private GameObject eatableItemPrefab;
        [SerializeField] 
        private GameObject bombItemPrefab;
        [SerializeField] 
        private GridSpawner gridSpawner;
        [SerializeField] 
        private AutomaticSnakey snakey;

        [SerializeField] 
        private SnakeyBodyBehaviour snakeyBodyBehaviour;

        private int itemCount = 0;

        private GameObject eatableItem;
        
        private Vector2Int itemSpawnPos;
        private Grid grid => gridSpawner.Grid;

        public Vector2Int ItemSpawnPos => itemSpawnPos;

        private Vector3 GetRandomPosition()
        {
            Vector2Int cell = new Vector2Int(Random.Range(0, grid.SizeX), Random.Range(0, grid.SizeY));
            while(grid.IsCellObstacle(cell))
            {
                cell = new Vector2Int(Random.Range(0, grid.SizeX), Random.Range(0, grid.SizeY));
            }

            if (snakeyBodyBehaviour.IsPositionOccupied(grid[cell.x, cell.y].WorldPositionOfCell))
            {
                cell = new Vector2Int(Random.Range(0, grid.SizeX), Random.Range(0, grid.SizeY));
            }
            itemSpawnPos = cell;
            return grid[cell.x, cell.y].WorldPositionOfCell;
        }
        
        public void SpawnEatableItem()
        {
            Vector3 position = GetRandomPosition();
            eatableItem = Instantiate(eatableItemPrefab, (Vector2)position, Quaternion.identity);
            eatableItem.transform.SetParent(transform);
            // if (itemCount == bombInterval) //Todo Fix bomb or remove in pathfinding 
            // {
            //     SpawnBomb();
            //     itemCount = 0;
            // }
            eatableItem.GetComponent<EatableItemBehaviour>().OnItemEaten += ChangeItemPosition;
            eatableItem.GetComponent<EatableItemBehaviour>().OnItemEaten += snakey.FillPositionStack;
            itemCount++;
        }

        public void ChangeItemPosition()
        {
            eatableItem.transform.position = GetRandomPosition();
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

    }
}
