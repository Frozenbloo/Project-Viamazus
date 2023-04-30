using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private int playerHeath;
    private Image healthBar;

    private void Awake()
    {
        healthBar = GetComponent<Image>();
    }

	private void Start()
	{
		if (GameEvents.instance.onPlayerHeathChange == null) GameEvents.instance.onPlayerHeathChange = new Viamazus2FloatEvent();
		GameEvents.instance.onPlayerHeathChange.AddListener(OnHealthChange);
	}

	private void OnDisable()
	{
		GameEvents.instance.onPlayerHeathChange.RemoveListener(OnHealthChange);
	}

	private void OnHealthChange(float health, float maxHealth)
    {
		healthBar.fillAmount = health / maxHealth;
	}

}
