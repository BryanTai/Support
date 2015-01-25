using UnityEngine;
using System.Collections;

public abstract class AiActorController : MonoBehaviour {

	public int speed;
	public int health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void Move(Vector3 target) {
		float deltaX = target.x - transform.position.x;

		int direction;

		if (deltaX > 0) { direction = 1;} 
		else {direction = -1;}
		
		Vector3 attack = new Vector3(direction * 2 * speed * Time.deltaTime, 0);
		transform.Translate(attack);
	}
}
