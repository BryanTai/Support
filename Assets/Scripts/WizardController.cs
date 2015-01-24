using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

	public float speed;

	public float jumpHeight;
	public int numJump;
	public bool isJumping = false;
	private int MAX_SPEED = 30;


	// Update is called once per frame
	void Update () {
		//Lock Rotations
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.z, 0, 0);
		
		// Keyboard horizontal movement
		if (speed > MAX_SPEED) {
			speed= MAX_SPEED;
		}
		var translationH = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		Debug.Log("magnitude of axis " + Input.GetAxis("Horizontal"));
		transform.Translate (translationH, 0, 0);

		// Keyboard jump movement
		if (Input.GetKeyDown (KeyCode.W)) {
			if(isJumping == false || numJump < 2) {
				// only jump if we are on the ground
				rigidbody2D.AddForce (Vector2.up * jumpHeight);
				isJumping = true;
				numJump++;
			}
		}
	}


void OnCollisionEnter2D(Collision2D other){
	
	if (other.gameObject.tag == "enemy") {
		//Wizard touched enemy, lose a health
		// Activate invincibility frames
	}
	
	if (other.gameObject.tag == "Ground") {
		// when we collide with ground 
		isJumping = false;
		numJump = 0;
		
	}
}
}
