using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour
{

	public int enemyRange = 2;
	public int speed = 2;
	public float leftLimit, rightLimit;
	bool attackMode = false;
	int direction = 1;



	// Use this for initialization
	void Start ()
	{
		if (leftLimit == 0f && rightLimit == 0f) {
			leftLimit = transform.position.x - enemyRange;
			rightLimit = transform.position.x + enemyRange;
		}
		Debug.Log ("Enemy Range is " + leftLimit + " to " + rightLimit);
	}

	// Update is called once per frame
	void Update ()
	{
		//Lock Rotations
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.z, 0, 0);
		if (attackMode) {

				Attack ();

		} else {

				Waddle ();

				CheckForAttack ();

		}

	}

	//Move towards whoever's in range
	void Attack ()
	{

	}

	//If Wizard or Minion in range, attack them.
	void CheckForAttack ()
	{

	}

	//Basic movement for Basic Enemy
	void Waddle ()
	{
		Vector3 move = new Vector3(direction * speed * Time.deltaTime, 0);
		transform.Translate(move);
		Debug.Log ("Vector3 = " + move);

		if (transform.position.x <= leftLimit) {
				direction = 1;
		} else if (transform.position.x >= rightLimit) {
				direction = -1;
		}
	}


}
