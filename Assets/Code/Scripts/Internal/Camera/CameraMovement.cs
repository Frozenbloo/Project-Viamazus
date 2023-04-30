using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private Transform player;
	private readonly float xBound = 0.45f, yBound = 0.15f;

	private void LateUpdate()
	{
		Vector3 camVector = Vector3.zero;

		float xDelta = player.position.x - transform.position.x;
		if (xDelta > xBound || xDelta < -xBound)
		{
			if (transform.position.x < player.position.x)
			{
				camVector.x = xDelta - xBound;
			}
			else
			{
				camVector.x = xDelta + xBound;
			}
		}

		float yDelta = player.position.y - transform.position.y;
		if (yDelta > yBound || yDelta < -yBound)
		{
			if (transform.position.y < player.position.y)
			{
				camVector.y = yDelta - yBound;
			}
			else
			{
				camVector.y = yDelta + yBound;
			}
		}

		transform.position += new Vector3(camVector.x, camVector.y, 0);
	}
}
