using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

	const float SPEED = 10;

	public float jumpHeight;
	public int numJump;
	public bool isJumping = false;

	public Transform commandBubble;
	public Camera mainCamera;



	// Update is called once per frame
	void FixedUpdate () {
		HandleInput ();
	}

	void HandleInput ()
	{
		// Keyboard horizontal movement

		float horizontalComponent = Input.GetAxis ("Horizontal");
		MoveH (horizontalComponent);

		// Keyboard jump movement
		if (Input.GetKeyDown (KeyCode.W) 
		    || Input.GetKeyDown(KeyCode.UpArrow)) {
			Jump ();
		}


		if (Input.GetMouseButtonDown (0)) {
			//Debug.Log(Input.mousePosition);
			Vector3 commandBubblePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			commandBubblePosition.z = 0;
			CreateCommandBubble (commandBubblePosition);
		}
	}

	void MoveH (float horizontalComponent)
	{
		
		transform.Translate (horizontalComponent* SPEED * Time.deltaTime, 0, 0);
	}

	void Jump ()
	{
		if (isJumping == false || numJump < 2) {
			// only jump if we are on the ground
			rigidbody2D.AddForce (Vector2.up * jumpHeight);
			isJumping = true;
			numJump++;
		}
	}

	void CreateCommandBubble (Vector3 location) {
		CommandBubbleController cbc = (CommandBubbleController) commandBubble.gameObject.GetComponent("CommandBubbleController");
		cbc.Enable (location);
	}

	void OnCollisionEnter2D(Collision2D other){
		
		if (other.gameObject.tag == "enemy") {
			//Wizard touched enemy, lose a health
			// Activate invincibility frames
		}
		
		if (other.gameObject.tag == "Platform") {
			// when we collide with ground 
			isJumping = false;
			numJump = 0;
			
		}
	}

}
