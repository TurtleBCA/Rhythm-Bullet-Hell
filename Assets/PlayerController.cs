using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    public bool isDashing = false;
    public float dashBoost = 10f;
    public float dashDuration = 0.1f;
    private float dashStartTime;

    public Camera camera;
    public float vertExtent;
    public float horzExtent;

    void Start() {
        vertExtent = camera.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;
        
        speed = 5f;
        isDashing = false;
        dashBoost = 10f;
        dashDuration = 0.1f;
    }
    
    void Update() {
        float xAxis = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -0.2f, 0.2f);
        float yAxis = Mathf.Clamp(Input.GetAxisRaw("Vertical"), -0.2f, 0.2f);
        
        Vector2 translation = Time.deltaTime * speed * (new Vector2(xAxis, yAxis)).normalized;
        transform.Translate(translation);
        
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -horzExtent, horzExtent), 
            Mathf.Clamp(transform.position.y, -vertExtent, vertExtent));


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
