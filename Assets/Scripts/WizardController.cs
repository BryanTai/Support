using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

	const float SPEED = 10;
	public const int MAX_HEALTH = 10;
	const string MINON_NAME = "BasicMinion";

	public float jumpHeight;
	public int numJump;
	public bool isJumping = false;

	public Transform commandBubble;
	public Transform minion;
	public Camera mainCamera;
	public Transform healthbar;

	public int maxMinions;
	public int currentMinions = 0;

	public int health = 10;

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

	void MoveH (float horizontalComponent){
		
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
		
		if (other.gameObject.tag == "Platform") {
			// when we collide with ground 
			isJumping = false;
			numJump = 0;
			
		}
		
		Vector3 relativePosition = transform.InverseTransformPoint(other.transform.position);
		bool landedOnTopOfActor = Mathf.Abs (relativePosition.x) < -relativePosition.y;
		
		if (other.gameObject.tag == "Enemy") {
			//Wizard touched enemy

			//Check if we goomba stomped

			if (landedOnTopOfActor){
				//Successful goomba stomp, convert enemy to minion
				ConvertMinion (other.gameObject);
				Debug.Log("STOMP! " + relativePosition );
			} else {
				//Get hurt, lose a health
				// TODO: Activate invincibility frames

				health -= 1;
				
				((BarAnimation) healthbar.gameObject.GetComponent("BarAnimation")).UpdateBar();
				Debug.Log("OW! " + relativePosition );
			}
		}

		// Reset jump if we landed on top of an enemy or minion
		if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Minion") && landedOnTopOfActor) {
			isJumping = false;
		}

	}

	void ConvertMinion (GameObject enemy) {
		if (currentMinions < maxMinions) {
			Vector3 position = enemy.transform.position;

			Destroy(enemy);
			Transform minionObject = Instantiate(minion, position, Quaternion.identity) as Transform;
			minionObject.name = MINON_NAME;
			minionObject.parent = GameObject.Find("Minions").transform;
			currentMinions++;
		}
	}
}
