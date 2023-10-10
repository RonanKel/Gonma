using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SongFormatScript
{
    public class MinHeap 
    {
        public List<Note> heap;

        public MinHeap()
        {
            heap = new List<Note>();
        }

        public int Count
        {
            get { return heap.Count; }
        }

        public void Insert(Note item)
        {
            heap.Add(item);
        }

        public Note Peek()
        {
            return heap[0];
        }

        public Note ExtractMin()
        {
            if (heap.Count == 0) {
                Debug.Log("the song heap is empty!");
            }

            Note minItem = heap[0];
            int length = heap.Count - 1;
            heap[0] = heap[length];
            heap.RemoveAt(length);
            HeapifyDown(0);

            return minItem;
        }

        private void HeapifyUp(int index)
        {
            int parentIndex;
            int compareResult;
            while (index > 0) {
                parentIndex = (index - 1) / 2;
                compareResult = heap[index].Compare(heap[parentIndex]);
                if (compareResult < 0) {
                    Swap(index, parentIndex);
                    index = parentIndex;
                }
                else {
                    break;
                }
            }
        }

        private void HeapifyDown(int index)
        {
            int smallest = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;

            if (leftChild < heap.Count && heap[leftChild].Compare(heap[smallest]) < 0)
            {
                smallest = leftChild;
            }

            if (rightChild < heap.Count && heap[rightChild].Compare(heap[smallest]) < 0)
            {
                smallest = rightChild;
            }

            if (smallest != index)
            {
                Swap(index, smallest);
                HeapifyDown(smallest);
            }
        }

        private void Swap(int index1, int index2)
        {
            Note temp = heap[index1];
            heap[index1] = heap[index2];
            heap[index2] = temp;
        }

        public void AddNote(float beatPos, string noteColor) 
        {
            Note note = new Note(beatPos, noteColor);
            Insert(note);
        }
    }

    public class Note
    {  
        public float beatPos;
        public string color; // 0 = gold, 1 = magenta, 2 = teal
        
        public Note(float thisBeatPos, string noteColor)
        {
            beatPos = thisBeatPos;
            color = noteColor;
        }

        public int Compare(Note other)
        {
            if (beatPos < other.beatPos) {
                return -1;
            }
            else if (beatPos > other.beatPos) {
                return 1;
            }
            else {
                return 0;
            }
        }
    }

    public class GoldNote : Note
    {
        public GoldNote(float beatPos) : base(beatPos, "gold") {}
    }

    public class MagentaNote : Note
    {
        public MagentaNote(float beatPos) : base(beatPos, "magenta") {}
    }

    public class TealNote : Note
    {
        public TealNote(float beatPos) : base(beatPos, "teal") {}
    }
}