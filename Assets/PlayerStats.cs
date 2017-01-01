using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public int BASE_HEALTH = 10;
	public int BASE_SPIRIT = 10;
	public int BASE_STRENGTH = 10;
	public int BASE_SPEED = 10;
	public int BASE_LEVEL = 1;
	public int LEVEL_CAP = 30;
	public int XP_RATE = 1;
	public int POINTS_PER_LEVEL = 2;

	public Slider xpBar;
	public Text nameText, levelText, healthText, spiritText, strengthText, speedText, skillText;
	public GameObject[] skillUpButtons;

	private int health, spirit, strength, speed, level;
	private float xp;

	private int healthPoints, spiritPoints, strengthPoints, speedPoints, availablePoints;

	// Use this for initialization
	void Start () {
		level = 1;
		healthPoints = 0;
		spiritPoints = 0;
		strengthPoints = 0;
		speedPoints = 0;
		availablePoints = 0;
		CalculateStats();
		nameText.text = "Player 1";
		levelText.text = "Level 1";
		HideSkillUps();

	}

	public int GetHealth() { return health; }
	public int GetSpirit() { return spirit; }
	public int GetStrength() { return strength; }
	public int GetSpeed() { return speed; }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.L))
		{
			AddExperience( Random.Range(80,120) );
		}

		if (Input.GetKeyDown(KeyCode.M) && level < 30)
		{
			AddExperience(3001);
		}

	}

	public void AddExperience(int xp) {
		if (this.xp + xp < 3000)
		{
			this.xp += xp * XP_RATE;
			Debug.Log(xp + "xp added");
		}
		else
			this.xp = 3000;
		CalculateLevel();
		xpBar.value = this.xp - (level - 1)*100;
		xpBar.GetComponentInChildren<Text>().text = "XP: " + this.xp + "/" + (level*100);
	}

	public void LevelUp() {
		if (level < LEVEL_CAP)
		{
			level++;
			Debug.Log("Level UP! " + level);
			levelText.text = "Level " + level;
			availablePoints += POINTS_PER_LEVEL;
			ShowSkillUps();
			CalculateStats();
			CalculateLevel();
		}


	}

	public void CalculateLevel() {
		//Level calculation
		int newLevel =  (int) (xp / 100) + 1;
		if (newLevel > level)
			LevelUp();

	}

	public void CalculateStats() {
		//Health calculation
		health = (int) ( BASE_HEALTH * (Mathf.Pow(1.05f, (float)healthPoints) + 0.5f) );
		healthText.text = "Health: " + health + " (" + healthPoints + ")";

		//Spirit calculation
		spirit = (int) ( BASE_SPIRIT * (Mathf.Pow(1.05f, (float)spiritPoints) + 0.5f) );
		spiritText.text = "Spirit: " + spirit + " (" + spiritPoints + ")";

		//Strength calculation
		strength = (int) ( BASE_STRENGTH * (Mathf.Pow(1.05f, (float)strengthPoints) + 0.5f) );
		strengthText.text = "Strength: " + strength + " (" + strengthPoints + ")";

		//Speed calculation
		speed = (int) ( BASE_SPEED * (Mathf.Pow(1.05f, (float)speedPoints) + 0.5f) );
		speedText.text = "Speed: " + speed + " (" + speedPoints + ")";

		xpBar.GetComponentInChildren<Text>().text = "XP: " + xp + "/" + (level*100);
	}

	public void ShowSkillUps() {
		foreach (GameObject b in skillUpButtons)
		{
			b.SetActive(true);
			skillText.text = availablePoints + " SP to spend";
		}
	}

	public void HideSkillUps() {
		foreach (GameObject b in skillUpButtons)
		{
			b.SetActive(false);
			skillText.text = "";
		}
	}

	public void IncreaseSkill(int skill) {
		if (availablePoints > 0)
		{
			switch (skill)
			{
				case 0:
					healthPoints++;
					break;
				case 1:
					spiritPoints++;
					break;
				case 2:
					strengthPoints++;
					break;
				case 3:
					speedPoints++;
					break;
			}

			availablePoints--;
			CalculateStats();
			if (availablePoints <= 0)
				HideSkillUps();
			else
				ShowSkillUps();
		}
	}

}
