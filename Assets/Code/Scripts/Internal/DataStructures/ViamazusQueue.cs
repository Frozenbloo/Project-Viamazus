using System.Collections.Generic;

public class ViamazusQueue<T>
{
	private readonly List<T> queue;

	public ViamazusQueue()
	{
		queue = new List<T>();
	}

	public void Enqueue(T item)
	{
		queue.Add(item);
	}

	public T Dequeue()
	{
		if (queue.Count == 0) return default(T);
		T item = queue[0];
		queue.RemoveAt(0);
		return item;
	}

	public void Clear()
	{
		queue.Clear();
	}

	public int Count
	{
		get { return queue.Count; }
	}

	public bool IsEmpty
	{
		get { return queue.Count == 0; }
	}
}
