using UnityEngine;

public enum Dirs
{
	North,
	South,
	East,
	West
}

public class MazeCell : ICell
{
	private bool beenVisited;
	private readonly short x, y;
	private bool cTopWall, cLeftWall;
	private Vector2Int cPosition;

	public MazeCell(short x, short y)
	{
		this.x = x;
		this.y = y;

		beenVisited = false;

		topWall = leftWall = true;
	}

	public Vector2Int position { get => new Vector2Int(x, y); set => cPosition = value; }
	public bool BeenVisited { get => beenVisited; set => beenVisited = value; }
	public bool topWall { get => cTopWall; set => cTopWall = value; }
	public bool leftWall { get => cLeftWall; set => cLeftWall = value; }
}
