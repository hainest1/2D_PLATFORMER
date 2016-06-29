using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Transform projectile;
    public Transform spawnLocation;
    public float bulletForce = 200.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
            Shoot();

    }

    void Shoot()
    {
        Transform clone;
        clone = Instantiate(projectile, spawnLocation.position, spawnLocation.rotation) as Transform;
        //Collider2D cloneCollider = clone.GetComponent<Collider2D>();
        //Collider2D myCollider = GetComponent<Collider2D>();
        // Add force to the cloned object in the object's forward direction
        // Physics.IgnoreCollision(myCollider, cloneCollider);
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletForce, 0), ForceMode2D.Impulse);
        //Destroy (clone, 20.0f);
    }
}
