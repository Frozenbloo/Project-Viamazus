using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
	[SerializeField] private MazeGenerator mazeGen;
	[SerializeField] private GameObject mazeCell;
	[SerializeField] private float cellSize = 0.64f;
	[Header("Chest")]
	[SerializeField] private GameObject chestObject;
	[SerializeField] private int chestChance;
	[Header("Enemy")]
	[SerializeField] private int enemyChance = 20;
	[Header("Exit")]
	[SerializeField] private GameObject mazeExitObject;

	private float exitSpawnChance = 0.0f;
	private bool exitSpawn = false;

	private List<GameObject> generatedEnemies;

	private void Start()
	{
		System.Random rnd = Utils.rng;

		MazeCell[,] maze = mazeGen.getMaze();

		exitSpawnChance = 100 / (mazeGen.GetMazeSize() ^ 2);

		generatedEnemies = mazeGen.GetEnemies();

		for (int x = 0; x < mazeGen.GetMazeSize(); x++)
		{
			for (int y = 0; y < mazeGen.GetMazeSize(); y++)
			{
				GameObject cell = Instantiate(mazeCell, new Vector3(Utils.GetClosestNumber(x * cellSize, 0.64f), Utils.GetClosestNumber(y * cellSize, 0.64f), 0f), Quaternion.identity, transform);
				AlignItemToGrid(cell, cellSize);
				if (rnd.Next(100) < chestChance)
				{
					Vector3 chestPos = new Vector3(x * cellSize, y * cellSize + 0.5f, 0f);

					Collider2D[] chestColliders = Physics2D.OverlapBoxAll(chestPos, new Vector2(0.16f, 0.16f), 0f, 7);
					if (chestColliders.Length == 0 && chestPos.y <= mazeGen.GetMazeSize() * 0.6f)
					{
						GameObject chest = Instantiate(chestObject, chestPos, Quaternion.identity, transform);
					}
				}

				if (rnd.Next(100) < enemyChance)
				{
					Vector3 enemyPos = new Vector3(x * cellSize, y * cellSize + 0.5f, 0f);
					SpawnEnemy(enemyPos);
				}

				if (!exitSpawn && rnd.Next(100) < exitSpawnChance)
				{
					Vector3 exitPos = new Vector3(x * cellSize, y * cellSize + 0.5f, 0f);

					if (exitPos.y <= mazeGen.GetMazeSize() * 0.6f)
					{
						GameObject exit = Instantiate(mazeExitObject, exitPos, Quaternion.identity, transform);
						exitSpawn = true;
					}

					exitPos = new Vector3(x * cellSize, y * cellSize - 0.2f, 0f);
				}

				MazeCellObjectGenerator mazeCellObject = cell.GetComponent<MazeCellObjectGenerator>();
				bool top = maze[x, y].topWall;
				bool left = maze[x, y].leftWall;

				bool right = false;
				bool bottom = false;
				if (x == mazeGen.GetMazeSize() - 1) right = true;
				if (y == 0) bottom = true;

				mazeCellObject.Init(top, bottom, right, left);
			}
		}

		if (!exitSpawn)
		{
			Vector3 exitPos = new Vector3(0.00f * cellSize, 0.00f * cellSize + 0.5f, 0f);
			GameObject exit = Instantiate(mazeExitObject, exitPos, Quaternion.identity, transform);
			exitSpawn = true;
		}
	}

	private void AlignItemToGrid(GameObject chest, float gridSize)
	{
		Vector3 chestPos = chest.transform.position;

		chestPos.x = Mathf.Round(chestPos.x / gridSize) * gridSize;
		chestPos.y = Mathf.Round(chestPos.y / gridSize) * gridSize;

		chest.transform.position = chestPos;
	}

	private void SpawnEnemy(Vector3 pos)
	{
		if (pos.y <= mazeGen.GetMazeSize() * 0.6f && generatedEnemies.Count > 0)
		{
			GameObject enemy = Instantiate(generatedEnemies[0], pos, Quaternion.identity, transform);
			generatedEnemies.RemoveAt(0);
		}
	}
}
