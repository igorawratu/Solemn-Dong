using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class StreamManager : MonoBehaviour {
	
	public GameObject myo = null;
	private Pose _lastPose = Pose.Unknown;

	private float _streamPower = 0;

	void Start(){
		_streamPower = GetComponent<ParticleSystem>().emissionRate;
	}

	// Update is called once per frame.
	void Update ()
	{
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		if (thalmicMyo.pose == Pose.Fist) {
			//thalmicMyo.Vibrate (VibrationType.Medium);
			GetComponent<ParticleSystem>().emissionRate = Mathf.Lerp(GetComponent<ParticleSystem>().emissionRate, 0, Time.deltaTime*5);
			//ExtendUnlockAndNotifyUserAction (thalmicMyo);
		}
		else
		{
			//thalmicMyo.Vibrate (VibrationType.Medium);
			GetComponent<ParticleSystem>().emissionRate = _streamPower;
			//ExtendUnlockAndNotifyUserAction (thalmicMyo);
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
