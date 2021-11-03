using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeyCollision : MonoBehaviour
{
    [SerializeField] 
    private Canvas canvas;
    public static event Action OnGameOver;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Snake"))
        {
            OnGameOver?.Invoke();
            canvas.gameObject.SetActive(true);
        }
    }
    //Todo FIX COLLISION WITH WALLS, MAKE SO OUTSIDE OF GRID OR COLLIDE WITH WALLS BUT HOW TO AVOID...
}
