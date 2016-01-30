using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToiletManager : MonoBehaviour {

	bool hittingWater = false;
	AudioSource audioSource;
	public Light light;

	public float DickFactor = 0;
	float maxDickFactor = 2000f;
	Slider dickSlider;

	// Use this for initialization
	void Start () {
		//dickSlider = GameObject.Find("Dick-o-meter").GetComponent<Slider>();
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update(){
		//dickSlider.value = (DickFactor/maxDickFactor);
		if(DickFactor/maxDickFactor>0.99f){
			GameObject.Find("AnchorCubeTop").SetActive(false);
			Destroy(this);
		}
	}

	void OnParticleCollision(GameObject other) {
		audioSource.mute = false;
		light.intensity = Mathf.Lerp(light.intensity, 4f, Time.deltaTime*4);
		
	}

	public void Missing(){
		audioSource.mute = true;
		light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime*4);
		DickFactor++;
	}

}
