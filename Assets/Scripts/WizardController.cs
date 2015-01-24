using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

	public float speed;

	// Update is called once per frame
	void Update () {
		//Lock Rotations
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.z, 0, 0);

		var translation = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		transform.Translate (translation, 0, 0);
	
	}

	void OnCollisionEnter2D(Collision2D other){
		Debug.Log ("WIZARD TOUCHED A THING");	

		if (other.gameObject.tag == "enemy") {
			//Wizard touched enemy, lose a health
			// Activate invincibility frames
			Debug.Log("OUCH I TOUCHED AN ENEMY");

		}
	}
}
