using UnityEngine;
using System.Collections;
using System;

public class MusicManager : MonoBehaviour {
    public float max_volume;
    public float volume_change_speed;

    private AudioSource audio_source;
	// Use this for initialization
	void Start () {
        audio_source = GetComponent<AudioSource>();
        float max_length = audio_source.clip.length - 15f;

        audio_source.Play();

        System.Random rng = new System.Random();
        float start_time = (float)rng.NextDouble() * max_length;

        audio_source.time = start_time;

        audio_source.volume = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Globals.fade_out)
        {
            audio_source.volume = Math.Max(0, audio_source.volume - volume_change_speed * Time.deltaTime);
        }
        else if (audio_source.volume < max_volume)
        {
            audio_source.volume = Math.Min(max_volume, audio_source.volume + volume_change_speed * Time.deltaTime);
        }
	}
}
