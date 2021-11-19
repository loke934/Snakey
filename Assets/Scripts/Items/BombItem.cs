using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class BombItem : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out SnakeyBodyBehaviour snakeyBodyBehaviour))
            {
                snakeyBodyBehaviour.RemoveBodyFromIndex();
                Destroy(gameObject);
            }
        }
    }
}

