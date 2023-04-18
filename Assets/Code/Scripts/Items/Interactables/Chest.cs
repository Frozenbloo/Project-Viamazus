using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : Interactable
{
	public SpriteRenderer chestSpriteRenderer;
    public Sprite openChest;

	private bool isChestOpen = false;

	public List<GameObject> chestDrops;

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
