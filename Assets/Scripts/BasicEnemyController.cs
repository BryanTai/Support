using UnityEngine;
using System.Collections;

public class BasicEnemyController : AiActorController {

	public int enemyRange;
	bool attackMode = false;
	public int direction = 1;
	public Animator animator;

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
		if (health <= 0)
			Die ();
		if (attackMode) {
			Attack ();
		} else {
			Waddle ();
			CheckForAttack ();
		}
	}

	void Die() {
		animator.SetBool ("isAlive", false);
		// TODO: FIXME THIS CODE IS A HACK
		StartCoroutine (removeFromScreen (2.0f));
		
		Destroy (this);
	}
	
	private IEnumerator removeFromScreen(float seconds) {
		yield return new WaitForSeconds(seconds);
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
	void CheckForAttack () {
		float distance;
		GameObject possibleTarget;

		//Find distance to Wizard
		float wizarddist = Vector3.Distance (wizard.transform.position, this.transform.position);
		possibleTarget = wizard;
		distance = wizarddist;

		//Find the closest Minion
		GameObject minion = FindNearestGameObject("Minion");

		if (minion != null) {
			float minionDistance = Vector3.Distance(minion.transform.position, this.transform.position);

			// Compare wizard distance to minion distance
			if (wizarddist > minionDistance){
				distance = minionDistance;
				possibleTarget = minion;
			}
		}


		//If Wizard/Minion in range, chase them down forever.
		if (distance < enemyRange) {
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
	void Waddle () {
		// Create target vector speed units to the left/right
		Vector3 target = new Vector3(direction * speed + transform.position.x , transform.position.y);
		Move (target);
	}

	new void OnCollisionEnter2D(Collision2D other) {
		base.OnCollisionEnter2D (other);
		if (other.gameObject.tag == "Minion") {
			((BasicMinionController) other.gameObject.GetComponent ("BasicMinionController")).health -= 10;
		}
	}

}
