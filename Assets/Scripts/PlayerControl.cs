using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

    public Transform explosionPrefab;

    public float jumpForce = 10.0f;
    public float horizontalSpeed = 15.0f;
    public float maxVelocity = 100.0f;
    public bool impulseJumpMode = false;
    public bool testingMode = false;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!testingMode)
            AddForceAndControl();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            
            case "Wall":
                //YTeleport(0.0f);
                //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Demise();

                break;
            case "Destructible":
                //YTeleport(0.0f);
                //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Demise();

                break;
            
            case "End":
                float yValue = transform.position.y;
                YTeleport(yValue);
                break;
        }
    }

    void Demise()
    {
        this.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<PolygonCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Transform clone;
        clone = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation) as Transform;
        Destroy(clone.gameObject, 1.5f);
        Invoke("ReloadMenu", 1.5f);
    }

    void ReloadMenu()
    {
        Destroy(GameObject.Find("UI"));
        SceneManager.LoadScene(0);

    }

    void AddForceAndControl()
    {
        if (GetComponent<Rigidbody2D>().velocity.x <= maxVelocity * 0.3f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(20 * horizontalSpeed * Time.deltaTime, 0));
        }
        else if (GetComponent<Rigidbody2D>().velocity.x <= maxVelocity * 0.6f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(10 * horizontalSpeed * Time.deltaTime, 0));
        }
        else if (GetComponent<Rigidbody2D>().velocity.x <= maxVelocity)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.Space))
            JumpCheck();
    }

    void JumpCheck()
    {
        if(impulseJumpMode)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
        else
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * Time.deltaTime));
    }

    void YTeleport(float y)
    {
        transform.position = new Vector3(0, y, 0);
        GetComponent<TrailRenderer>().Clear();
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        //GetComponent<Rigidbody2D>().Reset

    }
}
