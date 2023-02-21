using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static string renameObject(string wall, int x, int y)
	{
		return wall + " | (" + x + ", " + y + ")";
	}

	private static System.Random rng = new System.Random();

	public static void Shuffle<T>(this IList<T> list)
	{
		short n = (short)list.Count;
		while (n > 1)
		{
			n--;
			short k = (short)rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}
