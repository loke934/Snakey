using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class AddSnakeyPart : MonoBehaviour
{
    [SerializeField] 
    private GameObject snakeyBodyPart;
    private LinkedList<Transform> snakeyBody = new LinkedList<Transform>();
    
    private void MoveSnakeyBody(Vector3 snakeHeadPosition)
    {
        if (snakeyBody.Count <= 0)//only head on snake
        {
            return;
        }

        LinkedList<Transform>.ListNode currentNode = snakeyBody.Tail;
        while (currentNode != null)
        {
            if (currentNode == snakeyBody.Head)
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

    public void Grow()
    {
        Vector3 position = GetTailPosition();
        GameObject bodyPart = Instantiate(snakeyBodyPart, position, Quaternion.identity);
        snakeyBody.Add(bodyPart.transform);
    }

    private Vector3 GetTailPosition()
    {
        if (snakeyBody.Count <= 0)
        {
            return transform.position;
        }
        return snakeyBody.Tail.nodeItem.position;
    }
    
    private void Awake()
    {
        PlayerInput.OnMovement += MoveSnakeyBody; 
    }
}
