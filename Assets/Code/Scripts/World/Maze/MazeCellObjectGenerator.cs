using UnityEngine;

public class MazeCellObjectGenerator : MonoBehaviour
{
	[SerializeField]
	private GameObject northWall, southWall, eastWall, westWall;
	public void Init(bool north, bool south, bool east, bool west)
	{
		northWall.SetActive(north); southWall.SetActive(south); eastWall.SetActive(east); westWall.SetActive(west);
	}
}
