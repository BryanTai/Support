using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

	const float SPEED = 10;

	public float jumpHeight;
	public int numJump;
	public bool isJumping = false;

	public Transform commandBubble;
	public Transform minion;
	public Camera mainCamera;

	public int maxMinions;
	public int currentMinions;

	void Start(){
		currentMinions = GameObject.FindGameObjectsWithTag ("Minion").Length;
		maxMinions = 5;
	}

	// Update is called once per frame
	void Update () {
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
			//Wizard touched enemy

			//Check if we goomba stomped
			Vector3 relativePosition = transform.InverseTransformPoint(other.transform.position);

			if (Mathf.Abs(relativePosition.x) < -relativePosition.y){
				//Successful stomp

				//Convert if goomba stomp
				// else, do nothing.

				ConvertMinion (other.gameObject);
				Debug.Log("STOMP! " + relativePosition );
			}else{
				//Get hurt, lose a health
				// Activate invincibility frames

				Debug.Log("OW! " + relativePosition );
			}

		}
		
		if (other.gameObject.tag == "Platform") {
			// when we collide with ground 
			isJumping = false;
			numJump = 0;
			
		}
	}

	void ConvertMinion (GameObject enemy)
	{
		if (currentMinions < maxMinions) {
			Vector3 position = enemy.transform.position;

			// TODO: instantiate minions here
			Destroy(enemy);
			Instantiate(minion, position, Quaternion.identity);
			currentMinions++;
		}
	}
}
