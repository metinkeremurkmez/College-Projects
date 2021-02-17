using System;
using System.Collections.Generic;
using System.Text;

namespace Proje3B
{
    class Node
    {
        private int iData;
        public Node(int key) { iData = key; }
        public int getKey() { return iData; }
        public void setKey(int id) { iData = id; }
    }
    class Heap
    {
        private Node[] heapArray;
        private int maxSize; // size of array
        private int currentSize; // number of nodes in array
                                 // -------------------------------------------------------------
        public Heap(int mx) // constructor
        {
            maxSize = mx;
            currentSize = 0;
            heapArray = new Node[maxSize]; // create array
        }
        // -------------------------------------------------------------
        public bool isEmpty()
        { return currentSize == 0; }
        // -------------------------------------------------------------
        public bool insert(int key)
        {
            if (currentSize == maxSize)
                return false;
            Node newNode = new Node(key);
            heapArray[currentSize] = newNode;
            trickleUp(currentSize++);
            return true;
        } // end insert()
          // -------------------------------------------------------------
        public void trickleUp(int index)
        {
            int parent = (index - 1) / 2;
            Node bottom = heapArray[index];

            while (index > 0 &&
            heapArray[parent].getKey() < bottom.getKey())
            {
                heapArray[index] = heapArray[parent]; // move it down
                index = parent;
                parent = (parent - 1) / 2;
            } // end while
            heapArray[index] = bottom;
        } // end trickleUp()
          // -------------------------------------------------------------
        public Node remove() // delete item with max key
        { // (assumes non-empty list)
            Node root = heapArray[0];
            heapArray[0] = heapArray[--currentSize];
            trickleDown(0);
            return root;
        } // end remove()
          // -------------------------------------------------------------
        public void trickleDown(int index)
        {
            int largerChild;
            Node top = heapArray[index]; // save root
            while (index < currentSize / 2) // while node has at
            { // least one child,
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;
                // find larger child
                if (rightChild < currentSize && // (rightChild exists?)
                heapArray[leftChild].getKey() <
                heapArray[rightChild].getKey())
                    largerChild = rightChild;
                else
                    largerChild = leftChild;
                // top >= largerChild?
                if (top.getKey() >= heapArray[largerChild].getKey())
                    break;
                // shift child up
                heapArray[index] = heapArray[largerChild];
                index = largerChild; // go down
            } // end while
            heapArray[index] = top; // root to index

        } // end trickleDown()
          // -------------------------------------------------------------
        public bool change(int index, int newValue)
        {
            if (index < 0 || index >= currentSize)
                return false;
            int oldValue = heapArray[index].getKey(); // remember old
            heapArray[index].setKey(newValue); // change to new
            if (oldValue < newValue) // if raised,
                trickleUp(index); // trickle it up
            else // if lowered,
                trickleDown(index); // trickle it down
            return true;
        } // end change()
          // -------------------------------------------------------------



        public void displayHeap()
        {
            Console.WriteLine("heap tur fiyatlar: "); 
            for (int m = 0; m < currentSize; m++)
                if (heapArray[m] != null)
                    Console.WriteLine(heapArray[m].getKey() + " ");


            Console.WriteLine("heap en ucuz 3 tur fiyatı: "); 
            int x = 0;
            while (true)
            {
                if ((x * 2) + 1 < heapArray.Length)
                {
                    x = (x * 2) + 1;
                }
                else break;

            }
            Console.WriteLine(heapArray[x].getKey());
            Console.WriteLine(heapArray[x+1].getKey());
            Console.WriteLine(heapArray[(x - 1) / 2].getKey());





                }

    }

}