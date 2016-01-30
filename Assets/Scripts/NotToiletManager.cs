using UnityEngine;
using System.Collections;

public class NotToiletManager : MonoBehaviour {

	ToiletManager toiletManager;
	public bool IsToiletSeat = false;

	// Use this for initialization
	void Start () {
		toiletManager = GameObject.Find("ToiletInside_Toilet").GetComponent<ToiletManager>();
		if(IsToiletSeat)
			StartCoroutine("FlipSeat");
	}

	IEnumerator FlipSeat(){
		yield return new WaitForSeconds(Random.Range(10, 40));
		GetComponent<Animator>().SetBool("LidOpen", false);
		yield return new WaitForSeconds(0.5f);
		GetComponent<Animator>().SetBool("LidOpen", true);
		StartCoroutine("FlipSeat");
	}

	// Update is called once per frame
	void OnParticleCollision(GameObject other) {
		toiletManager.Missing();
		Debug.Log(other.transform.position);
	}
}
