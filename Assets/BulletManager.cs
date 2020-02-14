using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {
	public GameObject bulletPrefab;
	
	private static Queue<GameObject> pool = new Queue<GameObject>();

	void Start() {
		for (int i = 0; i < 500; i++) {
			GameObject bullet = Instantiate(bulletPrefab);
			bullet.transform.position = new Vector2(1000, 1000);
			pool.Enqueue(bullet);
		}

		InvokeRepeating("spawn", 0f, 0.2f);

	}

	public void SpawnBullet(Vector2 startPosition, Quaternion startRotation, int pathType, float speed) {
		GameObject spawned = pool.Dequeue();
		BulletController bulletController = spawned.GetComponent<BulletController>();

		bulletController.birthTime = TimeManager.SongPosition;
		bulletController.startPosition = startPosition;
		bulletController.startRotation = startRotation;
		bulletController.pathType = pathType;
		bulletController.speed = speed;
	}

	public void spawn() {
		SpawnBullet(
			new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)),
			Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)),
			1,
			Random.Range(-3f,3f));
	}
}
