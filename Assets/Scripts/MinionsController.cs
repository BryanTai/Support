using UnityEngine;
using System.Collections;

public class MinionsController : MonoBehaviour {
	
	Vector3 newPosition;
	private float xAxis;

	void Start() 
	{
		xAxis = gameObject.transform.position.x;
	}
	
	void Update() 
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray,out hit)){
				newPosition =hit.point;
			}

				
			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, 1);
		}


	}
}
