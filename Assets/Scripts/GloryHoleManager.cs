using UnityEngine;
using System.Collections;

public class GloryHoleManager : MonoBehaviour {

	DrunkManager drunkManager;
	bool used = false;
	public ParticleSystem particles;
	public AudioSource audio;

	// Use this for initialization
	void Start () {
		drunkManager = GameObject.FindObjectOfType<DrunkManager>();
	}
	
	// Update is called once per frame
	void OnParticleCollision(GameObject other) {
		if(!used){
			used = true;
			drunkManager.Holed();
			Debug.Log("Holed");
			particles.Play();
			audio.Play();
		}
	}
}
