using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

    public GameObject bullet;
    public float baseSpawnRate;
    public float spawnRateIncrease;
    public int playerCount;

    public void PlayerCount(int receivedCount)
    {
        playerCount += receivedCount;
    }

    void Start()
    {
        Invoke("SpawnBullet", Mathf.Clamp(baseSpawnRate - (spawnRateIncrease * playerCount), 0, 100));
    }

    void SpawnBullet()
    {
        GameObject instantiatedBullet = GameObject.Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        Invoke("SpawnBullet", Mathf.Clamp(baseSpawnRate - (spawnRateIncrease * playerCount), 0, 100));
    }
}
