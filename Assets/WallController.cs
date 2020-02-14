using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {
    public float birthTime;
    public Collider2D collider2d;
    public SpriteRenderer spriteRenderer;
    public Vector2 startPos;
    
    private void Start() {
        birthTime = TimeManager.SongPosition;
        collider2d = GetComponent<Collider2D>();
        collider2d.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,0.35f);
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (TimeManager.SongPosition >= birthTime + TimeManager.BeatDuration && !collider2d.enabled) {
            collider2d.enabled = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,1f);
        }

        if (TimeManager.SongPosition >= birthTime + TimeManager.BeatDuration) {
            transform.position = Vector2.Lerp(startPos + new Vector2(0f, GameManager.yMax*2f), startPos,
                (TimeManager.SongPosition - (birthTime + TimeManager.BeatDuration)) / (TimeManager.BeatDuration / 4f));
        }
        
        if (TimeManager.SongPosition >= birthTime + TimeManager.BeatDuration*2) {
            Destroy(gameObject);
        }
    }
}
