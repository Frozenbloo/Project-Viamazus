using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] MazeGenerator mazeGen;
    [SerializeField] GameObject mazeCell;

    public float cellSize = 0.16f;

	private void Start()
	{
		MazeCell[,] maze = mazeGen.getMaze();

		for (int x = 0; x < mazeGen.mazeSize; x++)
		{
			for (int y = 0; y < mazeGen.mazeSize; y++)
			{
				GameObject cell = Instantiate(mazeCell, new Vector3((float)x * cellSize, (float)y * cellSize, 0f), Quaternion.identity, transform);

				MazeCellObjectGenerator mazeCellObject = cell.GetComponent<MazeCellObjectGenerator>();
				bool top = maze[x, y].topWall;
				bool left = maze[x, y].leftWall;

				bool right = false;
				bool bottom = false;
				if (x == mazeGen.mazeSize - 1) right = true;
				if (y == 0) bottom = true;

				mazeCellObject.Init(top, bottom, right, left);
			}
		}
	}
}
