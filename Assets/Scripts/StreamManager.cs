using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class StreamManager : MonoBehaviour {
	
	public GameObject myo = null;
	private Pose _lastPose = Pose.Unknown;

	private float _streamPower = 0;
	public float BladderPower = 100;
	public Slider BladderSlider;
	private bool rupturedBladder = false;
	public Color BloodPiss;

	void Start(){
		_streamPower = GetComponent<ParticleSystem>().emissionRate;
	}

	// Update is called once per frame.
	void Update ()
	{
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		if (thalmicMyo.pose == Pose.Fist && !rupturedBladder) {
			//thalmicMyo.Vibrate (VibrationType.Medium);
			GetComponent<ParticleSystem>().emissionRate = Mathf.Lerp(GetComponent<ParticleSystem>().emissionRate, 0, Time.deltaTime*5);
			BladderPower-=0.4f;
			BladderSlider.value = BladderPower/100;
			//ExtendUnlockAndNotifyUserAction (thalmicMyo);
		}
		else
		{
			//thalmicMyo.Vibrate (VibrationType.Medium);
			GetComponent<ParticleSystem>().emissionRate = _streamPower;
			if(BladderPower<100)
				BladderPower+=0.05f;
			BladderSlider.value = BladderPower/100;
			//ExtendUnlockAndNotifyUserAction (thalmicMyo);
		}

		if(BladderPower<1){
			rupturedBladder = true;
			BladderSlider.enabled = false;
			GetComponent<ParticleSystem>().startColor = BloodPiss;
		}

		if (thalmicMyo.pose != _lastPose) {
			_lastPose = thalmicMyo.pose;

			if (thalmicMyo.pose == Pose.Fist) {
				thalmicMyo.Vibrate (VibrationType.Medium);
				//GetComponent<ParticleSystem>().startSpeed = Mathf.Lerp(GetComponent<ParticleSystem>().startSpeed, 0, Time.deltaTime);
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			} else if (thalmicMyo.pose == Pose.WaveIn) {
				//GetComponent<Renderer>().material = waveInMaterial;
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			} else if (thalmicMyo.pose == Pose.WaveOut) {
				//GetComponent<Renderer>().material = waveOutMaterial;
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			} else if (thalmicMyo.pose == Pose.DoubleTap) {
				GetComponent<ParticleSystem>().Play();
//				Debug.Log("Shake");
//				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			}
		}
	}

	// Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
	// recognized.
	void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
	{
		ThalmicHub hub = ThalmicHub.instance;

		if (hub.lockingPolicy == LockingPolicy.Standard) {
			myo.Unlock (UnlockType.Timed);
		}

		myo.NotifyUserAction ();
	}
}
