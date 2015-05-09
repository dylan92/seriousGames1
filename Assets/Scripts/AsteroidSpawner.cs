using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour {

	public Asteroid[] asteroids;
	public Bounds spawnLocation;
	
	[Header("# of asteroids = log(score) * X")]
	public int x = 50;
	
	private List<Asteroid> spawnedAsteroids;
	
	void OnEnable() {
		spawnedAsteroids = new List<Asteroid>();
		Counter.OnGoalReached += HandleOnGoalReached;
		HandleOnGoalReached(1);
	}
	
	void OnDisable() {
		Counter.OnGoalReached -= HandleOnGoalReached;
	}

	void HandleOnGoalReached (int score)
	{
		score++; // score starts at 0
		while (spawnedAsteroids.Count > 0) {
			spawnedAsteroids[0].Destroy();
		}
		Debug.Log("Spawning " + (int)(Mathf.Log(score) * (float)x) + " asteroids");
		SpawnAsterioids((int)(Mathf.Log(score) * (float)x));
	}
	
	void SpawnAsterioids(int amount) {
		bool side = false;
		for (int i = 0; i < amount; i++) {
			int x = 0;
			if (side) {
				x = (int)(0 - spawnLocation.extents.x / 2f + spawnLocation.center.x);
			} else {
				x = (int)(spawnLocation.extents.x / 2f + spawnLocation.center.x);
			}
			side = !side;
			int y = (int)Random.Range(0 - spawnLocation.extents.y / 2f + spawnLocation.center.y, spawnLocation.extents.y / 2f + spawnLocation.center.y);
			//Debug.Log(new Vector3(x, y, 0));
			spawnedAsteroids.Add(Instantiate(asteroids[Random.Range(0, asteroids.Length - 1)], new Vector3(x, y, 0), Quaternion.identity) as Asteroid);
		}
	}
	
	public void OnDrawGizmosSelected() {
		Gizmos.DrawWireCube(spawnLocation.center, spawnLocation.extents);
	}
}
