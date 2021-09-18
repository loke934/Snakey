using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemBehaviour : MonoBehaviour
{
    public UnityEvent OnItemEaten = new UnityEvent();

    private ItemSpawn _itemSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnItemEaten.Invoke();
        Destroy(gameObject);
    }
    
    private void Start()
    {
        _itemSpawner = FindObjectOfType<ItemSpawn>();
        OnItemEaten.AddListener(_itemSpawner.ItemEaten);
    }
}
