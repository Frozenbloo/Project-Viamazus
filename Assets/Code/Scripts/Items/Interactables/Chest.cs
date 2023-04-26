using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Chest : Interactable
{
	[SerializeField] SpriteRenderer chestSpriteRenderer;
	[SerializeField] Sprite openChest;
	[Header("Drops")]
	[SerializeField] int dropAmount;
	[SerializeField] float dropRadius;
	[SerializeField] GameObject healthPot;
	[SerializeField] GameObject speedPot;
	private ViamazusChestDictionary chestDrops = new ViamazusChestDictionary();

	private bool isChestOpen = false;

	private void OpenTheChest()
	{
		chestDrops.Add(healthPot, 0.5f);
		chestDrops.Add(speedPot, 0.2f);
		isChestOpen = true;
		chestSpriteRenderer.sprite = openChest;
		SpawnItems();
	}

	private void SpawnItems()
	{
		Debug.Log("Spawning Items");
		List<GameObject> drops = GetDrops(chestDrops, dropAmount);

		BoxCollider2D[] colliders = FindObjectsOfType<BoxCollider2D>();

		for (int i = 0; i < dropAmount; i++)
		{
			Vector2 pos = Random.insideUnitCircle * dropRadius;

			bool insideCollider = false;
			foreach (var collider in colliders)
			{
				if (collider.bounds.Contains(transform.position))
				{
					insideCollider = true;
					break;
				}
			}

			if (!insideCollider)
			{
				GameObject item = Instantiate(drops[i], transform.position, Quaternion.identity, gameObject.transform);
			}
		}
	}

	private List<GameObject> GetDrops(ViamazusChestDictionary drops, int dropAmount)
	{
		List<GameObject> items = new List<GameObject>();
		foreach (var item in drops)
		{
			float chance = item.Value;
			for (int i = 0; i < dropAmount; i++)
			{
				if (Utils.rng.NextDouble() < chance)
				{
					items.Add(item.Key);
				}
			}
		}
		return items;
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
