using UnityEngine;
using System.Collections;

public class RPGCamFollow : MonoBehaviour {
	public GameObject target;
	public float distance = -10f;

	// Use this for initialization
	void Start () 
	{
		transform.position = new Vector3(target.transform.position.x, target.transform.position.y, distance);

	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 newPos = new Vector3
			(target.transform.position.x, target.transform.position.y, distance);

		//if (newPos > 2 * Vector3.one)
		transform.position = newPos; //* Time.deltaTime;

		//newPos = Vector3.zero;
	
	}
}
