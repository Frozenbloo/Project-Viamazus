using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardNPC : Interactable
{
	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact"))
		{

		}
		else base.OnCollide(collider);
	}
}
