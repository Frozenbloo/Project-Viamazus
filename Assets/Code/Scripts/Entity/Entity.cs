using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [Header("Pathfinding")]
    [SerializeField] float pathfindingRadius;
    [SerializeField] float moveSpeed;
	private GameObject object2Pathfind2;

	[Header("Stats")]
    [SerializeField] float entityMaxHealth;
	[SerializeField] float damage;

    private float entityHealth;

    [Header("Pathfinding 'Radar'")]
	[SerializeField] int numOfRays = 30;
	[SerializeField] ContactFilter2D contactFilter;

	private Vector3 movementVector;
	private BoxCollider2D boxCollider2D;
	private RaycastHit2D hitRaycast;
	private Collider2D[] hits = new Collider2D[10];
	

	// Start is called before the first frame update
	void Start()
    {
		FullyHeal();
		
    }

	void Awake()
	{
		object2Pathfind2 = GameObject.Find("Player");
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        Pathfind();
		CheckCollision();
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
	    //
	}

    public virtual void Go2Player()
    {
		float xMovement = object2Pathfind2.transform.position.x - transform.position.x;

		// Set the movement vector and Normalise it
		movementVector = (object2Pathfind2.transform.position - transform.position).normalized;

		if (xMovement < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else if (xMovement > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}

		hitRaycast = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(0, movementVector.y), Mathf.Abs(movementVector.y * Time.deltaTime * 0.5f), LayerMask.GetMask("Entity", "Blocking"));
		if (hitRaycast.collider == null)
		{
			transform.Translate(0, movementVector.y * moveSpeed * Time.fixedDeltaTime, 0);
		}

		hitRaycast = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(movementVector.x, 0), Mathf.Abs(movementVector.x * Time.deltaTime  * 0.5f), LayerMask.GetMask("Entity", "Blocking"));
		if (hitRaycast.collider == null)
		{
			transform.Translate(movementVector.x * moveSpeed * Time.fixedDeltaTime, 0, 0);
		}
	}

    private void PingRadar()
    {
		movementVector = Vector3.zero;
        float angleInRads = 2 * Mathf.PI / numOfRays; //Calculates the angle between each ray
		for (int i = 0; i < numOfRays; i++)
        {
            float x = Mathf.Sin(angleInRads * i);
            float y = Mathf.Cos(angleInRads * i);

            Vector2 direction = new Vector2(x, y);
            RaycastHit2D radarHitInfo = Physics2D.Raycast(transform.position, direction);

            if (radarHitInfo.collider != null && !radarHitInfo.collider.CompareTag("Player"))
            {
				WanderAround();
			}
            else
            {
				Go2Player();
			}
        }
    }
	#endregion

	#region Damage
	public virtual void CheckCollision()
	{
		boxCollider2D.OverlapCollider(contactFilter, hits);
		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i] == null)
				continue;

			OnCollide(hits[i]);

			//Cleans arr
			hits[i] = null;
		}
	}

	public virtual void OnCollide(Collider2D collider)
	{
		if (collider.CompareTag("Player"))
		{
			DamagePlayer();
			Destroy(gameObject);
		}
	}

	public virtual void DamagePlayer()
	{
		object2Pathfind2.GetComponent<IDamageable>().Damage(damage);
	}
	#endregion
}
