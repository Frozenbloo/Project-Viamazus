using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [SerializeField] public static GameEvents instance { get; private set; }

	private void Awake()
	{
		if (GameEvents.instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public ViamazusIntEvent onCoinCollect;

	public ViamazusIntEvent onMazeBeat;

	public Viamazus2FloatEvent onPlayerHeathChange;
}
