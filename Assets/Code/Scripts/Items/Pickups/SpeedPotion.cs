using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Interactable
{
    public float speedMultiplier;
	public int duration;

	private void speedUp(Player player)
	{
		player.speedUp(speedMultiplier, duration);
		Destroy(gameObject);
	}

	protected override void OnCollide(Collider2D collider)
	{
		if (collider.GetComponent<Player>())
		{
			speedUp(collider.GetComponent<Player>());
		}
	}
}
