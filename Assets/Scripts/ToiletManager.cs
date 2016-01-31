using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToiletManager : MonoBehaviour {

	bool hittingWater = false;
	AudioSource audioSource;
	public Light light;

	DrunkManager drunkManager;

	float awesomeNumber = 0;


	// Use this for initialization
	void Start () {
		
		audioSource = GetComponent<AudioSource>();
		drunkManager = GameObject.FindObjectOfType<DrunkManager>();
	}

	// Update is called once per frame
	void Update(){

		if(awesomeNumber > 5){
			audioSource.mute = true;
		}
	}

	void OnParticleCollision(GameObject other) {
		awesomeNumber = 0;
		audioSource.mute = false;
		light.intensity = Mathf.Lerp(light.intensity, 4f, Time.deltaTime*4);
		
	}

	public void Missing(){
		awesomeNumber ++;
		light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime*4);
		drunkManager.Missing();
	}
}
