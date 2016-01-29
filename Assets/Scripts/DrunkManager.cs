using UnityEngine;
using System.Collections;

public class DrunkManager : MonoBehaviour {

	public float Speed = 1f;
	public float TimeThingy = 1f;

	bool waiting = false;
	Vector3 randomPosition;
	float moveDelay = 1;

	void Start(){
		StartCoroutine("ApplyForce");
	}

	void FixedUpdate ()
	{

//		if (waiting == false)
//		{
//			StartCoroutine("LerpX");
//		}
//		else
//		{
//			transform.position = Vector3.Lerp (transform.position, randomPosition, Time.deltaTime*Speed);
//		}


	}

	void Update(){
		GetComponent<Rigidbody>().AddForce(theForce);
	}

	private Vector3 theForce = Vector3.zero;

	IEnumerator ApplyForce(){
		
		yield return new WaitForSeconds(Random.value);
		Debug.Log("Applying force");
		theForce = new Vector3(Random.value*Speed, Random.value*Speed, Random.value*Speed);
		StartCoroutine("ApplyForce");
	}

	IEnumerator LerpX()
	{
		randomPosition = new Vector3 (Random.Range( 4,-4 ), Random.Range( 3, -1 ), Random.Range(2, -1));
		waiting = true;
		yield return new WaitForSeconds (Random.value/TimeThingy);
		waiting = false;
		//Debug.Log ("waited");
	}


}


