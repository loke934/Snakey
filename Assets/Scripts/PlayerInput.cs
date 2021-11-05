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
        private LevelController levelController;
        [SerializeField, Range(0.1f, 1f)] 
        private float currentSpeed = 0.7f;

        private float maxSpeed = 0.1f;
        private bool gameOver;
        private Vector2Int currentCell;
        
        //need this?
        private Vector2Int currenPosition;
        private List<Vector2Int> positionsList;
        
        public static event Action<Vector3> OnMovement;

        private Grid Grid => levelController.Grid; //Better to make a variable and store it instead of getting it all the time?
        
        private Vector2Int DirectionToPosition()
        {
            switch (currentDirection)
            {
                case Direction.up:
                    return Vector2Int.up;

                case Direction.right:
                    return Vector2Int.right;

                case Direction.down:
                    return Vector2Int.down;

                case Direction.left:
                    return Vector2Int.left;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

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
                    nextCell = new Vector2Int(currentCell.x, currentCell.y + 1);
                    break;
                case Direction.right:
                    nextCell = new Vector2Int(currentCell.x + 1, currentCell.y);
                    break;
                case Direction.down:
                    nextCell = new Vector2Int(currentCell.x, currentCell.y - 1);
                    break;
                case Direction.left:
                    nextCell = new Vector2Int(currentCell.x - 1, currentCell.y);
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
                if (Grid.CheckIfInsideGrid(nextCell.x, nextCell.y))
                {
                    Vector3 previousPosition = transform.position;
                    OnMovement?.Invoke(previousPosition);
                    transform.position = Grid[nextCell.x, nextCell.y].WorldPositionOfCell;
                    currentCell = nextCell;
                }
                else
                {
                    //Todo Invoke OnGameOver
                }
                // currenPosition = new Vector2Int((int) transform.position.x, (int) transform.position.y);
                // Vector2Int newPosition = currenPosition + DirectionToPosition();
                // if (positionsList.Contains(newPosition))
                // {
                //     Vector3 previousPos = transform.position;
                //     OnMovement?.Invoke(previousPos);
                //     transform.position = new Vector2(newPosition.x, newPosition.y);
                // }
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
            currentCell = new Vector2Int(Random.Range(0, Grid.SizeX), Random.Range(0, Grid.SizeY));
            transform.position = Grid[currentCell.x, currentCell.y].WorldPositionOfCell;
        }

        private void Awake()
        {
            SnakeyCollision.OnGameOver += GameOver;
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