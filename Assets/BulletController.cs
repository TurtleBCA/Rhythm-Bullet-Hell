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
    public float intensity;
    public float offset;

    private delegate void PathFunction(float speed, float intensity, float offset);
    private PathFunction[] pathChoices;

    private void Start() {
        birthTime = TimeManager.SongPosition;
        pathChoices = new PathFunction[] {
            (speed, intensity, offset) => { },
            (speed, intensity, offset) => {
                transform.position = startPosition + (TimeManager.SongPosition - birthTime) * speed * (Vector2)transform.right;
                transform.rotation = startRotation;
                // transform.rotation = startRotation * Quaternion.Euler(0f, 0f, (TimeManager.SongPosition - birthTime) * 5);
            },
            (speed, intensity, offset) => {
                transform.position = startPosition + (TimeManager.SongPosition - birthTime) * speed * (Vector2)transform.right +
                                     intensity * Mathf.Sin((TimeManager.SongPosition - birthTime) * speed) * (Vector2)transform.up;
                transform.rotation = startRotation;
            },
            (speed, intensity, offset) => {
                float t = TimeManager.SongPosition - birthTime;
                transform.position = startPosition + Mathf.Sin(t) * Mathf.Cos(Mathf.PI*t*speed+offset) * intensity * (Vector2)transform.right +
                                     Mathf.Sin(t) * Mathf.Sin(t*speed+offset) * intensity * (Vector2)transform.up;
                transform.rotation = startRotation;
            },
            (speed, intensity, offset) => {
                float t = TimeManager.SongPosition - birthTime;
                transform.position = startPosition + Mathf.Sin(t) * Mathf.Cos(t*speed+offset) * intensity * (Vector2)transform.right +
                                     Mathf.Sin(t) * Mathf.Sin(Mathf.PI*t*speed+offset) * intensity * (Vector2)transform.up;
                transform.rotation = startRotation;
            },
            (speed, intensity, offset) => {
                float t = TimeManager.SongPosition - birthTime;
                if (t < TimeManager.BeatDuration * 4) {
                    Vector2 center = Vector2.Lerp(new Vector2(10, 10), startPosition,
                        t / (TimeManager.BeatDuration * 4));
                    transform.position =
                        center + intensity * Mathf.Cos(2 * Mathf.PI * speed * t / (TimeManager.BeatDuration * 4) + offset) *
                        (Vector2) transform.right +
                        intensity * Mathf.Sin(2 * Mathf.PI * speed * t / (TimeManager.BeatDuration * 4) + offset) *
                        (Vector2) transform.up;
                } else {
                    transform.rotation = Quaternion.Euler(0f, 0f, offset * Mathf.Rad2Deg);
                    transform.position = startPosition + (TimeManager.SongPosition - (birthTime + TimeManager.BeatDuration*4)) * 3*speed * (Vector2)transform.right;
                }
            }
        };
    }

    void Update() {
        pathChoices[pathType](speed, intensity, offset);
    }
}
