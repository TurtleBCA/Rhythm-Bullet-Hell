using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    public float bpm = 170;
    public static float BeatDuration;
    public float offset = 0.2f;
    public static float SongPosition;
    public double startTime;
    public int measure;
    public int beat;
    
    void Start() {
        BeatDuration = 60 / bpm;
        startTime = AudioSettings.dspTime;
        measure = 0;
        beat = 0;
    }
    
    void Update() {
        SongPosition = (float) (AudioSettings.dspTime - startTime) - offset + BeatDuration*2;
        measure = (int)(SongPosition / (BeatDuration * 4));
        beat = ((int)(SongPosition / BeatDuration) % 4) + 1;
    }
}
