using UnityEngine;
using System.Collections;

public class CommandBubbleController : MonoBehaviour {

	public const float DURATION = 5;
	public float timeLeft = 0;
	public bool active = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0) {
			Disable ();
		}
	}

	void Disable () {
		this.active = false;
		renderer.enabled = false;
	}

	public void Enable(Vector3 position) {
		transform.position = position;
		renderer.enabled = true;
		this.active = true;

		this.timeLeft = DURATION;
	}
}
