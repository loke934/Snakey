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
        private float destroyInSeconds = 5f;
        
        [Header("Prefabs and references")]
        [SerializeField] 
        private GameObject eatableItemPrefab;
        [SerializeField] 
        private GameObject bombItemPrefab;
        [SerializeField] 
        private CreateGrid createGrid;
        [SerializeField] 
        private AutomaticSnakey snakey;
        [SerializeField] 
        private SnakeyBodyBehaviour snakeyBodyBehaviour;
        [SerializeField]
        private GameObject eatableItem;

        private Vector2Int itemPosition;
        
        private Grid grid => createGrid.Grid;
        private bool isManualMovement;
        private bool isAutomaticMovement;
        private int itemCount = 0;
        
        public Vector2Int ItemPosition => itemPosition;
        public bool IsManualMovement
        {
            set => isManualMovement = value;
        }

        public bool IsAutomaticMovement
        {
            get => isAutomaticMovement;
            set => isAutomaticMovement = value;
        }

        public void SpawnEatableItem()
        {
            ChangeItemPosition();
            eatableItem.GetComponent<EatableItemBehaviour>().OnItemEaten += ChangeItemPosition;
            if (isAutomaticMovement)
            {
                eatableItem.GetComponent<EatableItemBehaviour>().OnItemEaten += snakey.FillPositionStack;
            }
        }
        /// <summary>
        /// Returns random position that is not an obstacle tile or occupied by snakey body.
        /// </summary>
        /// <returns>Vector3 position</returns>
        private Vector3 GetRandomPosition()
        {
            Vector2Int cell = new Vector2Int(Random.Range(0, grid.SizeX), Random.Range(0, grid.SizeY));
            Vector3 position = grid[cell.x, cell.y].WorldPositionOfCell;
            while(grid.IsCellObstacle(cell))
            {
                cell = new Vector2Int(Random.Range(0, grid.SizeX), Random.Range(0, grid.SizeY));
            }

            if (snakeyBodyBehaviour.IsPositionOccupied(position))
            {
                cell = new Vector2Int(Random.Range(0, grid.SizeX), Random.Range(0, grid.SizeY));
            }
            itemPosition = cell;
            return grid[cell.x, cell.y].WorldPositionOfCell;
        }
   
        public void ChangeItemPosition()
        {
            eatableItem.transform.position = GetRandomPosition();
            if (isManualMovement)
            {
                if (itemCount == bombInterval) 
                {
                    SpawnBomb();
                    itemCount = 0;
                }
                itemCount++;
            }
        }

        private void SpawnBomb()
        {
            Vector3 position = GetRandomPosition();
            GameObject bomb = Instantiate(bombItemPrefab, (Vector2)position, Quaternion.identity);
            bomb.transform.SetParent(transform);
            StartCoroutine(DestroyAfterSeconds(bomb));
        }

        private IEnumerator DestroyAfterSeconds(GameObject item)
        {
            yield return new WaitForSeconds(destroyInSeconds);
            Destroy(item);
        }
    }
}
