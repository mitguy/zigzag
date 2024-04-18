using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
	public GameObject enemyPrefab;
	public float spawnInterval = 2;
	public int maxEnemies = 20;
}


public class SpawnEnemy : GameManagerBehavior {
	public GameObject[] waypoints;
	public Wave[] waves;
	private int wave;
	public int timeBetweenWaves = 5;
	private float lastSpawnTime;
	private int enemiesSpawned = 10;

	private void Start() {
		lastSpawnTime = Time.time;
		wave = 1;
	}

	private void Update() {
		if (wave < waves.Length) {
			float timeInterval = Time.time - lastSpawnTime;
			float spawnInterval = waves[wave].spawnInterval;
			if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
			timeInterval > spawnInterval) &&
			enemiesSpawned < waves[wave].maxEnemies) {
				lastSpawnTime = Time.time;
				GameObject newEnemy = Instantiate(waves[wave].enemyPrefab) as GameObject;
				newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
				enemiesSpawned++;
			}

			if (enemiesSpawned == waves[wave].maxEnemies &&
			GameObject.FindGameObjectWithTag("Enemy") == null) {
				wave++;
				enemiesSpawned = 0;
				lastSpawnTime = Time.time;
			}
		}
	}
}
