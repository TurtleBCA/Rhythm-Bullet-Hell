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
	}

	public static void SpawnBullet(Vector2 startPosition, Quaternion startRotation, int pathType, float speed, float intensity, float offset) {
		GameObject spawned = pool.Dequeue();
		float shade = Random.Range(0f, 0.25f);
		spawned.GetComponent<SpriteRenderer>().color = new Color(shade, shade+Random.Range(0f, 0.5f), 1f);
		BulletController bulletController = spawned.GetComponent<BulletController>();

		bulletController.birthTime = TimeManager.SongPosition;
		bulletController.startPosition = startPosition;
		bulletController.startRotation = startRotation;
		bulletController.pathType = pathType;
		bulletController.speed = speed;
		bulletController.intensity = intensity;
		bulletController.offset = offset;

		pool.Enqueue(spawned);
	}
	
	public static void SpawnInRectArea(Vector2 bottomLeft, Vector2 topRight, Quaternion startRotation, int pathType, float speed, float intensity, float offset) {
		float xPos = Random.Range(bottomLeft.x, topRight.x);
		float yPos = Random.Range(bottomLeft.y, topRight.y);
		
		SpawnBullet(new Vector2(xPos, yPos), startRotation, pathType, speed, intensity, offset);
	}

	public static void SpawnBurst(Vector2 center, int quantity, Quaternion startRotation, int pathType, float speed, float intensity, float offset) {
		for (int i = 0; i < quantity; i++) {
			SpawnBullet(center, startRotation * Quaternion.Euler(0f, 0f, i*360f/quantity), pathType, speed, intensity, offset);
		}
	}
}
