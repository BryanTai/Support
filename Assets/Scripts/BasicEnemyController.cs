using UnityEngine;
using System.Collections;

public class BasicEnemyController : AiActorController {

	public int enemyRange;
	bool attackMode = false;
	public int direction = 1;

	GameObject wizard;
	GameObject[] minions;

	//In attack mode, this is the wizard/minion that this enemy will chase
	GameObject myTarget; 

	// Use this for initialization
	new void Start () {
		base.Start ();
		speed = 2;
		wizard = GameObject.Find ("Wizard");
	}

	// Update is called once per frame
	new void Update () {
		base.Update ();

		if (attackMode) {
			Attack ();
		} else {
			Waddle ();
			CheckForAttack ();
		}
	}

	//Move towards designated target
	void Attack () {
		if (myTarget == null) {
			attackMode = false;
		} else {
			Move (myTarget.transform.position);
		}
	}

	//If Wizard or Minion in range, attack them.
	void CheckForAttack ()
	{
		float distance;
		GameObject possibleTarget;

		//Find distance to Wizard
		float wizarddist = Vector3.Distance (wizard.transform.position, this.transform.position);
		possibleTarget = wizard;
		distance = wizarddist;

		//Find the closest Minion

		minions = GameObject.FindGameObjectsWithTag ("Minion");
		if (minions.Length != 0) {
			GameObject closestminion = minions[0];
			float miniondist = Mathf.Infinity;
			foreach(GameObject m in minions){
				float tempdist = Vector3.Distance(m.transform.position, this.transform.position);
				if( tempdist < miniondist){
					closestminion = m;
					miniondist = tempdist;
				}
			}

			// Compare wizard distance to minion distance
			if (wizarddist > miniondist){
				distance = miniondist;
				possibleTarget = closestminion;
			}
		}


		//If Wizard/Minion in range, chase them down forever.
		if (distance < enemyRange) {
			Debug.Log("ATTACK! ATTACK!" + possibleTarget.ToString());
			attackMode = true;
			myTarget = possibleTarget;
		}
	}

	new void OnTriggerEnter2D(Collider2D trigger) {
		base.OnTriggerEnter2D (trigger);
		if (!attackMode && trigger.gameObject.tag == "PlatformBorder") {
			// Turn around when at edge of platform
			direction *= -1;
		} else if (attackMode) {
			
		}
	}

	//Basic movement for Basic Enemy
	void Waddle ()
	{
		Vector3 move = new Vector3(direction * speed * Time.deltaTime, 0);
		transform.Translate(move);
	}


}
