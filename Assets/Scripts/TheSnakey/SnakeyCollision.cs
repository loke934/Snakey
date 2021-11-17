using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class SnakeyCollision : MonoBehaviour
    {
        [SerializeField] 
        private Canvas canvas;
        private bool isGameOver;
        private const string snakeTag= "Snake";
        private const string wallTag= "Wall";
        public event Action OnGameOver;

        private void GameOver()
        {
            OnGameOver?.Invoke();
            canvas.gameObject.SetActive(true);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(snakeTag) || other.gameObject.CompareTag(wallTag))
            {
                GameOver();
            }
        }
    }
}
