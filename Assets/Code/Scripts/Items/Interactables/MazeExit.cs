using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeExit : Interactable
{
	[SerializeField] SpriteRenderer exitSpriteRenderer;
	[SerializeField] Sprite openDoor;

	private void ExitTheMaze()
	{
		exitSpriteRenderer.sprite = openDoor;
	}

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact"))
		{
			ExitTheMaze();
		}
		else base.OnCollide(collider);
	}
}
