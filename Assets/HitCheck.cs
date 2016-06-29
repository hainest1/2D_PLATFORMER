using UnityEngine;
using System.Collections;

public class HitCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Destructible":
                this.GetComponent<AudioSource>().Play();
                other.gameObject.SetActive(false);
                Debug.Log("Hit destructible");
                Destroy(this.gameObject);
                break;

            case "Edge":
                Destroy(this.gameObject);
                Debug.Log("Hit edge");
                break;

            case "Player":
                // Ignores player
                // Do nothing here, in the future may be able to use physics.ignorecollider
                break;

            default:
                Destroy(this.gameObject);
                Debug.Log("Hit non-actor");
                break;
        }
    }
}
