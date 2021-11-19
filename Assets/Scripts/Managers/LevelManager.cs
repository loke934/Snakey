using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snakey
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Level options")] 
        [SerializeField]
        private bool levelFromText;
        [SerializeField]
        private bool levelFromInspector;
        [Header("Movement options")] 
        [SerializeField]
        private bool manualMovement;
        [SerializeField]
        private bool automaticMovement;
        
        [Header("References")] 
        [SerializeField] 
        private CreateGrid createGrid;
        [SerializeField] 
        private CreateGraph createGraph;
        [SerializeField] 
        private ItemSpawn itemSpawn;
        [SerializeField] 
        private AutomaticSnakey automaticSneaky;
        [SerializeField] 
        private PlayerInput playerInput;
        [SerializeField] 
        private SnakeyBodyBehaviour snakeyBodyBehaviour;
        
        private void Awake()
        {
            ExecuteLevelSettings();
            ExecuteMovementSetting();
            SetUpLevel();
        }

        private void Start()
        {
            if (automaticMovement)
            {
                automaticSneaky.StartSnakeyMovement();
            }
        }
        
        private void ExecuteLevelSettings()
        {
            if (levelFromText)
            {
                createGrid.IsTextLevel = levelFromText;
            }
            else if (levelFromInspector)
            {
                createGrid.IsInspectorLevel = levelFromInspector;
            }
        }

        private void SetUpLevel()
        {
            createGrid.CreateTheGrid();
            createGraph.CreateTheGraph();
            createGraph.SetUpGraph(createGrid.Grid);
            itemSpawn.SpawnEatableItem();
        }
        
        private void ExecuteMovementSetting()
        {
            if (manualMovement)
            {
                manualMovement = true;
                automaticMovement = false;
            }
            
            else if (automaticMovement)
            {
                manualMovement = false;
                automaticMovement = true;
            }
            
            else
            {
                manualMovement = true;
                automaticMovement = false;
            }
            automaticSneaky.enabled = automaticMovement;
            playerInput.enabled = manualMovement;
            itemSpawn.IsAutomaticMovement = automaticMovement;
            itemSpawn.IsManualMovement = manualMovement;
            snakeyBodyBehaviour.IsAutomaticMovement = automaticMovement;
            snakeyBodyBehaviour.IsManualMovement = manualMovement;
        }
    }
}
