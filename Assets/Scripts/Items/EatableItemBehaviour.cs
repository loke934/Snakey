using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class EatableItemBehaviour : MonoBehaviour
    {
        public event Action OnItemEaten;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<SnakeyBodyBehaviour>(out SnakeyBodyBehaviour snakeyBodyBehaviour))
            {
                other.gameObject.TryGetComponent<PlayerInput>(out var playerInput); //How to do this in a better way?
                playerInput.IncreaseSpeed();
                snakeyBodyBehaviour.GrowBody();
                OnItemEaten?.Invoke();
            }
        }
    }
}

