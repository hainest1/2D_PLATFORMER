using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int hp = 100;
	public PlayerStats playerStats;
	public Transform player;
	public int speed = 10;
	public int rotSpeed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SeekTarget();

		if (hp <= 0)
		{
			this.gameObject.SetActive(false);
			playerStats.AddExperience(Random.Range(13, 25));
			Invoke("Respawn", 1.0f);
		}
			

	}

	void Respawn() {
		hp = 100;
		this.gameObject.SetActive(true);
	}

	void SeekTarget() {
		if (player != null)
		{
			Vector3 dir = player.position - transform.position;
			dir.z = 0.0f;
			if (dir != Vector3.zero)
				transform.rotation = Quaternion.Slerp(transform.rotation,
					Quaternion.FromToRotation(Vector3.right, dir),
					rotSpeed * Time.deltaTime);
			transform.position += (player.position - transform.position).normalized
				* speed * Time.deltaTime;
		}
	}

	public void TakeDamage(int d) {
		hp -= d;
		Debug.Log(d + "dmg -> " + hp + " hp");
	}
}
