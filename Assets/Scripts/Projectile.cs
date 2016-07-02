using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Transform rocketProjectile;
    public Transform rocketSpawnLocation;
    public Transform bombProjectile;
    public Transform bombSpawnLocation;
    public float rocketForce = 50.0f;
    public float bombForce = 10.0f;
    public float shotOneCooldown = 1.0f;
    public float shotTwoCooldown = 1.0f;

    private bool shoot1CD = false;
    private bool shoot2CD = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && !shoot1CD)
            Shoot();
        if (Input.GetButtonDown("Fire2") && !shoot2CD)
            DropBomb();

    }

    void Shoot()
    {
        Transform clone;
        clone = Instantiate(rocketProjectile, rocketSpawnLocation.position, rocketSpawnLocation.rotation) as Transform;
        shoot1CD = true;
        PlayShootSound();
        //Collider2D cloneCollider = clone.GetComponent<Collider2D>();
        //Collider2D myCollider = GetComponent<Collider2D>();
        // Add force to the cloned object in the object's forward direction
        // Physics.IgnoreCollision(myCollider, cloneCollider);
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(rocketForce, 0), ForceMode2D.Impulse);
        Destroy (clone.gameObject, 5.0f);
        Invoke("ResetShoot1CD", shotOneCooldown);
    }

    void DropBomb()
    {
        Transform clone;
        clone = Instantiate(bombProjectile, bombSpawnLocation.position, bombSpawnLocation.rotation) as Transform;
        shoot2CD = true;
        PlayShootSound();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        // TODO: Change this to only set the X component of velocity for the bomb
        clone.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity;
        clone.GetComponent<Rigidbody2D>().angularVelocity = player.GetComponent<Rigidbody2D>().angularVelocity;
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(bombForce, 0), ForceMode2D.Impulse);
        Destroy(clone.gameObject, 10.0f);
        Invoke("ResetShoot2CD", shotTwoCooldown);
    }

    void PlayShootSound()
    {
        this.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        this.GetComponent<AudioSource>().Play();
    }

    void ResetShoot1CD()
    {
        shoot1CD = false;
    }

    void ResetShoot2CD()
    {
        shoot2CD = false;
    }
}
