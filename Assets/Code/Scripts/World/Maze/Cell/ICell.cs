using UnityEngine;

public interface ICell
{
	bool topWall { get; set; }
	bool leftWall { get; set; }
	Vector2Int position { get; set; }
}
