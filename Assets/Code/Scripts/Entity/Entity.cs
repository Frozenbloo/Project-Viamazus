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

    [Header("Pathfinding 'Radar'")]
	[SerializeField] int numOfRays = 12;

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
        PingRadar();
    }

    public virtual void WanderAround()
    {

    }

    public virtual void Go2Player()
    {

    }

    private void PingRadar()
    {
        float angleInRads = 2 * Mathf.PI / numOfRays; //Calculates the angle between each ray
		for (int i = 0; i < numOfRays; i++)
        {
            float x = Mathf.Sin(angleInRads * i);
            float y = Mathf.Cos(angleInRads * i);

            Vector2 direction = new Vector2(x, y);
            RaycastHit2D radarHitInfo = Physics2D.Raycast(transform.position, direction);

            if (radarHitInfo.collider != null && !radarHitInfo.collider.CompareTag("Player"))
            {
                Go2Player();
            }
            else
            {
                WanderAround();
            }
        }
    }
	#endregion
}
