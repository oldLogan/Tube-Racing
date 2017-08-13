using UnityEngine;
using System.Collections;

public class Laucher : MonoBehaviour {

	public float maxLauchSpeed;
	public AudioClip windUpSound, launchSound;
	public PhysicsEngine ballToLaunch;

	public float lauchSpeed;
	private AudioSource audioSource;
	private float extraSpeedPerFrame;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = windUpSound;
		extraSpeedPerFrame = (maxLauchSpeed * Time.fixedDeltaTime) / audioSource.clip.length;
	}

	void OnMouseDown(){
		lauchSpeed = 0;
		InvokeRepeating ("IncreaseLaunchSpeed", 0.5f, Time.fixedDeltaTime);
		audioSource.clip = windUpSound;
		audioSource.Play();
	}

	void OnMouseUp(){
		CancelInvoke ();

		audioSource.Stop ();
		audioSource.clip = launchSound; 
		audioSource.Play ();

		PhysicsEngine newBall = Instantiate (ballToLaunch) as PhysicsEngine;
		newBall.transform.parent = GameObject.Find ("Lauched Balls").transform;
		Vector3 lauchVelocity = new Vector3 (1, 1, 0).normalized * lauchSpeed;
		newBall.velocityVector = lauchVelocity;
	}

	void IncreaseLaunchSpeed(){
		if(lauchSpeed <= maxLauchSpeed){
			lauchSpeed += extraSpeedPerFrame;
		}
	}
}
