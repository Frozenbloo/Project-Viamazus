using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellObjectGenerator : MonoBehaviour
{
	[SerializeField]
	GameObject northWall, southWall, eastWall, westWall;
	public void Init(bool north, bool south, bool east, bool west)
	{
		northWall.SetActive(north); southWall.SetActive(south); eastWall.SetActive(east); westWall.SetActive(west);
	}
}
