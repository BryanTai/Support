using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour
{

	public int enemyRange = 2;
	public int speed = 2;
	float leftLimit, rightLimit;
	bool attackMode = false;
	public int direction = 1;

	GameObject wizard;



	// Use this for initialization
	void Start ()
	{

		wizard = GameObject.Find ("Wizard");

		Debug.Log ("Wizard is at " + wizard.transform.position.x);

		//if (leftLimit == 0f && rightLimit == 0f) {
			leftLimit =  transform.position.x - enemyRange;
			rightLimit = transform.position.x + enemyRange;
		//}
		Debug.Log ("Enemy Range is " + leftLimit + " to " + rightLimit);
	}

	// Update is called once per frame
	void Update ()
	{
		//Lock Rotations
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.z, 0, 0);
		if (attackMode) {

			Attack ();

		} else {

			Waddle ();

			CheckForAttack ();

		}

	}

	//Move towards whoever's in range
	void Attack ()
	{
		float deltaX = wizard.transform.position.x - transform.position.x;

		if (deltaX > 0) { direction = 1;} 
		else {direction = -1;}

		Vector3 attack = new Vector3(direction * 2 * speed * Time.deltaTime, 0);
		transform.Translate(attack);
		Debug.Log ("Attack Vector = " + attack);

	}

	//If Wizard or Minion in range, attack them.
	void CheckForAttack ()
	{
		float distance = Mathf.Abs(wizard.transform.position.x - transform.position.x);
		if (distance < enemyRange) {
			attackMode = true;
			Debug.Log("ATTACK! ATTACK!");
		}
	}

	void OnTriggerEnter2D(Collider2D trigger) {
		Debug.Log ("collided with: " + trigger.gameObject.tag);
		if (!attackMode && trigger.gameObject.tag == "PlatformBorder") {
			// Turn around when at edge of platform
			direction *= -1;
		}
	}

	void OnCollisionEnter2D(Collision2D other){

	}

	//Basic movement for Basic Enemy
	void Waddle ()
	{
		Vector3 move = new Vector3(direction * speed * Time.deltaTime, 0);
		transform.Translate(move);
	}


}
