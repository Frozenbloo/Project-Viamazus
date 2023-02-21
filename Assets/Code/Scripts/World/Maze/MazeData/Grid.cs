using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Grid
{

    private int width, height;
	private float size;
    private int[,] gridArr;
	private bool[,] cellVisited;

    public Grid(int width, int height, float size)
	{
		this.width = width;
		this.height = height;
		this.size = size;

		gridArr = new int[width, height];
		cellVisited = new bool[width, height];

		for (int x = 0; x < gridArr.GetLength(0); x++)
		{
			for (int y = 0; y < gridArr.GetLength(1); y++)
			{
				//Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
				//Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
			}
		}
		//Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
		//Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
	}

	public void SetCellValue(int x, int y, int value)
	{
		gridArr[x, y] = value;
	}

	public int GetCellValue(int x, int y)
	{
		return gridArr[x, y];
	}

	public void SetCellVisited(int x, int y, bool visited)
	{
		if (x >= 0 && y >= 0 && x < width && y < height) cellVisited[x, y] = visited;
	}

	public bool GetCellVisited(int x, int y)
	{
		if (x >= 0 && y >= 0 && x < width && y < height) return cellVisited[x, y];
		return true;
	}

	public Vector3 GetWorldPosition(int x, int y)
	{
		return new Vector3(x, y) * size;
	}

	#region Encapsulated Fields

	public int Width { get => width; set => width = value; }
	public int Height { get => height; set => height = value; }
	public float Size { get => size; set => size = value; }

	#endregion
}
