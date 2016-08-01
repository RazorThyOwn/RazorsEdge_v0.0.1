using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHeapItem<T>{

	T[] items;
	int currentItemCount;


	public Heap(int maxHeapSize){

		items = new T[maxHeapSize];

	}

	//Add an item into the heap by adding the item to the current count, sorting up the item, and then adding up the count
	public void Add(T item){
		item.HeapIndex = currentItemCount;
		items [currentItemCount] = item;
		SortUp (item);
		currentItemCount++;
	}

	//Remove and return the first item from the heap by taking away the item at 0, lessening the count, taking the latest item and putting it at 0 and sorting it down
	public T RemoveFirst(){
		T firstItem = items [0];
		currentItemCount--;

		items [0] = items [currentItemCount];
		items [0].HeapIndex = 0;
		sortDown (items [0]);
		return firstItem;

	}

	//Updating and item in case it priority changes and you need to sort it up
	public void UpdateItem(T item){
		SortUp (item);
	}

	//simply return the count of an item
	public int Count {
		get{
			return currentItemCount;
		}
	}

	//check to see if the heap contains an item by looking for the item at the heapindex
	public bool Contains(T item){
		return Equals (items [item.HeapIndex], item);
	}

	//sort an item down by getting its children, checking to see if there are children, checking to see whether right or left has a higher priority and setting that one to the swap index,
	// then swaping them if they have a higher priority than the parent, all going in a while loop until there aren't children
	void sortDown(T item){
		while (true) {
			int childIndexLeft = item.HeapIndex * 2 + 1;
			int childIndexRight = item.HeapIndex * 2 + 2;
			int swapIndex = 0;

			if (childIndexLeft < currentItemCount) {
				swapIndex = childIndexLeft;

				if (childIndexRight < currentItemCount) {
					if (items [childIndexLeft].CompareTo (items [childIndexRight]) < 0) {
						swapIndex = childIndexRight;
					}
				}

				if (item.CompareTo (items [swapIndex]) < 0) {
					Swap (item, items [swapIndex]);
				} else {
					return;
				}
			} else {
				return;
			}
		}
	}

	//finding the parent of an item and checking to see if the parent has a higher priority and if so swaping them, while loop breaks if it doesn't
	void SortUp(T item){
		int parentIndex = (item.HeapIndex - 1) / 2;

		while (true) {
			T parentItem = items [parentIndex];
			if (item.CompareTo (parentItem) > 0) {
				Swap (item, parentItem);
			} else {
				break;
			}
		}
	}

	//takes in two items, set the items at eachother's index, and switches the heap index of those items with a temporary variable
	void Swap(T itemA, T itemB){
		items [itemA.HeapIndex] = itemB;
		items [itemB.HeapIndex] = itemA;

		int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}

}

//allows us to get and compare the heap indexes of items
public interface IHeapItem<T> : IComparable<T>{
	int HeapIndex {
		get;
		set;
	}
}
