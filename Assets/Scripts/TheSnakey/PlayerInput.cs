using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snakey
{
    public enum Direction
    {
        up,
        right,
        down,
        left
    }
    public class PlayerInput : MonoBehaviour
    {
        [Header("Speed options")]
        [SerializeField, Range(0.1f, 1f)] 
        private float currentSpeed = 0.7f;
        [SerializeField, Range(0.01f, 0.04f)] 
        private float speedIncrease = 0.02f;
        
        [Header("References")]
        [SerializeField] 
        private CreateGrid createGrid;
        [SerializeField] 
        private EatableItemBehaviour eatableItemBehaviour;
        
        private Direction currentDirection;
        private Vector2Int currentGridCell;
        private Grid grid;
        private int distToGridEdge = 2;
        private float maxSpeed = 0.1f;
        private bool gameOver;
        
        public event Action<Vector3> OnMovement;
        public Direction CurrentDirection => currentDirection;

        private void Awake()
        {
            GetComponent<SnakeyCollision>().OnGameOver += GameOver;
            eatableItemBehaviour.OnItemEaten += IncreaseSpeed;
        }

        private void Start()
        {
            grid = createGrid.Grid;
            SetRandomStartPosition();
            StartCoroutine(ContinuousMovement());
        }

        private void Update()
        {
            if (!gameOver)
            {
                SetDirection();
                SetRotation();
            }
        }
        private void SetDirection()
        {
            if (Input.GetKeyDown(KeyCode.W) && currentDirection != Direction.down)
            {
                currentDirection = Direction.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) && currentDirection != Direction.up)
            {
                currentDirection = Direction.down;
            }
            if (Input.GetKeyDown(KeyCode.D) && currentDirection != Direction.left)
            {
                currentDirection = Direction.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) && currentDirection != Direction.right)
            {
                currentDirection = Direction.left;
            }
        }
        
        private void SetRotation()
        {
            Quaternion rotation;
            switch (currentDirection)
            {
                case Direction.up:
                    rotation = Quaternion.AngleAxis(90f, Vector3.forward);
                    break;
                case Direction.right:
                    rotation = Quaternion.AngleAxis(0f, Vector3.forward);
                    break;
                case Direction.down:
                    rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
                    break;
                case Direction.left:
                    rotation = Quaternion.AngleAxis(180f, Vector3.forward);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            transform.rotation = rotation;
        }

        private Vector2Int FindNextCell()
        {
            Vector2Int nextCell;
            switch (currentDirection)
            {
                case Direction.up:
                    nextCell = new Vector2Int(currentGridCell.x, currentGridCell.y + 1);
                    break;
                case Direction.right:
                    nextCell = new Vector2Int(currentGridCell.x + 1, currentGridCell.y);
                    break;
                case Direction.down:
                    nextCell = new Vector2Int(currentGridCell.x, currentGridCell.y - 1);
                    break;
                case Direction.left:
                    nextCell = new Vector2Int(currentGridCell.x - 1, currentGridCell.y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (!grid.CheckIfInsideGrid(nextCell.x, nextCell.y)) 
            {
                nextCell = FindNextCellWithWrap();
            }
            return nextCell;
        }

        private Vector2Int FindNextCellWithWrap()//Todo make more clean have same switch on may places
        {
            Vector2Int nextCell;
            switch (currentDirection)
            {
                case Direction.up:
                    nextCell = new Vector2Int(currentGridCell.x, 0);
                    break;
                case Direction.right:
                    nextCell = new Vector2Int(0, currentGridCell.y);
                    break;
                case Direction.down:
                    nextCell = new Vector2Int(currentGridCell.x, grid.SizeY -1);
                    break;
                case Direction.left:
                    nextCell = new Vector2Int(grid.SizeX -1, currentGridCell.y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return nextCell;
        }

        private IEnumerator ContinuousMovement()
        {
            while (!gameOver)
            {
                yield return new WaitForSeconds(currentSpeed);
                Vector2Int nextCell = FindNextCell();
                Vector3 previousPosition = transform.position;
                currentGridCell = nextCell;
                if (!gameOver)
                {
                    transform.position = grid[nextCell.x, nextCell.y].WorldPositionOfCell;  
                }
                OnMovement?.Invoke(previousPosition);
            }
        }

        public void IncreaseSpeed()
        {
            if (currentSpeed > maxSpeed)
            {
                currentSpeed -= speedIncrease;
            }
        }

        public void GameOver()
        {
            gameOver = true;
        }

        /// <summary>
        /// Set random start position on non-obstacle tile and not alongside grid edge.
        /// </summary>
        private void SetRandomStartPosition()
        {
            currentGridCell = new Vector2Int(Random.Range(distToGridEdge, grid.SizeX -distToGridEdge),
             Random.Range(distToGridEdge, grid.SizeY -distToGridEdge));
            while (grid.IsCellObstacle(currentGridCell))
            { 
                currentGridCell = new Vector2Int(Random.Range(distToGridEdge, grid.SizeX -distToGridEdge),
                 Random.Range(distToGridEdge, grid.SizeY -distToGridEdge));
            }
            transform.position = grid[currentGridCell.x, currentGridCell.y].WorldPositionOfCell;
        }
    }
}