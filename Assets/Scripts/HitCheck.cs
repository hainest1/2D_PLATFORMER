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
                Destroy(this.gameObject);
                break;

            case "Edge":
                DoExplode();
                Destroy(this.gameObject);
                Debug.Log("Hit edge");
                break;

            case "Player":
                // Ignores player
                // Do nothing here, in the future may be able to use physics.ignorecollider
                break;

            case "End":
                //same as player
                break;

            default:
                DoExplode();
                Destroy(this.gameObject);
                Debug.Log("Hit non-actor");
                break;
        }
    }

    void DoExplode()
    {
        this.GetComponent<AudioSource>().Play();
        Transform clone;
        clone = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation) as Transform;
        Destroy(clone.gameObject, 1.0f);
    }
}
