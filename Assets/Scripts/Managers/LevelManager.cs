using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] 
        private GridSpawner gridspawner;
        [SerializeField] 
        private CreateGraph createGraph;
        [SerializeField] 
        private ItemSpawn itemSpawn;
        [SerializeField] 
        private AutomaticSnakey automaticSneaky;

        private void Awake()
        {
            gridspawner.CreateGrid();
            createGraph.CreateTheGraph();
            createGraph.FillGraph(gridspawner.Grid);
            itemSpawn.SpawnEatableItem();
        }

        private void Start()
        {
            automaticSneaky.StartSnakeyMovement();
        }
    }

}
