using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

namespace Snakey
{
    public class SnakeyBodyBehaviour : MonoBehaviour
    {
        [SerializeField] 
        private GameObject snakeyBodyPartPrefab;
        private LinkedList<Transform> snakeyBodyLL = new LinkedList<Transform>();
        
        private void MoveBody(Vector3 snakeHeadPosition)
        {
            if (snakeyBodyLL.Count <= 0)
            {
                return;
            }
            
            LinkedList<Transform>.ListNode currentNode = snakeyBodyLL.Tail;
            
            while (currentNode != null)
            {
                if (currentNode == snakeyBodyLL.Head)
                {
                    currentNode.nodeItem.position = snakeHeadPosition;
                }
                else
                {
                    currentNode.nodeItem.position = currentNode.previousNode.nodeItem.position;
                }
                currentNode = currentNode.previousNode;
            }
        }
    
        public void GrowBody()
        {
            Vector3 position = GetTailPosition();
            GameObject bodyPart = Instantiate(snakeyBodyPartPrefab, position, Quaternion.identity);
            snakeyBodyLL.Add(bodyPart.transform);
        }

        private Vector3 GetTailPosition()
        {
            if (snakeyBodyLL.Count <= 0)
            {
                return transform.position;
            }
            return snakeyBodyLL.Tail.nodeItem.position;
        }
    
        public void ResetBody()
        {
            snakeyBodyLL.Clear();
        }
        
        private void Awake()
        {
            GetComponent<PlayerInput>().OnMovement += MoveBody;
            GetComponentInChildren<SnakeyCollision>().OnGameOver += ResetBody;
        }
    }
}

