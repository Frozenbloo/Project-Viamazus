using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
	private MazeCell[,] maze;
	private int width;
	private int height;

	public Pathfinding(MazeCell[,] maze, int width, int height)
	{
		this.maze = maze;
		this.width = width;
		this.height = height;
	}

	public List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int endPos)
	{
		List<Vector2Int> path = new List<Vector2Int>();
		HashSet<Vector2Int> openSet = new HashSet<Vector2Int>();
		HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
		Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
		Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float>();
		Dictionary<Vector2Int, float> fScore = new Dictionary<Vector2Int, float>();

		openSet.Add(startPos);
		gScore[startPos] = 0;
		fScore[startPos] = Heuristic(startPos, endPos);

		while (openSet.Count > 0)
		{
			Vector2Int current = GetLowestFScore(openSet, fScore);
			if (current == endPos)
			{
				path = ReconstructPath(cameFrom, current);
				break;
			}

			openSet.Remove(current);
			closedSet.Add(current);

			foreach (Vector2Int neighbor in GetNeighbors(current))
			{
				if (closedSet.Contains(neighbor))
					continue;

				float tentativeGScore = gScore[current] + 1; // Assuming a cost of 1 for each step

				if (!openSet.Contains(neighbor))
					openSet.Add(neighbor);
				else if (tentativeGScore >= gScore[neighbor])
					continue;

				cameFrom[neighbor] = current;
				gScore[neighbor] = tentativeGScore;
				fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, endPos);
			}
		}
		return path;
	}

	#region Helper Methods
	private Vector2Int GetLowestFScore(HashSet<Vector2Int> openSet, Dictionary<Vector2Int, float> fScore)
	{
		Vector2Int lowest = new Vector2Int(-1, -1);
		float lowestFScore = float.MaxValue;

		foreach (Vector2Int point in openSet)
		{
			if (!fScore.ContainsKey(point)) continue;

			if (fScore[point] < lowestFScore)
			{
				lowestFScore = fScore[point];
				lowest = point;
			}
		}

		return lowest;
	}

	private List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
	{
		List<Vector2Int> path = new List<Vector2Int>();

		while (cameFrom.ContainsKey(current))
		{
			path.Add(current);
			current = cameFrom[current];
		}
		path.Add(current);

		path.Reverse();
		return path;
	}

	private float Heuristic(Vector2Int a, Vector2Int b)
	{
		return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
	}

	private List<Vector2Int> GetNeighbors(Vector2Int current)
	{
		List<Vector2Int> neighbors = new List<Vector2Int>();

		Vector2Int[] possibleNeighbors = new Vector2Int[]
		{
		new Vector2Int(current.x, current.y + 1),
		new Vector2Int(current.x, current.y - 1),
		new Vector2Int(current.x + 1, current.y),
		new Vector2Int(current.x - 1, current.y)
		};

		foreach (Vector2Int neighbor in possibleNeighbors)
		{
			if (neighbor.x >= 0 && neighbor.y >= 0 && neighbor.x < width && neighbor.y < height)
			{
				if (!maze[neighbor.x, neighbor.y].BeenVisited)
				{
					neighbors.Add(neighbor);
				}
			}
		}

		return neighbors;
	}

	#endregion

}
