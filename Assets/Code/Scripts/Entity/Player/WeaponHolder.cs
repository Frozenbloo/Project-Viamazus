using System.Collections;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
	[SerializeField] float swingTime = 0.2f;
	[SerializeField] float swingAngle = 10f;
    [SerializeField] public Vector2 pointerPos { get; set; }
	private GameObject weapon;
	private float oldAngle;

	private void Awake()
	{
		weapon = GameObject.Find("Weapon");
	}

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
		oldAngle = transform.rotation.eulerAngles.z;
		if (Input.GetMouseButtonDown(0)) StartCoroutine(SwingWeapon());
	}

	private IEnumerator SwingWeapon()
	{
		float startTime = Time.time;
		while (Time.time < startTime + swingTime)
		{
			float elapsed = Time.time - startTime;
			float t = elapsed / swingTime;
			float angle = Mathf.Lerp(-swingAngle, swingAngle, t);

			weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);

			yield return null;
		}
		weapon.transform.rotation = Quaternion.Euler(0f, 0f, oldAngle);
	}
}
