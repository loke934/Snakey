using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<Vector3> OnMovement;
    public enum Direction
    {
        up,
        right,
        down,
        left
    }
    private Direction currentDirection;
    
    private Vector2Int currenPosition;
    private Vector2Int newPosition;
    private List<Vector2Int> positions;
    private SpawnGrid gridSpawner;

    private float currentSpeed = 0.7f;
    private float maxSpeed = 0.1f;

    private bool gameOver = false;

    private Quaternion GetRotation()
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
        return rotation;
    }
    private Vector2Int DirectionToVector2Int(Direction dir)
    {
        switch (dir)
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
        currenPosition = new Vector2Int((int) transform.position.x, (int) transform.position.y);
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
        transform.rotation = GetRotation();
    }
    private IEnumerator AutomaticMovement()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(currentSpeed);
            
            newPosition = currenPosition + DirectionToVector2Int(currentDirection);
            if (positions.Contains(newPosition))
            {
                Vector3 previousPos = transform.position;
                OnMovement?.Invoke(previousPos);
                transform.position = new Vector2(newPosition.x, newPosition.y);
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
    private void Awake()
    {
        gridSpawner = FindObjectOfType<SpawnGrid>().GetComponent<SpawnGrid>();
        StartCoroutine(AutomaticMovement());
    }
    
    void Start()
    {
        positions = gridSpawner.GridPositionsList;
    }

    private void Update()
    {
        if (!gameOver)
        {
            SetDirection();
        }
    }
    
}
