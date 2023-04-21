using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPotion : Interactable
{
	[SerializeField] int healthAmount;

	private void healEntity(Player player)
	{
		player.Damage(-healthAmount);
		Destroy(gameObject);
	}

	protected override void OnCollide(Collider2D collider)
	{
		if (collider.GetComponent<Player>())
		{
			healEntity(collider.GetComponent<Player>());
		}
	}
}
