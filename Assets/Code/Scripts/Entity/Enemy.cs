using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    protected float health;
    protected float damage;
    protected float attackSpeed;
    protected float movementSpeed;
    protected int xpReward;
    protected int goldReward;

	public abstract void Attack();
	public abstract void Move();
	public abstract void Die();

	public void Damage(float dmgAmount)
	{
		health -= dmgAmount;
		if (health <= 0) Die();
	}
}
