using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeyCollision : MonoBehaviour
{
    public UnityEvent OnGameOver;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("col");
        if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall");
            OnGameOver.Invoke();
        }
        
        if (other.gameObject.CompareTag("Snake"))
        {
            Debug.Log("Snake");
            OnGameOver.Invoke();
        }
    }

    private void Awake()
    {
        OnGameOver = new UnityEvent();
        //Add listener to event
    }
}
