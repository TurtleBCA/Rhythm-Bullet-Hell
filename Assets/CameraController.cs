using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static float shakeDecay;
    public static float shakeIntensity;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (shakeIntensity > 0){
            transform.position = new Vector3(0, 0, -10) + Random.insideUnitSphere * shakeIntensity;
            shakeIntensity -= shakeDecay;
        } else {    
            transform.position = new Vector3(0, 0, -10);
            transform.rotation = Quaternion.identity;
        }

        if (Camera.main.backgroundColor != Color.black) {
            Camera.main.backgroundColor -= Color.white * 0.03f;
        }
    }
}
