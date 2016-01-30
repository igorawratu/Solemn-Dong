using UnityEngine;
using System.Collections;

public class NotToiletManager : MonoBehaviour {

	ToiletManager toiletManager;

	// Use this for initialization
	void Start () {
		toiletManager = GameObject.Find("ToiletInside_Toilet").GetComponent<ToiletManager>();
	}
	
	// Update is called once per frame
	void OnParticleCollision(GameObject other) {
		toiletManager.Missing();

	}
}
