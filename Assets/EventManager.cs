using UnityEngine;

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

    public GameObject wallPrefab;
    GameObject leftWall;
    GameObject rightWall;

    private float segment6Start;

    void Update() {
        if (TimeManager.SongPosition > lastSpawned1 + TimeManager.BeatDuration / 2 && TimeManager.measure <= 27) {
            float speed = Random.Range(1f, 3f) * (lastSpawned4 != 0 ? 3*(1+TimeManager.SongPosition-lastSpawned4) : 1);
            Vector2 bottomLeft1 = new Vector2(GameManager.xMax, -GameManager.yMax);
            Vector2 topRight1 = new Vector2(GameManager.xMax, -(GameManager.yMax/2));
            BulletManager.SpawnInRectArea(bottomLeft1, topRight1, 
                Quaternion.Euler(0f, 0f, 180f), 1, speed, 0f);
            
            speed = Random.Range(1f, 3f) * (lastSpawned4 != 0 ? 3*(1+TimeManager.SongPosition-lastSpawned4) : 1);
            Vector2 bottomLeft2 = new Vector2(-GameManager.xMax, GameManager.yMax/2);
            Vector2 topRight2 = new Vector2(-GameManager.xMax, GameManager.yMax);
            BulletManager.SpawnInRectArea(bottomLeft2, topRight2, 
                Quaternion.identity, 1, speed, 0f);
            
            lastSpawned1 = TimeManager.SongPosition;
        }

        if (TimeManager.SongPosition > lastSpawned2 + TimeManager.BeatDuration*4 && TimeManager.measure >= 5 && TimeManager.measure <= 12) {
            float leftBound = Random.Range(-GameManager.xMax, GameManager.xMax - GameManager.xMax/4);
            Vector2 bottomLeft = new Vector2(leftBound, -GameManager.yMax);
            Vector2 topRight = new Vector2(leftBound+GameManager.xMax/4, -GameManager.yMax);
            for (int i = 0; i < 5; i++) {
                BulletManager.SpawnInRectArea(bottomLeft, topRight, Quaternion.Euler(0f, 0f, 90f),
                    2, Random.Range(5f, 10f), Random.Range(0.1f, 0.4f));
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
                BulletManager.SpawnBullet(smileyTransform.position + smileyTransform.right*1.54f, smileyTransform.rotation, 1, 4f, 0f);
                BulletManager.SpawnBullet(smileyTransform.transform.position - smileyTransform.right*1.54f, smileyTransform.rotation * Quaternion.Euler(0f, 0f, 180f), 1, 4f, 0f);
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
                BulletManager.SpawnBurst(smileyTransform.position, 8, Quaternion.Euler(0f, 0f, Random.Range(0f, 180f)), 1, 6f, 0f);
                lastSpawned4 = TimeManager.SongPosition;
            }
        }
        
        if (TimeManager.measure >= 25 && TimeManager.measure <= 27) {
            if (segment5Start == 0) {
                segment5Start = TimeManager.SongPosition;
                leftWall = Instantiate(wallPrefab);
                rightWall = Instantiate(wallPrefab);
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
                BulletManager.SpawnBullet(smileyTransform.position + smileyTransform.right*1.54f, smileyTransform.rotation, 1, 4f, 0f);
                BulletManager.SpawnBullet(smileyTransform.transform.position - smileyTransform.right*1.54f, smileyTransform.rotation * Quaternion.Euler(0f, 0f, 180f), 1, 4f, 0f);
                lastSpawned5_1 = TimeManager.SongPosition;
            }
            
            if (TimeManager.SongPosition > lastSpawned5_2 + TimeManager.BeatDuration) {
                BulletManager.SpawnBurst(smileyTransform.position, 8, Quaternion.Euler(0f, 0f, Random.Range(0f, 180f)), 1, 6f, 0f);
                lastSpawned5_2 = TimeManager.SongPosition;
            }
        }

        if (TimeManager.measure >= 28) {
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

    }
}
