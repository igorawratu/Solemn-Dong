using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToiletManager : MonoBehaviour {

	bool hittingWater = false;
	AudioSource audioSource;
	public Light light;

	DrunkManager drunkManager;


	// Use this for initialization
	void Start () {
		
		audioSource = GetComponent<AudioSource>();
		drunkManager = GameObject.FindObjectOfType<DrunkManager>();
	}

	// Update is called once per frame
	void Update(){
		
	}

	void OnParticleCollision(GameObject other) {
		audioSource.mute = false;
		light.intensity = Mathf.Lerp(light.intensity, 4f, Time.deltaTime*4);
		
	}

	public void Missing(){
		audioSource.mute = true;
		light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime*4);
		drunkManager.Missing();
	}

}
