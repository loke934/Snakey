using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2Int _direction;
    private Vector2Int _currenPosition;
    private Vector2Int _newPosition;
    
    private SpawnGrid spawner;
    private List<Vector2Int> positions;

    private Queue<Vector2Int> _directionsQueue;
    private Vector2Int _currentDirection;

    private void SetDirection()
    {
        _currenPosition = new Vector2Int((int) transform.position.x, (int) transform.position.y);
        
        if (Input.GetKey(KeyCode.W)) {
            _direction = Vector2Int.up;
            _directionsQueue.Enqueue(_direction);
        }
        else if (Input.GetKey(KeyCode.S)) {
            _direction = Vector2Int.down;
            _directionsQueue.Enqueue(_direction);
        }
        if (Input.GetKey(KeyCode.D)) {
            _direction = Vector2Int.right;
            _directionsQueue.Enqueue(_direction);
        }
        else if (Input.GetKey(KeyCode.A)) {
            _direction = Vector2Int.left;
            _directionsQueue.Enqueue(_direction);
        }
        if (_directionsQueue.Count == 0)
        { 
            _directionsQueue.Enqueue(_direction);
        }
        //Problem that it skip dir if current dir changes 2 times in update but movement not happened
        _currentDirection = _directionsQueue.Dequeue();
        _newPosition = _currenPosition + _currentDirection;
    }
    private IEnumerator AutomaticMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);//Make variable conf in inspector
            
            if (positions.Contains(_newPosition))
            {
                transform.position = new Vector2(_newPosition.x, _newPosition.y);
            }
        }
    }
    private void Awake()
    {
        spawner = FindObjectOfType<SpawnGrid>().GetComponent<SpawnGrid>();
        _directionsQueue = new Queue<Vector2Int>();
        _direction = Vector2Int.right;
        _directionsQueue.Enqueue(_direction);
        StartCoroutine(AutomaticMovement());
    }
    
    void Start()
    {
        positions = spawner.GridPositionsList;
        
    }
    void FixedUpdate()
    {
       SetDirection();
       
    }
}
