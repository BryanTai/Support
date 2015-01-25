using UnityEngine;
using System.Collections;

public abstract class AiActorController : MonoBehaviour {

	public int speed;
	public int health = 100;
	public int JUMP_HEIGHT;

	bool inAir;

	public double randomJumpTimer;
	const double MAX_JUMP_TIME = 10;
	const double MIN_JUMP_TIME = 1;

	// Use this for initialization
	protected void Start () {
		RandomizeJumpTimer ();
	}
	
	// Update is called once per frame
	protected void Update () {
		CheckRandomJump ();
	}

	void CheckRandomJump() {
		randomJumpTimer -= Time.deltaTime;

		if (randomJumpTimer <= 0) {
			Jump ();
			RandomizeJumpTimer ();
		}
	}

	void RandomizeJumpTimer() {
		randomJumpTimer = Random.value * (MAX_JUMP_TIME - MIN_JUMP_TIME) + MIN_JUMP_TIME;
		Debug.Log (randomJumpTimer);
	}

	protected void Move(Vector3 target) {
		int xDirection = (target.x > transform.position.x) ? 1 : -1;
		
		Vector3 attack = new Vector3(xDirection * 2 * speed * Time.deltaTime, 0);
		transform.Translate(attack);

		// Jump if target is above
		if (target.y > transform.position.y && !inAir) {
			Jump ();
		}
	}

	protected void OnTriggerEnter2D(Collider2D trigger) {
		if (!inAir && trigger.gameObject.tag == "JumpTrigger") {
			Jump();
		}
	}

	protected void Jump() {
		rigidbody2D.AddForce (Vector2.up * JUMP_HEIGHT);
		inAir = true;
	}

	protected void OnCollisionEnter2D(Collision2D other){ 
		if (other.gameObject.tag == "Platform") {
			inAir = false;
		}
	}
}
