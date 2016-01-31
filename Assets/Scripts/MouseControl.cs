using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseControl : MonoBehaviour {

	public Transform tracer;
	public Transform target;

	public bool MYO = true;

	Toggle toggle;
	// Use this for initialization
	void Start () {
		toggle = GameObject.Find("MYO").GetComponent<Toggle>();
	}
	
	// Update is called once per frame
	void Update () {
		MYO = toggle.isOn;
		GetComponent<JointOrientation>().enabled = MYO;

		if(MYO)
			return;


		var screenPos = Input.mousePosition;
		screenPos.z = 20;
		var worldPos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(screenPos);
		tracer.position = worldPos;

		transform.LookAt(target.position);

		if(Input.GetMouseButtonUp(0)){
			GameObject.Find("TheTip").GetComponent<ParticleSystem>().Play();
			GameObject.Find("TapFingers").SetActive(false);
		}
	}
}
