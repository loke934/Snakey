using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList<T>
{
    private ListNode head;
    private ListNode tail;
    private int count;

    public LinkedList()
    {
        head = null; //same as value default?
        tail = null;
        count = 0;
    }
    
    public int Count => count;
    public ListNode Tail => tail;
    public ListNode Head => head;
    
    public class ListNode //In own script?
    {
        public T nodeItem;
        public ListNode previousNode;
        public ListNode nextNode;
        public int index = -1;
        
        public ListNode(T item, ListNode previous, ListNode next, int i)
        {
            nodeItem = item;
            previousNode = previous;
            nextNode = next;
            index = i;
        }
    }

    //Methods
    /// <summary>
    /// Adds item to a new node, to the end of the linked list.
    /// </summary>
    /// <param name="Node item, privious node, next node, count."></param>
    public void Add(T item)
    {
        ListNode newNode = new ListNode(item, tail, null, count);
        if (count == 0)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.nextNode = newNode;
            tail = newNode;
        }
        count++;
    }
}
