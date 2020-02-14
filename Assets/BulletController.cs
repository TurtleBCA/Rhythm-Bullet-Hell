using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public float birthTime;
    public Vector2 startPosition;
    public Quaternion startRotation;
    public int pathType;
    public float speed;

    private delegate void PathFunction(float speed);
    private PathFunction[] pathChoices;

    private void Start() {
        birthTime = TimeManager.SongPosition;
        pathChoices = new PathFunction[] {
            (speed) => { },
            (speed) => {
                transform.position = startPosition + (TimeManager.SongPosition - birthTime) * speed * (Vector2)transform.right;
                transform.rotation = startRotation * Quaternion.Euler(0f, 0f, (TimeManager.SongPosition - birthTime) * 5);
            }
        };
    }

    void Update() {
        pathChoices[pathType](speed);
    }
}
