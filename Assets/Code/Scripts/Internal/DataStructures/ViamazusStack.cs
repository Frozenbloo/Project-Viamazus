using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViamazusStack<T>
{
    private List<T> stack;

	/// <summary>
	/// Generic class, which means that it can be used with any type of data
	/// </summary>
	public ViamazusStack() 
	{
		this.stack = new List<T>();
	}

	public void push(T item) { stack.Add(item); }

	public T peek() { return stack[stack.Count - 1]; }

	public int count() { return stack.Count; }

	//Returns true if stack.count == 0, false if not
	public bool isEmpty() { return stack.Count == 0; }

	public T pop() 
	{ 
		int lastItem = stack.Count - 1;
		T item = stack[lastItem];
		stack.RemoveAt(lastItem);
		return item;
	}
}
