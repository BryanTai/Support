using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	//public PlayerHealth playerHealth; 
	public GameObject enemy;
	public float spawnTime = 0.1f;
	public Transform[] spawnPoints;
	const string BASICENEMY = "basicenemy";

	void Start() {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);

	}

	void Spawn () {

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		//Transform basicEnemy = Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation) as Transform;
		//basicEnemy.name = BASICENEMY;
		//basicEnemy.parent = GameObject.Find ("Enemies").transform;


		}


}
