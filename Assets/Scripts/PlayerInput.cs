using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Snakey;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Snakey
{
    public class PlayerInput : MonoBehaviour
    { 
        public enum Direction
        {
            up,
            right,
            down,
            left
        }
        private Direction currentDirection;
        
        [SerializeField] 
        private GridSpawner gridSpawner;
        [SerializeField, Range(0.1f, 1f)] 
        private float currentSpeed = 0.7f;

        private float maxSpeed = 0.1f;
        private bool gameOver;
        private Vector2Int currentGridCell;
        public event Action<Vector3> OnMovement;

        private Grid grid => gridSpawner.Grid; //Better to store it instead of getting it all the time?
        public Direction CurrentDirection => currentDirection;

        private void SetDirection()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentDirection = Direction.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentDirection = Direction.down;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentDirection = Direction.right;
            }
            else if (Input.GetKeyDown(KeyCode.A))
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
            return nextCell;
        }

        private IEnumerator AutomaticMovement()
        {
            while (!gameOver)
            {
                yield return new WaitForSeconds(currentSpeed);

                Vector2Int nextCell = FindNextCell();
                if (grid.CheckIfInsideGrid(nextCell.x, nextCell.y))
                {
                    Vector3 previousPosition = transform.position;
                    transform.position = grid[nextCell.x, nextCell.y].WorldPositionOfCell;
                    currentGridCell = nextCell;
                    OnMovement?.Invoke(previousPosition);
                }
                else
                {
                    GetComponentInChildren<SnakeyCollision>().GameOver(); //moving outside of grid
                }
            }
        }

        public void IncreaseSpeed()
        {
            if (currentSpeed > maxSpeed)
            {
                currentSpeed -= 0.02f;
            }
        }

        public void GameOver()
        {
            gameOver = true;
        }

        private void SetRandomStartPosition()
        {
            currentGridCell = new Vector2Int(Random.Range(1, grid.SizeX -1), Random.Range(1, grid.SizeY -1)); //Not to spawn at edge
            transform.position = grid[currentGridCell.x, currentGridCell.y].WorldPositionOfCell;
        }

        private void Awake()
        {
            GetComponentInChildren<SnakeyCollision>().OnGameOver += GameOver;
        }

        private void Start()
        {
            SetRandomStartPosition();
            StartCoroutine(AutomaticMovement());
        }

        private void Update()
        {
            if (!gameOver)
            {
                SetDirection();
                SetRotation();
            }
        }
    }
}