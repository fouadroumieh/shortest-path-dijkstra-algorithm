using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingNetworkLibrary
{
    class PriorityQueue
    {
        public class PriorityQueueElement
        {
            public int index;
            public int value;
            public int vertex;
            public PriorityQueueElement(int index, int value, int vertex)
            {
                this.index = index;
                this.value = value;
                this.vertex = vertex;
            }
        }

        int size;  // size of the queue
        int n;     // number of elements in the queue right now
        PriorityQueueElement[] heap; // heap
        int[] index; // index of the element
        public int getMin()
        {
            int x = heap[0].vertex;
            swap(0, n - 1);
            n--;
            down(0);
            return x;
        }

        public PriorityQueue(int n)
        {
            size = n;
            this.n = 0;
            heap = new PriorityQueueElement[size];
        }

        public void Update(int i)
        {
            up(i);
        }

        public void Insert(PriorityQueueElement e)
        {
            e.index = n;
            heap[n] = e;
            n++;
            up(n - 1);
        }

        public bool IsEmpty()
        {
            return n == 0;
        }

        // the element in position index goes down in the heap
        void down(int index)
        {
            int leftChild = index * 2 + 1;
            int rightChild = index * 2 + 2;
            if (leftChild >= n) return;     // last level of the heap
            int bestChild = leftChild;
            if (rightChild < n)
            {
                if (heap[rightChild].value < heap[leftChild].value)
                    bestChild = rightChild;
            }
            if (heap[bestChild].value < heap[index].value) // this element needs to go down in the heap
            {
                swap(bestChild, index);
                down(bestChild);
            }
        }

        // the element in position index goes down in the heap
        void up(int index)
        {
            if (index == 0) return; // element already in the root
            int parent = (index - 1) / 2;
            if (heap[parent].value > heap[index].value)
            {
                swap(index, parent);
                up(parent);
            }
        }


        void swap(int i, int j)
        {
            PriorityQueueElement temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
            heap[i].index = i;
            heap[j].index = j;
        }
    }
}
