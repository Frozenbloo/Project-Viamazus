using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static string renameObject(string wall, int x, int y)
	{
		return wall + " | (" + x + ", " + y + ")";
	}

	public static System.Random rng = new System.Random();

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

	public static bool IsDivisible(float x, float y)
	{
		return (x % y) == 0;
	}

	public static float GetClosestNumber(float x, float y)
	{
		float div = x / y;
		float firstClosest = y * div;
		float secondClosest = (x * y) > 0 ? (y * (div + 1)) : (y * (div - 1));

		if (Mathf.Abs(x - firstClosest) < Mathf.Abs(x - secondClosest)) 
		{
			return firstClosest;
		}
		return secondClosest;
	}
}
