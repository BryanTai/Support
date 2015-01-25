using UnityEngine;
using System.Collections;

public class BasicMinionController : AiActorController {

	public enum AiState { STANDBY, MOVE, ATTACK }
	public AiState state = AiState.STANDBY;
	public Animator animator;

	GameObject commandBubble;

	public GameObject targetEnemy;

	// Use this for initialization
	new void Start () {
		base.Start ();
		speed = 3;

		commandBubble = GameObject.Find ("CommandBubble");
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		if (health <= 0)
			Die ();

		LookForCommandBubble ();

		if (state == AiState.ATTACK) {
			SeekTarget();
		} else if (state == AiState.MOVE) {
			MoveToCommandBubble();
		} else if (state == AiState.STANDBY) {
			// TODO: Idle animation?
			// "What do we do now?"
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

	void SeekTarget() {
		
		// Change target to the nearestEnemy
		GameObject nearestEnemy = FindNearestGameObject ("Enemy");
		targetEnemy = (nearestEnemy != null) ? nearestEnemy : targetEnemy;

		if (targetEnemy != null) {
			Move (targetEnemy.transform.position);
		}
	}

	void LookForCommandBubble() {
		CommandBubbleController cbc = (CommandBubbleController) commandBubble.GetComponent ("CommandBubbleController");

		bool isInsideBubble = IsInsideCommandBubble ();

		if (cbc.isActive && isInsideBubble) {
			state = AiState.ATTACK;
		} else if (cbc.isActive && !isInsideBubble) {
			state = AiState.MOVE;
		} else {
			state = AiState.STANDBY;
		}

	}

	bool IsInsideCommandBubble () {
		return commandBubble.collider2D.bounds.Contains (this.transform.position);
	}


	void MoveToCommandBubble() {
		Move (commandBubble.transform.position);
	}

	new void OnCollisionEnter2D(Collision2D other) {
		base.OnCollisionEnter2D (other);
		if (other.gameObject.tag == "Enemy") {
			((BasicEnemyController) other.gameObject.GetComponent ("BasicEnemyController")).health -= 10;
		}
	}

}
