using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;

public class Entity : MonoBehaviour, IDamageable
{
    [Header("Pathfinding")]
    [SerializeField] GameObject object2Pathfind2;
    [SerializeField] float pathfindingRadius;
    [SerializeField] float moveSpeed;

	[Header("Stats")]
    [SerializeField] float entityMaxHealth;
    private float entityHealth;

	// Start is called before the first frame update
	void Start()
    {
        FullyHeal();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Pathfind();
    }

	#region Health
	public void KillEntity()
    {
        Destroy(gameObject);
    }

    // Could be used for boss ability later
    public void FullyHeal()
    {
		entityHealth = entityMaxHealth;
	}

	public void Damage(float dmgAmount)
	{
		float tempHealth = entityHealth - dmgAmount;
        if (tempHealth <= 0) KillEntity();
        else if (tempHealth > entityMaxHealth) 
        {
            FullyHeal();
        }
	}
	#endregion

	#region Pathfinding
	public virtual void Pathfind()
    {
        
    }
	#endregion
}
