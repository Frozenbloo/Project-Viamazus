using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dirs
{
    North,
    South,
    East,
    West
}


public class MazeCell
{
    private bool beenVisited;
    private short x, y;
    private bool topWall, leftWall;
    private Vector2Int position;

    public MazeCell(short x, short y)
    {
        this.x = x;
        this.y = y;

        beenVisited= false;

        topWall = leftWall = true;
    }

	public Vector2Int Position { get => new Vector2Int(x, y); }
	public bool BeenVisited { get => beenVisited; set => beenVisited = value; }
	public bool TopWall { get => topWall; set => topWall = value; }
	public bool LeftWall { get => leftWall; set => leftWall = value; }
}
