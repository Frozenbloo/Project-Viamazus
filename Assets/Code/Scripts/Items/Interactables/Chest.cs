using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
	[SerializeField] private SpriteRenderer chestSpriteRenderer;
	[SerializeField] private Sprite openChest;
	[Header("Drops")]
	[SerializeField] private int dropAmount;
	[SerializeField] private float dropRadius;
	[SerializeField] private GameObject healthPot;
	[SerializeField] private GameObject speedPot;
	[SerializeField] private int maxCoins = 50;
	private readonly ViamazusChanceDictionary chestDrops = new ViamazusChanceDictionary();

	private bool isChestOpen = false;

	private void OpenTheChest()
	{
		chestDrops.Add(healthPot, 0.5f);
		chestDrops.Add(speedPot, 0.2f);
		isChestOpen = true;
		chestSpriteRenderer.sprite = openChest;
		GameEvents.instance.onCoinCollect.Invoke(Utils.rng.Next(maxCoins));
		SpawnItems();
	}

	private void SpawnItems()
	{
		Debug.Log("Spawning Items");
		List<GameObject> drops = GetDrops(chestDrops, dropAmount);

		BoxCollider2D[] colliders = FindObjectsOfType<BoxCollider2D>();

		for (int i = 0; i < drops.Count; i++)
		{
			Vector2 pos = (Random.insideUnitCircle * dropRadius) + (Vector2)gameObject.transform.position;

			bool insideCollider = false;
			foreach (var collider in colliders)
			{
				if (collider.bounds.Contains(transform.position) && !collider.CompareTag("Interactable"))
				{
					insideCollider = true;
					break;
				}
			}
			if (!insideCollider)
			{
				GameObject item = Instantiate(drops[i], pos, Quaternion.identity);
			}
		}
	}

	private List<GameObject> GetDrops(ViamazusChanceDictionary drops, int dropAmount)
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
