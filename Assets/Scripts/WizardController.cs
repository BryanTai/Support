using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

	public float speed;

	// Update is called once per frame
	void Update () {
		var translation = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;

		transform.Translate (translation, 0, 0);
	
	}
}
