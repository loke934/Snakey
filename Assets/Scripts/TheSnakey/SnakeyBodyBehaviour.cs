using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snakey
{
    [RequireComponent(typeof(AutomaticSnakey))]
    [RequireComponent(typeof(PlayerInput))]
    public class SnakeyBodyBehaviour : MonoBehaviour
    {
        [Header("Prefabs and references")]
        [SerializeField] 
        private GameObject snakeyBodyPartPrefab;
        [SerializeField] 
        private EatableItemBehaviour eatableItemBehaviour;
        [SerializeField] 
        private CreateGrid createGrid;
        
        private LinkedList<Transform> snakeyBodyLL = new LinkedList<Transform>();
        private PlayerInput playerInput;
        private AutomaticSnakey automaticSnakey;
        private bool isManualMovement;
        private bool isAutomaticMovement;
        
        public bool IsManualMovement
        {
            set => isManualMovement = value;
        }

        public bool IsAutomaticMovement
        {
            set => isAutomaticMovement = value;
        }

        private void Awake()
        {
            ExecuteMovementSettings();
            GetComponentInChildren<SnakeyCollision>().OnGameOver += ResetBody;
            eatableItemBehaviour.OnItemEaten += GrowBody;
        }

        private void Update()
        {
            //ChangeCellTypeAfterBody();
        }

        private void ChangeCellTypeAfterBody()
        {
            GridCell[,] array = createGrid.Grid.GridArray;
            if (snakeyBodyLL.Count <= 0)
            {
                return;
            }
            LinkedList<Transform>.ListNode currentNode = snakeyBodyLL.Head;
            while (currentNode != null)
            {
                Vector3 previousPos = currentNode.nodeItem.position;
                int x = 0;
                int y = 0;
                if (transform.position != previousPos)
                {
                    x = Mathf.Abs((int) previousPos.x);
                    y = Math.Abs((int) previousPos.y);
                    array[x,y].CellType = CellType.Walkable;
                    x = Mathf.Abs((int) currentNode.nodeItem.position.x);
                    y = Math.Abs((int) currentNode.nodeItem.position.y);
                    array[x,y].CellType = CellType.Obstacle;
                }
                currentNode = currentNode.previousNode;
            }
        }

        private void ExecuteMovementSettings()
        {
            if (isManualMovement)
            {
                playerInput = GetComponent<PlayerInput>();
                playerInput.OnMovement += MoveBody;
            }
            else if (isAutomaticMovement)
            {
                automaticSnakey = GetComponent<AutomaticSnakey>();
                automaticSnakey.OnMovement += MoveBody;
            }
            else
            {
                isManualMovement = true;
                isAutomaticMovement = false;
                playerInput = GetComponent<PlayerInput>();
                playerInput.OnMovement += MoveBody;
            } 
        }
        
        public bool IsPositionOccupied(Vector3 position)
        {
            if (transform.position == position)
            {
                return true;
            }
            List<Transform> list = snakeyBodyLL.GetAllAfterIndex();
            foreach (Transform node in list)
            {
                if (node.position == position)
                {
                    return true;
                }
            }
            return false;
        }
        
        public void RemoveBodyFromIndex()
        {
            int index = Random.Range(0, snakeyBodyLL.Count);
            List<Transform> removeList = snakeyBodyLL.GetAllAfterIndex(index);
            foreach (Transform thing in removeList)
            {
                Destroy(thing.gameObject);
            }
            snakeyBodyLL.RemoveAllFrom(index);
        }
        private void MoveBody(Vector3 snakeHeadPosition)
        {
            int x = 0;
            int y = 0;
            GridCell[,] array = createGrid.Grid.GridArray;
            if (snakeyBodyLL.Count <= 0)
            {
                return;
            }
            
            LinkedList<Transform>.ListNode currentNode = snakeyBodyLL.Tail;
            while (currentNode != null)
            {
                x = Mathf.Abs((int) currentNode.nodeItem.position.x);
                y = Math.Abs((int) currentNode.nodeItem.position.y);
                array[x,y].CellType = CellType.Walkable;
                if (currentNode == snakeyBodyLL.Head)
                {
                    currentNode.nodeItem.position = snakeHeadPosition;
                }
                else
                {
                    currentNode.nodeItem.position = currentNode.previousNode.nodeItem.position;
                }
                x = Mathf.Abs((int) currentNode.nodeItem.position.x);
                y = Math.Abs((int) currentNode.nodeItem.position.y);
                array[x,y].CellType = CellType.Obstacle;
                currentNode = currentNode.previousNode;
            }
            // Debug.Log("Snek head: " + transform.position);
            // Debug.Log("Grid cell " + array[Mathf.Abs((int)transform.position.x),Mathf.Abs((int)transform.position.y)].CellType);
        }
    
        public void GrowBody()
        {
            Vector3 position = GetTailPosition();
            GameObject bodyPart = Instantiate(snakeyBodyPartPrefab, position, Quaternion.identity);
            snakeyBodyLL.Add(bodyPart.transform);
        }

        private Vector3 GetTailPosition()
        {
            if (snakeyBodyLL.Count <= 0)
            {
                Direction currentDirection;
                Vector3 position;
                Vector3 sneakyHead = transform.position;
                if (isManualMovement)
                {
                    currentDirection = playerInput.CurrentDirection;
                }
                else
                {
                    currentDirection = automaticSnakey.CurrentDirection;
                }
                switch (currentDirection)
                {
                    case Direction.up:
                        position = new Vector3(sneakyHead.x, sneakyHead.y -1, 0f);
                        break;
                    case Direction.right:
                        position = new Vector3(sneakyHead.x -1 , sneakyHead.y, 0f);
                        break;
                    case Direction.down:
                        position = new Vector3(sneakyHead.x, sneakyHead.y + 1, 0f);
                        break;
                    case Direction.left:
                        position = new Vector3(sneakyHead.x + 1 , sneakyHead.y, 0f);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return position;
            }
            return snakeyBodyLL.Tail.nodeItem.position;
        }
    
        public void ResetBody()
        {
            snakeyBodyLL.Clear();
        }
    }
}

