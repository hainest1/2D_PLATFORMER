using UnityEngine;
using System.Collections;

[RequireComponent( typeof (PlayerStats) )]
public class RPGPlayer : MonoBehaviour {
	public float speed = 15f;
	public float runMultiplier = 1f;
	public KeyCode runKey = KeyCode.LeftShift;
	public Sprite upSprite, downSprite, leftSprite, rightSprite;
	public bool tiledMovement = false;
	private float moveX, moveY;

	private PlayerStats stats;
	private SpriteRenderer playerSprite;

	// Use this for initialization
	void Start () {
		moveX = 0f;
		moveY = 0f;
		stats = GetComponent<PlayerStats>();
		playerSprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckInputs();

	}

	void CheckInputs() {
		//Check mouse
		if (Input.GetButtonDown("Fire1"))
			Attack();
			


		//Movement
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");

		//Tiled movement
		if (tiledMovement)
		{
			if (moveY != 0)
				moveX = 0;
			else if (moveX != 0)
				moveY = 0;
		}

		//Sprite change
		if (moveY > 0) playerSprite.sprite = upSprite;
		else if (moveY < 0) playerSprite.sprite = downSprite;
		if (moveX > 0) playerSprite.sprite = rightSprite;
		else if (moveX < 0) playerSprite.sprite = leftSprite;

		//Stat based speed modifier
		float velX;
		float velY;
		if (Input.GetKey(runKey))
		{
			velX = speed * runMultiplier;
			velY = speed * runMultiplier;
		}
		else
		{
			velX = speed;
			velY = speed;
		}
		velX *= (float) (stats.GetSpeed() * .001f) + 1f;
		velY *= (float) (stats.GetSpeed() * .001f) + 1f;

		GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * velX, moveY * velY);


	}

	void Attack() {
		Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, 3.5f);
		if (hits != null) 
		{
			foreach (Collider2D col in hits)
				if (col.tag == "Enemy")
					col.GetComponent<Enemy>().TakeDamage( (int)(stats.GetStrength() * Random.Range(0.85f, 1.15f)) );
		}
	}

}
