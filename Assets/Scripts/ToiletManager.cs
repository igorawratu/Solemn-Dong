using UnityEngine;
using System.Collections;

public class ToiletManager : MonoBehaviour {

	bool hittingWater = false;
	AudioSource audioSource;
	public Light light;
	public Material SuccessMaterial;
	public Material StandardMaterial;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
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
	}

}
