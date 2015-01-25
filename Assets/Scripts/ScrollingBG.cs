using UnityEngine;
using System.Collections;

public class ScrollingBG : MonoBehaviour {
	public float scrollspeed=3;
	public float tileSizeZ=70;

	private Vector3 startPosition;
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * scrollspeed, tileSizeZ);
		transform.position = startPosition + Vector3.left * newPosition;
	}
}
