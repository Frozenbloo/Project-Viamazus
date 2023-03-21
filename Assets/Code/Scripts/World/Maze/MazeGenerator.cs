using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
	//Range from 5 to 40
	[Range(5, 40)]
	public short mazeSize = 5;
	private float startX, startY, endX, endY;
	private System.Random rand = new System.Random();
	public GameObject spawnPos;

	private List<Vector2Int> cellPath = new List<Vector2Int>();
	private List<Dirs> randomDir = new List<Dirs> { Dirs.North, Dirs.South, Dirs.East, Dirs.West };

	private MazeCell[,] maze;

	Vector2Int cell;

	/// <summary>
	/// This method returns the maze generated by the algorithm and is the default class constructor.
	/// </summary>
	/// <returns>The maze generated by the algorithm</returns>
	public MazeCell[,] getMaze()
	{
		startX = rand.Next(mazeSize - 1) * 0.64f;
		startY = rand.Next(mazeSize - 1) * 0.64f;
		maze = new MazeCell[mazeSize, mazeSize];
		spawnPos.transform.position = new Vector2(startX + 0.32f, startY + 0.32f);
		fillFullCells();
		CarveMazePath((short)startX, (short)startY);
		return maze;
	}

	/// <summary>
	/// This method initializes all cells in the maze with all walls intact
	/// </summary>
	private void fillFullCells()
	{
		for (short x = 0; x < mazeSize; x++)
		{
			for (short y = 0; y < mazeSize; y++)
			{
				maze[x, y] = new MazeCell(x, y);
			}
		}
	}

	List<Dirs> dirs = new List<Dirs> { Dirs.North, Dirs.South, Dirs.East, Dirs.West };

	/// <summary>
	/// This method returns a random order of the four possible directions (North, South, East, West)
	/// </summary>
	/// <returns>One of the four possible directions</returns>
	private List<Dirs> GetRandomDirs()
	{
		Utils.Shuffle(randomDir);
		return randomDir;
	}

	private bool isCellValid(short x, short y)
	{
		if (x < 0 || y < 0 || x > mazeSize - 1 || y > mazeSize - 1 || maze[x, y].BeenVisited) return false;
		else return true;
	}

	private Vector2Int CheckNeighbourValid()
	{
		List<Dirs> randomDir = GetRandomDirs();

		for (int i = 0; i < randomDir.Count; i++)
		{
			Vector2Int neighbourCell = cell;

			if (randomDir[i] == Dirs.North)
			{
				neighbourCell.y++;
			}
			else if (randomDir[i] == Dirs.South)
			{
				neighbourCell.y--;
			}
			else if (randomDir[i] == Dirs.East)
			{
				neighbourCell.x++;
			}
			else if (randomDir[i] == Dirs.West)
			{
				neighbourCell.x--;
			}

			if (isCellValid((short)neighbourCell.x, (short)neighbourCell.y)) return neighbourCell;

		}
		return cell;
	}

	private void DetermineWallLocations(Vector2Int cell, Vector2Int neighbourCell)
	{
		if (cell.x > neighbourCell.x)
		{
			maze[cell.x, cell.y].leftWall = false;
		}
		else if (cell.x < neighbourCell.x)
		{
			maze[neighbourCell.x, neighbourCell.y].leftWall = false;
		}
		else if (cell.y < neighbourCell.y)
		{
			maze[cell.x, cell.y].topWall = false;
		}
		else if (cell.y > neighbourCell.y)
		{
			maze[neighbourCell.x, neighbourCell.y].topWall = false;
		}
	}
	private void CarveMazePath(short x, short y)
	{
		cell = new Vector2Int(x, y);

		Vector2Int nextCellForPath = CheckNeighbourValid();
		if (nextCellForPath == cell)
		{
			//Attempts to backtrack
			for (short i = (short)((short)cellPath.Count - 1); i >= 0; i--)
			{
				cell = cellPath[i];
				cellPath.RemoveAt(i);
				nextCellForPath = CheckNeighbourValid();
				if (nextCellForPath != cell) break;
			}
			//Couldn't find a cell to backtrack to
			if (cell == nextCellForPath) return;
		}
		else
		{
			DetermineWallLocations(cell, nextCellForPath);
			maze[cell.x, cell.y].BeenVisited = true;
			cell = nextCellForPath;
			cellPath.Add(cell);
		}
		CarveMazePath((short)cell.x, (short)cell.y);
	}

	public int GetMazeSize()
	{
		return mazeSize;
	}

}
