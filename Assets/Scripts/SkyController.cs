using UnityEngine;
using System.Collections;

public class SkyController : MonoBehaviour {
	public int materialIndex = 0;
	public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
	public string textureName = "_MainTex";
	Vector2 uvoffset = Vector2.zero;


	// Use this for initialization
	void LateUpdate() {
		uvoffset += (uvAnimationRate * Time.deltaTime);
		if (renderer.enabled)
		{
			Renderer.materials[materialIndex].SetTextureOffset(Texturename, uvoffset);
		}

}
