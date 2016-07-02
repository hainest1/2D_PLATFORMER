using UnityEngine;
using System.Collections;

public class HitCheck : MonoBehaviour {

    public Transform explosionPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.M))
            this.GetComponent<AudioSource>().Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Destructible":
                other.gameObject.SetActive(false);
                Debug.Log("Hit destructible");
                DoExplode();
                break;

            case "Edge":
                DoExplode();
                Debug.Log("Hit edge");
                break;

            case "Player":
                // Ignores player
                // Do nothing here, in the future may be able to use physics.ignorecollider
                Debug.Log("Hit player");
                break;

            case "End":
                //same as player
                break;

            default:
                DoExplode();
                Debug.Log("Hit non-actor");
                break;
        }
    }

    void OnColliderExit2D(Collider2D other)
    {
        if (other.tag == "Edge")
            DoExplode();
    }

    public void DoExplode()
    {
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<PolygonCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Transform clone;
        clone = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation) as Transform;
        Destroy(clone.gameObject, 1.0f);
        Destroy(this.gameObject, 2.0f);
    }
}