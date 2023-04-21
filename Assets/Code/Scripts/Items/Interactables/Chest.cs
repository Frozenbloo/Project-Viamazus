using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : Interactable
{
	[SerializeField] SpriteRenderer chestSpriteRenderer;
	[SerializeField] Sprite openChest;
	[Header("Drops")]
	[SerializeField] List<GameObject> chestDrops;

	private bool isChestOpen = false;

	private void OpenTheChest()
	{
		isChestOpen = true;
		chestSpriteRenderer.sprite = openChest;
	}

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact") && !isChestOpen)
		{
			OpenTheChest();
		}
		else base.OnCollide(collider);
	}
}
