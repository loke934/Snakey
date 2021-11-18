using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snakey
{
    public class LinkedList<T>
    {
        private ListNode head;
        private ListNode tail;
        private int count;
    
        public LinkedList()
        {
            head = null; 
            tail = null;
            count = 0;
        }
        
        public int Count => count;
        public ListNode Tail => tail;
        public ListNode Head => head;
        
        public class ListNode 
        {
            public T nodeItem;
            public ListNode previousNode;
            public ListNode nextNode;
            public int index;
        
            public ListNode(T item, ListNode previous, ListNode next, int i)
            {
                nodeItem = item;
                previousNode = previous;
                nextNode = next;
                index = i;
            }
        }
    
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

        public List<T> GetAllAfterIndex(int i = 0) 
        {
            List<T> nodesToReturn = new List<T>();
            ListNode currentNode = head;
            while (currentNode != null)
            {
                if (currentNode.index >= i)
                {
                    nodesToReturn.Add(currentNode.nodeItem);
                }
                currentNode = currentNode.nextNode;
            }
            return nodesToReturn;
        }
        
        public void RemoveAllFrom(int i)
        {
            if (i == 0)
            {
                Clear();
            }
            else
            {
                ListNode currentNode = head;

                while (currentNode != null)
                {
                    if (currentNode.index == i)
                    {
                        tail = currentNode.previousNode;
                        currentNode.previousNode.nextNode = null;
                        break;
                    }
                    currentNode = currentNode.nextNode;
                }
                count -= (count - i);
            }
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
    }
}

