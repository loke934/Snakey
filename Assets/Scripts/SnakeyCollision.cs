using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Snakey
{
    public class SnakeyCollision : MonoBehaviour
    {
        [SerializeField] 
        private Canvas canvas;

        private bool isGameOver;
        public event Action OnGameOver;

        public void GameOver()
        {
            OnGameOver?.Invoke();
            canvas.gameObject.SetActive(true);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Snake"))
            {
                GameOver();
            }
        }
    }
}
