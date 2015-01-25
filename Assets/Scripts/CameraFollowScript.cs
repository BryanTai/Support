using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

	private float smoothing = 8f;        

	public float maxX;	// input
	public float maxY;	// input
	public float minX;	// input
	public float minY;	// input
	
	private Transform player;        // Reference to the player's transform.

	void Awake ()
	{
		player = GameObject.Find("Wizard").gameObject.transform;//this.transform.parent.gameObject.transform;
	}

	void FixedUpdate ()
	{
		TrackPlayer();
	}

	void TrackPlayer ()
	{
		// current camera position
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// new targetX
		targetX = Mathf.Lerp(transform.position.x, player.position.x, smoothing * Time.deltaTime);
		
		// new targetY
		targetY = Mathf.Lerp(transform.position.y, player.position.y, smoothing * Time.deltaTime);
		
		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minX, maxX);
		targetY = Mathf.Clamp(targetY, minY, maxY);
		
		// Set the camera's position;
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
