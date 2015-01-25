using UnityEngine;
using System.Collections;

public class CommandBubbleController : MonoBehaviour {

	public const float DURATION = 5;
	public float timeLeft = 0;
	public bool isActive = false;



	// Update is called once per frame
	void Update () {
		if (timeLeft <= 0) {
			Disable ();
		} else {
			timeLeft -= Time.deltaTime;
		}


	}

	void Disable () {
		this.isActive = false;
		renderer.enabled = false;
	}

	public void Enable(Vector3 position) {
		transform.position = position;
		renderer.enabled = true;
		this.isActive = true;

		this.timeLeft = DURATION;
	}
}
