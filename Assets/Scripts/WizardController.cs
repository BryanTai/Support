using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

	public float speed;

	public float jumpHeight;
	public int numJump;
	public bool isJumping = false;


	// Update is called once per frame
	void Update () {
		// Keyboard horizontal movement
		var translationH = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
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

	void OnCollisionEnter2D(Collision2D col) {
		//http://www.youtube.com/watch?v=t1GB1dl_aj0
		if (col.gameObject.tag == "Ground") {
			// when we collide with ground 
			isJumping = false;
			numJump = 0;

		}
	}
	
}
