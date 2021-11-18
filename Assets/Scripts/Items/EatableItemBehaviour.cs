using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class EatableItemBehaviour : MonoBehaviour
    {
        private const string snakeTag= "Snake";
        public event Action OnItemEaten;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(snakeTag))
            {
                OnItemEaten?.Invoke();
            }
        }
    }
}

