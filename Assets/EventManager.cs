using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EventManager : MonoBehaviour {
    private float lastSpawned1;
    private float lastSpawned2;

    public GameObject smileyObstacle;
    public GameObject smileyFace;
    private float segment3Start;
    private float lastSpawned3;

    private float segment4Start;
    public float lastSpawned4;

    private float segment5Start;
    private float lastSpawned5_1;
    private float lastSpawned5_2;

    public GameObject closingWallPrefab;
    GameObject leftWall;
    GameObject rightWall;

    public GameObject wallPrefab;
    private float segment6Start;

    private float segment7Start;
    private float lastSpawned7_1;
    private float lastSpawned7_2;
    
    private float segment8Start;
    private float lastSpawned8;
    
    private float segment9Start;
    private Vector2 startLerp;

    private float lastSpawned10;

    private float segment11Start;
    private bool spawnedFirstRing;
    private bool spawnedSecondRing;
    private float lastSpawned11;

    void Update() {
        if (TimeManager.SongPosition > lastSpawned1 + TimeManager.BeatDuration / 2 && TimeManager.measure <= 27) {
            float speed = Random.Range(1f, 3f) * (lastSpawned4 != 0 ? 3*(1+TimeManager.SongPosition-lastSpawned4) : 1);
            Vector2 bottomLeft1 = new Vector2(GameManager.xMax, -GameManager.yMax);
            Vector2 topRight1 = new Vector2(GameManager.xMax, -(GameManager.yMax/2));
            BulletManager.SpawnInRectArea(bottomLeft1, topRight1, 
                Quaternion.Euler(0f, 0f, 180f), 1, speed, 0, 0f);
            
            speed = Random.Range(1f, 3f) * (lastSpawned4 != 0 ? 3*(1+TimeManager.SongPosition-lastSpawned4) : 1);
            Vector2 bottomLeft2 = new Vector2(-GameManager.xMax, GameManager.yMax/2);
            Vector2 topRight2 = new Vector2(-GameManager.xMax, GameManager.yMax);
            BulletManager.SpawnInRectArea(bottomLeft2, topRight2, 
                Quaternion.identity, 1, speed, 0f, 0f);
            
            lastSpawned1 = TimeManager.SongPosition;
        }

        if (TimeManager.SongPosition > lastSpawned2 + TimeManager.BeatDuration*4 && TimeManager.measure >= 5 && TimeManager.measure <= 12) {
            float leftBound = Random.Range(-GameManager.xMax, GameManager.xMax - GameManager.xMax/4);
            Vector2 bottomLeft = new Vector2(leftBound, -GameManager.yMax);
            Vector2 topRight = new Vector2(leftBound+GameManager.xMax/4, -GameManager.yMax);
            for (int i = 0; i < 5; i++) {
                BulletManager.SpawnInRectArea(bottomLeft, topRight, Quaternion.Euler(0f, 0f, 90f),
                    2, Random.Range(5f, 10f), Random.Range(0.1f, 0.4f), 0f);
            }

            lastSpawned2 = TimeManager.SongPosition;
        }
        

        if (TimeManager.measure >= 13 && TimeManager.measure <= 20) {
            if (segment3Start == 0) {
                smileyObstacle.SetActive(true);
                segment3Start = TimeManager.SongPosition;
            }
            
            Transform smileyTransform = smileyObstacle.transform;

            float t = TimeManager.BeatDuration*16 - (TimeManager.SongPosition - segment3Start)*0.5f;
            smileyTransform.position = new Vector2(-2*t*Mathf.Cos(2*t), 1.1f*t*Mathf.Sin(2*t));
            float u = TimeManager.SongPosition - segment3Start;
            smileyTransform.rotation = Quaternion.Euler(0f, 0f,
                -180 * Mathf.Cos(Mathf.PI * u / (TimeManager.BeatDuration * 8)) + 180);
            smileyFace.transform.localPosition = new Vector2(0f, -0.05f*Mathf.Cos(Mathf.PI * u / (TimeManager.BeatDuration/2)));

            if (TimeManager.SongPosition > lastSpawned3 + TimeManager.BeatDuration/4) {
                BulletManager.SpawnBullet(smileyTransform.position + smileyTransform.right*1.54f, 
                    smileyTransform.rotation, 1, 4f, 0f, 0f);
                BulletManager.SpawnBullet(smileyTransform.transform.position - smileyTransform.right*1.54f, 
                    smileyTransform.rotation * Quaternion.Euler(0f, 0f, 180f), 1, 4f, 0f, 0f);
                lastSpawned3 = TimeManager.SongPosition;
            }
        }

        if (TimeManager.measure >= 21 && TimeManager.measure <= 24) {
            if (segment4Start == 0) {
                segment4Start = TimeManager.SongPosition;
            }
            
            Transform smileyTransform = smileyObstacle.transform;
            float t = TimeManager.SongPosition - segment4Start;
            smileyTransform.transform.position = new Vector2(0, 0.8f*Mathf.Sin(Mathf.PI * t / (TimeManager.BeatDuration * 4)));
            smileyFace.transform.localPosition = new Vector2(0f, -0.05f*Mathf.Cos(Mathf.PI * t / (TimeManager.BeatDuration/2)));
            
            if (TimeManager.SongPosition > lastSpawned4 + TimeManager.BeatDuration) {
                BulletManager.SpawnBurst(smileyTransform.position, 8, 
                    Quaternion.Euler(0f, 0f, Random.Range(0f, 180f)), 1, 6f, 0f, 0f);
                lastSpawned4 = TimeManager.SongPosition;
            }
        }
        
        if (TimeManager.measure >= 25 && TimeManager.measure <= 27) {
            if (segment5Start == 0) {
                segment5Start = TimeManager.SongPosition;
                leftWall = Instantiate(closingWallPrefab);
                rightWall = Instantiate(closingWallPrefab);
                leftWall.transform.localScale = new Vector2(GameManager.xMax/8/1.29f, GameManager.yMax/1.29f);
                rightWall.transform.localScale = new Vector2(GameManager.xMax/8/1.29f, GameManager.yMax/1.29f);
            }
            
            Transform smileyTransform = smileyObstacle.transform;
            float t = TimeManager.SongPosition - segment5Start;
            smileyTransform.rotation = Quaternion.Euler(0f, 0f, 12*(1 + t)*(1 + t)*(1 + t));
            smileyFace.transform.localPosition = new Vector2(0f, -0.05f*Mathf.Cos(Mathf.PI * t / (TimeManager.BeatDuration/2)));
            
            Vector2 creepPos = Vector2.Lerp(new Vector2(GameManager.xMax+GameManager.xMax/8, 0f), 
                new Vector2(GameManager.xMax-GameManager.xMax/8, 0f), t/(TimeManager.BeatDuration*16));
            leftWall.transform.position = -creepPos;
            rightWall.transform.position = creepPos;

            
            if (TimeManager.SongPosition > lastSpawned5_1 + TimeManager.BeatDuration/4) {
                BulletManager.SpawnBullet(smileyTransform.position + smileyTransform.right*1.54f, 
                    smileyTransform.rotation, 1, 4f, 0f, 0f);
                BulletManager.SpawnBullet(smileyTransform.transform.position - smileyTransform.right*1.54f, 
                    smileyTransform.rotation * Quaternion.Euler(0f, 0f, 180f), 1, 4f, 0f, 0f);
                lastSpawned5_1 = TimeManager.SongPosition;
            }
            
            if (TimeManager.SongPosition > lastSpawned5_2 + TimeManager.BeatDuration) {
                BulletManager.SpawnBurst(smileyTransform.position, 8, 
                    Quaternion.Euler(0f, 0f, Random.Range(0f, 180f)), 1, 6f, 0f, 0f);
                lastSpawned5_2 = TimeManager.SongPosition;
            }
        }

        if (TimeManager.measure == 28) {
            if (segment6Start == 0) {
                segment6Start = TimeManager.SongPosition;
            }
            
            Transform smileyTransform = smileyObstacle.transform;
            float t = TimeManager.SongPosition - segment6Start;
            smileyObstacle.transform.localPosition = Vector2.Lerp(new Vector2(0, 0f),
                new Vector2(0f, -8f), t/(TimeManager.BeatDuration));

            Vector2 creepPos = Vector2.Lerp(new Vector2(GameManager.xMax-GameManager.xMax/8, 0f),
                new Vector2(GameManager.xMax+GameManager.xMax/8, 0f), t/(TimeManager.BeatDuration));
            leftWall.transform.position = -creepPos;
            rightWall.transform.position = creepPos;
        }

        if (TimeManager.measure >= 29 && TimeManager.measure <= 36) {
            if (segment7Start == 0) {
                Destroy(leftWall);
                Destroy(rightWall);
                
                for (int i = 0; i < 8; i++) {
                    BulletManager.SpawnBullet(Vector2.zero, Quaternion.identity, 3, 2, 3, i * Mathf.PI / 4);
                    BulletManager.SpawnBullet(Vector2.zero, Quaternion.identity, 4, 2, 3, i * Mathf.PI / 4);
                }

                segment7Start = TimeManager.SongPosition;
            }
            
            float t = TimeManager.SongPosition - segment7Start;
            if (TimeManager.SongPosition > lastSpawned7_1 + TimeManager.BeatDuration/4) {
                Quaternion spawnRotation = Quaternion.Euler(0f, 0f,
                    -360 * Mathf.Cos(Mathf.PI * t / (TimeManager.BeatDuration * 4)) + 360);
                BulletManager.SpawnBullet(Vector2.zero, spawnRotation, 1, 4f, 0f, 0f);
                lastSpawned7_1 = TimeManager.SongPosition;
            }

            if (TimeManager.SongPosition > lastSpawned7_2 + TimeManager.BeatDuration) {
                GameObject wall = Instantiate(wallPrefab, new Vector2(Random.Range(-GameManager.xMax, GameManager.xMax),0f), Quaternion.identity);
                wall.transform.localScale = new Vector2(Random.Range(1f, 2f)/1.29f, GameManager.yMax / 1.29f);
                float shade = Random.Range(0f, 0.25f);
                wall.GetComponent<SpriteRenderer>().color = new Color(shade, shade+Random.Range(0f, 0.5f), 1f);

                CameraController.shakeDecay = 0.1f;
                CameraController.shakeIntensity = 0.3f;
                shade = Random.Range(0f, 0.0625f);
                Camera.main.backgroundColor = new Color(shade, shade+Random.Range(0f, 0.125f), 0.25f);
                
                lastSpawned7_2 = TimeManager.SongPosition;
            }
        }
        
        if (TimeManager.measure >= 37 && TimeManager.measure <= 43) {
            if (segment8Start == 0) {
                smileyObstacle.SetActive(true);
                segment8Start = TimeManager.SongPosition;
            }
            
            Transform smileyTransform = smileyObstacle.transform;

            float t = TimeManager.BeatDuration*16 - (TimeManager.SongPosition - segment3Start)*0.5f;
            smileyTransform.position = new Vector2(7*Mathf.Sin(1.1f*2*t), 6*Mathf.Cos(1.1f*3*t));
            float u = TimeManager.SongPosition - segment3Start;
            smileyTransform.rotation = Quaternion.Euler(0f, 0f,
                -180 * Mathf.Cos(Mathf.PI * u / (TimeManager.BeatDuration * 8)) + 180);
            smileyFace.transform.localPosition = new Vector2(0f, -0.05f*Mathf.Cos(Mathf.PI * u / (TimeManager.BeatDuration/2)));

            if (TimeManager.SongPosition > lastSpawned8 + TimeManager.BeatDuration/4) {
                float speed = Random.Range(2f, 4f);
                Vector2 bottomLeft1 = new Vector2(GameManager.xMax, -GameManager.yMax);
                Vector2 topRight1 = new Vector2(GameManager.xMax, -(GameManager.yMax/2));
                BulletManager.SpawnInRectArea(bottomLeft1, topRight1, 
                    Quaternion.Euler(0f, 0f, 180f), 1, speed, 0, 0f);
            
                speed = Random.Range(2f, 4f);
                Vector2 bottomLeft2 = new Vector2(-GameManager.xMax, GameManager.yMax/2);
                Vector2 topRight2 = new Vector2(-GameManager.xMax, GameManager.yMax);
                BulletManager.SpawnInRectArea(bottomLeft2, topRight2, 
                    Quaternion.identity, 1, speed, 0f, 0f);

                BulletManager.SpawnBullet(smileyTransform.position + smileyTransform.right*1.54f, 
                    smileyTransform.rotation, 1, 3f, 0f, 0f);
                BulletManager.SpawnBullet(smileyTransform.transform.position - smileyTransform.right*1.54f, 
                    smileyTransform.rotation * Quaternion.Euler(0f, 0f, 180f), 1, 3f, 0f, 0f);
                lastSpawned8 = TimeManager.SongPosition;
            }
        }
        
        if (TimeManager.measure == 44) {
            Transform smileyTransform = smileyObstacle.transform;
            
            if (segment9Start == 0) {
                segment9Start = TimeManager.SongPosition;
                startLerp = smileyTransform.position;
            }
            
            float t = TimeManager.SongPosition - segment9Start;
            smileyObstacle.transform.localPosition = Vector2.Lerp(startLerp,
                new Vector2(0f, -8f), t/(TimeManager.BeatDuration));
        }

        if (TimeManager.measure >= 45 && TimeManager.measure <= 51) {
            if (TimeManager.SongPosition > lastSpawned10 + TimeManager.BeatDuration) {
                Vector2 burstLocation = new Vector2(Random.Range(-GameManager.xMax, GameManager.xMax),
                    Random.Range(-GameManager.yMax, GameManager.yMax));
                for (int i = 0; i < 8; i++) {
                    BulletManager.SpawnBullet(burstLocation, Quaternion.identity, 5, 1, 0.2f, i*Mathf.PI/4);
                }
                
                CameraController.shakeDecay = 0.1f;
                CameraController.shakeIntensity = 0.3f;
                float shade = Random.Range(0f, 0.0625f);
                Camera.main.backgroundColor = new Color(shade, shade+Random.Range(0f, 0.125f), 0.25f);
                
                lastSpawned10 = TimeManager.SongPosition;
            }
        }

        if (TimeManager.measure >= 53 && TimeManager.measure <= 60) {
            if (segment11Start == 0) {
                segment11Start = TimeManager.SongPosition;
            }
            
            if (!spawnedFirstRing && TimeManager.measure == 53 && TimeManager.beat == 1) {
                for (int i = 0; i < 8; i++) {
                    BulletManager.SpawnBullet(Vector2.zero, Quaternion.identity,
                        6, 2*Mathf.PI/(TimeManager.BeatDuration*4), 2, i*Mathf.PI/4);
                }

                spawnedFirstRing = true;
            }
            
            if (!spawnedSecondRing && TimeManager.measure == 53 && TimeManager.beat == 2) {
                for (int i = 0; i < 8; i++) {
                    BulletManager.SpawnBullet(Vector2.zero, Quaternion.identity,
                        6, -2*Mathf.PI/(TimeManager.BeatDuration*4), 2, i*Mathf.PI/4);
                }

                spawnedSecondRing = true;
            }

            float period;
            float t = TimeManager.SongPosition - segment11Start;
            Quaternion rotation;
            if (TimeManager.measure == 53) {
                period = TimeManager.BeatDuration * 8;
                rotation = Quaternion.Euler(0f, 0f, 
                               -180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 54) {
                period = TimeManager.BeatDuration * 8;
                rotation = Quaternion.Euler(0f, 0f, 
                    180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 55 && TimeManager.beat >= 1 && TimeManager.beat >= 2) {
                period = TimeManager.BeatDuration * 4;
                rotation = Quaternion.Euler(0f, 0f, 
                    -180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 55 && TimeManager.beat >= 3 && TimeManager.beat >= 4) {
                period = TimeManager.BeatDuration * 4;
                rotation = Quaternion.Euler(0f, 0f, 
                    180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 56) {
                period = TimeManager.BeatDuration * 8;
                rotation = Quaternion.Euler(0f, 0f, 
                    -180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 57 && TimeManager.beat >= 1 && TimeManager.beat <= 2) {
                period = TimeManager.BeatDuration * 4;
                rotation = Quaternion.Euler(0f, 0f, 
                    180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 57 && TimeManager.beat >= 3 && TimeManager.beat <= 4) {
                period = TimeManager.BeatDuration * 4;
                rotation = Quaternion.Euler(0f, 0f, 
                    -180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 58 && TimeManager.beat >= 1 && TimeManager.beat <= 2) {
                period = TimeManager.BeatDuration * 4;
                rotation = Quaternion.Euler(0f, 0f, 
                    180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 58 && TimeManager.beat >= 3 && TimeManager.beat <= 4) {
                period = TimeManager.BeatDuration * 4;
                rotation = Quaternion.Euler(0f, 0f, 
                    -180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 58 && TimeManager.beat == 1) {
                period = TimeManager.BeatDuration * 2;
                rotation = Quaternion.Euler(0f, 0f, 
                    180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 58 && TimeManager.beat == 2) {
                period = TimeManager.BeatDuration * 2;
                rotation = Quaternion.Euler(0f, 0f, 
                    -180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 58 && TimeManager.beat == 3) {
                period = TimeManager.BeatDuration * 2;
                rotation = Quaternion.Euler(0f, 0f, 
                    180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else if (TimeManager.measure == 58 && TimeManager.beat == 4) {
                period = TimeManager.BeatDuration * 2;
                rotation = Quaternion.Euler(0f, 0f, 
                    -180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            } else {
                period = TimeManager.BeatDuration;
                rotation = Quaternion.Euler(0f, 0f, 
                    180 * Mathf.Cos(2 * Mathf.PI * t / period) + 180);
            }

            if (TimeManager.SongPosition >= lastSpawned11 + TimeManager.BeatDuration / 4) {
                BulletManager.SpawnBurst(Vector2.zero, 4, rotation, 1, 5f, 1, 0f);
                lastSpawned11 = TimeManager.SongPosition;
            }

        }
    }
}
