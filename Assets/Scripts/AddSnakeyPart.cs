using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class AddSnakeyPart : MonoBehaviour
{
    [SerializeField] private GameObject snakeyBodyPart;
    private LinkedList<Transform> snakeyBody = new LinkedList<Transform>();

    
    private void Awake()
    {
        PlayerInput.OnMovement += PlayerInputOnMovement; 
    }

    private void PlayerInputOnMovement(Vector3 previousPosition)
    {
        if (snakeyBody.Count <= 0)
        {
            return;
        }

        var snakePart = snakeyBody.Tail;
        while (snakePart != null)
        {
            if (snakePart == snakeyBody.Head)
            {
                snakePart.nodeItem.position = previousPosition;
            }
            else
            {
                snakePart.nodeItem.position = snakePart.previousNode.nodeItem.position;
            }
            snakePart = snakePart.previousNode;
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
}
