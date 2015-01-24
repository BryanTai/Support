using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

	public float speed;

	// Update is called once per frame
	void Update () {
		// Keyboard: 
		var translationH = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		var translationV = Input.GetAxis ("Vertical") * speed * Time.deltaTime;
	
		transform.Translate (translationH, 0, 0);
		transform.Translate (0, translationV, 0);
	}

	void FixedUpdate(){
		rigidbody2D.AddForce (Vector2.up *speed* Time.deltaTime);

	}
}
