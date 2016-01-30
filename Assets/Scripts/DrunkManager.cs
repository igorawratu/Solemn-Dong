using UnityEngine;
using System.Collections;

public class DrunkManager : MonoBehaviour {

	public float Speed = 1f;
	public float TimeThingy = 1f;

	bool waiting = false;
	Vector3 randomPosition;
	float moveDelay = 1;

	public Transform Toilet;

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
		
		var rid=GetComponent<Rigidbody>();

		Vector3 viewPos = Camera.main.WorldToViewportPoint(Toilet.position);

		if ((viewPos.x < 0.6F && viewPos.x > 0.4f) && (rid.velocity.x < Speed && rid.velocity.z < Speed)){
			
			rid.AddForce(theForce);	
			Debug.Log("In view");
		}
		else{
			var targetDir = Toilet.transform.position - transform.position;
			var forward = transform.forward;
			var localTarget = transform.InverseTransformPoint(Toilet.transform.position);

			var angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

			var eulerAngleVelocity = new Vector3 (0, angle, 0);
			var deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime );
			rid.MoveRotation(rid.rotation * deltaRotation);

			Debug.Log("Rectifying");
		}

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


