using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject player;
	public PlayerController playerController;
	public float invincibleStartTime;
	public int hitCount;

	public Camera camera;
	public static float yMax;
	public static float xMax;

	private void Start() {
		yMax = camera.orthographicSize;
		xMax = yMax * Screen.width / Screen.height;
		
		print(yMax + " " + xMax);
	}

	private void Update() {
		if (playerController.isInvincible && Time.time > invincibleStartTime + 1f) {
			playerController.isInvincible = false;
		}

		if (!playerController.isInvincible && !playerController.isDashing && Physics2D.OverlapCircle(player.transform.position, 0.129f)) {
			playerController.isInvincible = true;
			invincibleStartTime = Time.time;
			hitCount++;
		}
	}
}
