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


    private void ChangeDirection()
    {
        _currenPosition = new Vector2Int((int) transform.position.x, (int) transform.position.y);
        
        if (Input.GetKey(KeyCode.W)) {
            _direction = Vector2Int.up;
        }
        else if (Input.GetKey(KeyCode.S)) {
            _direction = Vector2Int.down;
        }
        if (Input.GetKey(KeyCode.D)) {
            _direction = Vector2Int.right;
        }
        else if (Input.GetKey(KeyCode.A)) {
            _direction = Vector2Int.left;
        }
        _newPosition = _currenPosition + _direction;
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
        StartCoroutine(AutomaticMovement());
    }
    
    void Start()
    {
        positions = spawner.GridPositionsList;
    }
    
    void Update()
    {
       ChangeDirection();
       
    }
}
