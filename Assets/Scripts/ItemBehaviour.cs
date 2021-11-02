using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemBehaviour : MonoBehaviour
{
    public event Action OnItemEaten;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<AddSnakeyPart>(out var addSnakeyPart))
        {
            addSnakeyPart.Grow();
            OnItemEaten?.Invoke();
            Destroy(gameObject);
        }
    }
}
