using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snakey
{
    public class AutomaticSnakey : MonoBehaviour
    {
        [Header("Speed options")]
        [SerializeField, Range(0.1f, 1f)] 
        private float currentSpeed = 0.4f;
        
        [Header("References")]
        [SerializeField] 
        private CreateGrid createGrid;
        [SerializeField] 
        private CreateGraph graphCreator;
        [SerializeField] 
        private ItemSpawn itemSpawn;

        private Direction currentDirection = Direction.right;
        private Vector2Int currentGridCell;
        private Vector2 up;
        private Vector2 down;
        private Vector2 right;
        private Vector2 left;
        private Graph graph;
        private Vertex[,] verticesArray;
        private Stack<GridCell> positionsStack;
        private int distToGridEdge = 2;
        private bool gameOver;
        public event Action<Vector3> OnMovement;
        
        public Direction CurrentDirection => currentDirection;
        
        public void StartSnakeyMovement()
        {
            graph = graphCreator.Graph;
            verticesArray = graphCreator.VerticesArray;
            up = new Vector2(0f,1f);
            down = new Vector2(0f,-1f);
            right = new Vector2(1f,0f);
            left = new Vector2(-1f,0f);
            SetRandomStartPosition();
            FillPositionStack();
            StartCoroutine(AutomaticMovement());
            GetComponent<SnakeyCollision>().OnGameOver += GameOver;
        }
        
        /// <summary>
        /// Set random start position on non-obstacle tile and not alongside grid edge.
        /// </summary>
        private void SetRandomStartPosition()
        {
            Grid grid = createGrid.Grid;
            int x = Random.Range(distToGridEdge, grid.SizeX - distToGridEdge);
            int y = Random.Range(distToGridEdge, grid.SizeY - distToGridEdge);
            currentGridCell = new Vector2Int(x, y);
            
            while (grid.IsCellObstacle(currentGridCell))
            { 
                x = Random.Range(distToGridEdge, grid.SizeX - distToGridEdge);
                y = Random.Range(distToGridEdge, grid.SizeY - distToGridEdge);
                currentGridCell = new Vector2Int(x, y);
            }
            transform.position = grid[currentGridCell.x, currentGridCell.y].WorldPositionOfCell;
        }
        
        public void FillPositionStack()
        {
            Vector2Int targetPosition = itemSpawn.ItemPosition;
            positionsStack = graph.Pathfinding(verticesArray, currentGridCell, targetPosition, currentDirection);
            currentGridCell = targetPosition;
        }
        
        private IEnumerator AutomaticMovement()
        {
            while (!gameOver)
            {
                yield return new WaitForSeconds(currentSpeed);
                Vector3 previousPosition = transform.position;
                GridCell nextCell = positionsStack.Pop();
                //Debug.Log(nextCell.CellType);
                SetDirection(nextCell);
                if (!gameOver)
                {
                    SetRotation();
                    transform.position = nextCell.WorldPositionOfCell;  
                }
                OnMovement?.Invoke(previousPosition);
            }
        }

        private void SetDirection(GridCell cell)
        {
            Vector2 direction = cell.WorldPositionOfCell - transform.position;

            if (direction == up)
            {
                currentDirection = Direction.up;
            }
            if (direction == down)
            {
                currentDirection = Direction.down;
            }
            if (direction == right)
            {
                currentDirection = Direction.right;
            }
            if (direction == left)
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

        public void GameOver()
        {
            gameOver = true;
        }
    }
}

