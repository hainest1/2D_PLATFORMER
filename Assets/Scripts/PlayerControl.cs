using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

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
        if (other.tag == "Wall")
        {
            GameOver(0.0f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        if (other.tag == "End")
        {
            float yValue = transform.position.y;
            GameOver(yValue);
        }
        if (other.tag == "Destructible")
        {
            GameOver(0.0f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        if (other.tag == "Coin")
        {
            // Add points?
            Destroy(other.gameObject);
        }
    }

    void AddForceAndControl()
    {
        if (GetComponent<Rigidbody2D>().velocity.x <= maxVelocity * 0.3f)
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

    void GameOver(float y)
    {
        transform.position = new Vector3(0, y, 0);
        GetComponent<TrailRenderer>().Clear();
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        //GetComponent<Rigidbody2D>().Reset

    }
}
