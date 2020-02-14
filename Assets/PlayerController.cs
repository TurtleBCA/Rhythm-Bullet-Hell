using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    public bool isDashing;
    public float dashBoost = 10f;
    public float dashDuration = 0.1f;
    private float dashStartTime;

    public bool isInvincible;
    
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        speed = 5f;
        isDashing = false;
        dashBoost = 8f;
        dashDuration = 0.075f;
    }
    
    void Update() {
        if (isInvincible) {
            spriteRenderer.color = Color.red;
        } else if (isDashing) {
            spriteRenderer.color = Color.white;
        }
        else {
            spriteRenderer.color = Color.cyan;
        }

        float xAxis = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -0.2f, 0.2f);
        float yAxis = Mathf.Clamp(Input.GetAxisRaw("Vertical"), -0.2f, 0.2f);
        
        Vector2 translation = Time.deltaTime * speed * (new Vector2(xAxis, yAxis)).normalized;
        transform.Translate(translation);
        
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -GameManager.xMax + 0.129f, GameManager.xMax - 0.129f), 
            Mathf.Clamp(transform.position.y, -GameManager.yMax + 0.129f, GameManager.yMax - 0.129f));


        if (isDashing && Time.time > dashStartTime + dashDuration) {
            speed /= dashBoost;
            isDashing = false;
        }

        if (!isDashing && Input.GetKeyDown(KeyCode.Space)) {
            speed *= dashBoost;
            dashStartTime = Time.time;
            isDashing = true;
        }
    }
}
