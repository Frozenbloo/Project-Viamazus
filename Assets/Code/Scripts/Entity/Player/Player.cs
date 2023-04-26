using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
	#region Movement Vars
	[SerializeField] float playerSpeed = 1f;
	[SerializeField] Transform spawnPoint;
	[SerializeField] bool canMove = true;
	private BoxCollider2D boxCollider2D;
	private Vector3 movementVector;
	private RaycastHit2D hitRaycast;
	#endregion

	#region Data Vars
	Vector3 mousePos;

	#endregion

	#region Weapon
	private WeaponHolder weaponHolder;
	#endregion

	#region Gameplay
	private int Exp, Level, Gold;
	#endregion

	[SerializeField] public float HP, maxHP;
	[SerializeField] GameObject weapon;

	IEnumerator TeleportToSpawn(float time)
	{
		yield return new WaitForSeconds(time);
		moveSpawnPosition();
	}

	void moveSpawnPosition()
	{
		transform.position = spawnPoint.position;
	}

	void Start()
    {
		boxCollider2D = GetComponent<BoxCollider2D>();
		if (SceneManager.GetActiveScene().name == "MazeWorld")StartCoroutine(TeleportToSpawn(0.01f));
		else moveSpawnPosition();
		if (SceneManager.GetActiveScene().name == "Hub") weapon.SetActive(false);
		else weapon.SetActive(true);
	}

	void Awake()
	{
		weaponHolder = GetComponentInChildren<WeaponHolder>();
	}

    void FixedUpdate()
    {
		if (canMove)
		{
			#region Movement
			// Get horizontal and vertical input
			float xInput = Input.GetAxisRaw("Horizontal");
			float yInput = Input.GetAxisRaw("Vertical");

			// Set the movement vector and Normalise it
			movementVector = new Vector2(xInput, yInput).normalized;

			// Flip the player sprite if moving left
			if (xInput < 0)
			{
				GetComponent<SpriteRenderer>().flipX = true;
			}
			// Flip the player sprite if moving right
			else if (xInput > 0)
			{
				GetComponent<SpriteRenderer>().flipX = false;
			}

			hitRaycast = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(0, movementVector.y), Mathf.Abs(movementVector.y * Time.deltaTime), LayerMask.GetMask("Entity", "Blocking"));
			if (hitRaycast.collider == null)
			{
				//Move the player
				transform.Translate(0, movementVector.y * playerSpeed * Time.fixedDeltaTime, 0);
			}

			hitRaycast = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(movementVector.x, 0), Mathf.Abs(movementVector.x * Time.deltaTime), LayerMask.GetMask("Entity", "Blocking"));
			if (hitRaycast.collider == null)
			{
				//Move the player
				transform.Translate(movementVector.x * playerSpeed * Time.fixedDeltaTime, 0, 0);
			}
			#endregion
		}

		weaponHolder.pointerPos = mousePos;
	}

	void Update()
	{
		#region Data
		mousePos = Input.mousePosition;
		#endregion
	}

	public void killPlayer()
	{
		StartCoroutine(FadeInGameOver());
	}

	public void Damage(float dmgAmount)
	{
		float tempHP = HP - dmgAmount;
		if (tempHP <= 0)
		{
			GameObject.Find("HealthBar").GetComponent<Image>().fillAmount = 0;
			killPlayer();
		}
		else if (!(tempHP > maxHP))
		{
			HP = tempHP;
		}
	}

	public void speedUp(float multiplier, int dur)
	{
		StartCoroutine(FasterCoroutine(multiplier, dur));
	}

	IEnumerator FasterCoroutine(float multiplier, int dur)
	{
		playerSpeed = playerSpeed * multiplier;
		yield return new WaitForSeconds((float)dur);
		playerSpeed = playerSpeed / multiplier;
	}

	IEnumerator FadeInGameOver()
	{
		TextMeshProUGUI gameOverText = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();
		gameOverText.alpha = 0f;

		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			gameOverText.alpha = Mathf.Clamp01(t / 1f);
			yield return null;
		}
		gameOverText.alpha = 1f;
		yield return new WaitForSeconds(1f);
		SceneManager.LoadSceneAsync("Hub");
	}

	public int getLevel() { return Level; }
	public int getExp() { return Exp; }
	public void setLevel(int level) { Level = level; }
	public void setExp(int exp) { Exp = exp; }
	public int getGold() { return Gold; }
}
