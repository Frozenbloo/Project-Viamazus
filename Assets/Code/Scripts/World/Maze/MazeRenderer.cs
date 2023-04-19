using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] MazeGenerator mazeGen;
    [SerializeField] GameObject mazeCell;
	[SerializeField] GameObject chestObject;
	[SerializeField] int chestChance;

    [SerializeField] float cellSize = 0.64f;

	private void Start()
	{
		System.Random rnd = Utils.rng;

		MazeCell[,] maze = mazeGen.getMaze();

		for (int x = 0; x < mazeGen.mazeSize; x++)
		{
			for (int y = 0; y < mazeGen.mazeSize; y++)
			{
				GameObject cell = Instantiate(mazeCell, new Vector3(Utils.GetClosestNumber((float)x * cellSize, 0.64f), Utils.GetClosestNumber((float)y * cellSize, 0.64f), 0f), Quaternion.identity, transform);
				AlignItemToGrid(cell, cellSize);
				if (rnd.Next(100) < chestChance)
				{
					Vector3 chestPos = new Vector3((float)x * cellSize, (float)y * cellSize + 0.5f, 0f);

					Collider2D[] chestColliders = Physics2D.OverlapBoxAll(chestPos, new Vector2(0.16f, 0.16f), 0f, 7);
					if (chestColliders.Length == 0)
					{
						GameObject chest = Instantiate(chestObject, chestPos, Quaternion.identity, transform);
						AlignItemToGrid(chest, 0.16f);
					}
				}
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

	private void AlignItemToGrid(GameObject chest, float gridSize)
	{
		Vector3 chestPos = chest.transform.position;

		chestPos.x = Mathf.Round(chestPos.x / gridSize) * gridSize;
		chestPos.y = Mathf.Round(chestPos.y / gridSize) * gridSize;

		chest.transform.position = chestPos;
	}
}
