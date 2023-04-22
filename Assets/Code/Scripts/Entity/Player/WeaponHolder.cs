using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Vector2 pointerPos { get; set; }

	private void Update()
	{
		//Points towards mouse position
		Vector2 direction = (pointerPos - (Vector2)transform.position).normalized;
		transform.right = direction;

		Vector2 scale = transform.localScale;
		if (direction.x < 0)
		{
			scale.y = -1;
		}
		else if(direction.x > 0) {
			scale.y = 1;
		}
		transform.localScale = scale;
	}
}
