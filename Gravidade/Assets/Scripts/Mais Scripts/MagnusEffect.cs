using UnityEngine;
using System.Collections;

public class MagnusEffect : MonoBehaviour {

	public float magnusConstant = 1f;

	private Rigidbody rigidBody;
	private Vector3 vCross;

	void Start(){
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidBody.AddForce (magnusConstant * Vector3.Cross(rigidBody.angularVelocity, rigidBody.velocity) * Time.deltaTime);
		vCross = Vector3.Cross(rigidBody.angularVelocity, rigidBody.velocity);
		Debug.Log ("Vector3 cross: " + vCross + "\tAngular velocity: " + rigidBody.angularVelocity + "\tVelocity: " + rigidBody.velocity);
	}
}
