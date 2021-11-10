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

        private PlayerInput playerInput;
        
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
                Vector3 position;
                switch (playerInput.CurrentDirection)
                {
                    case PlayerInput.Direction.up:
                        position = new Vector3(transform.position.x, transform.position.y -1, 0f);
                        break;
                    case PlayerInput.Direction.right:
                        position = new Vector3(transform.position.x -1 , transform.position.y, 0f);
                        break;
                    case PlayerInput.Direction.down:
                        position = new Vector3(transform.position.x, transform.position.y + 1, 0f);
                        break;
                    case PlayerInput.Direction.left:
                        position = new Vector3(transform.position.x + 1 , transform.position.y, 0f);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return position;
            }
            return snakeyBodyLL.Tail.nodeItem.position;
        }
    
        public void ResetBody()
        {
            snakeyBodyLL.Clear();
        }
        
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.OnMovement += MoveBody;
            //GetComponent<PlayerInput>().OnMovement += MoveBody;
            GetComponentInChildren<SnakeyCollision>().OnGameOver += ResetBody;
        }
    }

    internal class Direction
    {
    }
}

