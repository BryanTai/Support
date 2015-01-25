using UnityEngine;
using System.Collections;

public class BasicEnemyController : AiActorController
{

	public int enemyRange = 2;
	bool attackMode = false;
	public int direction = 1;

	GameObject wizard;



	// Use this for initialization
	void Start ()
	{
		speed = 2;
		wizard = GameObject.Find ("Wizard");

	}

	// Update is called once per frame
	void Update ()
	{
		if (attackMode) {

			Attack ();

		} else {

			Waddle ();

			CheckForAttack ();

		}

	}

	//Move towards whoever's in range
	void Attack () {
		Move (wizard.transform.position);
	}

	//If Wizard or Minion in range, attack them.
	void CheckForAttack ()
	{
		float distance = Vector3.Distance (wizard.transform.position, this.transform.position);
	
		if (distance < enemyRange) {
			attackMode = true;
		}
	}

	void OnTriggerEnter2D(Collider2D trigger) {
			if (!attackMode && trigger.gameObject.tag == "PlatformBorder") {
				// Turn around when at edge of platform
				direction *= -1;
			} else if (attackMode) {
				
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
