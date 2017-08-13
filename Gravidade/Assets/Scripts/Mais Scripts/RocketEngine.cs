using UnityEngine;
using System.Collections;


[RequireComponent (typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {

	public float fuelMass;
	public float maxThrust;				// KN [Kg m s^-1]

	[Range (0, 1f)]
	public float thrustPercent;
	public Vector3 thrustUnityVector;

	private PhysicsEngine physicsEngine;
	private float currentThrust;

	// Use this for initialization
	void Start () {
		physicsEngine = GetComponent<PhysicsEngine> ();
		physicsEngine.mass += fuelMass;
	}

	// Update is called once per frame
	//physicsEngine.AddForce (thrustUnityVector);
	void FixedUpdate () {
		if (fuelMass > fuelThisUpdate()) {
			fuelMass -= fuelThisUpdate();
			physicsEngine.mass -= fuelThisUpdate ();
			ExertForce();
		} else {
			Debug.LogWarning ("out of fuel");
		}
	}

	float fuelThisUpdate(){
		float exhaustMassFlow;
		float effectiveExhaustVelocity = 4462f;

		exhaustMassFlow = currentThrust / effectiveExhaustVelocity;

		return exhaustMassFlow * Time.deltaTime;
	}

	void ExertForce(){
		currentThrust = thrustPercent * maxThrust * 1000f;
		Vector3 thrustVector = thrustUnityVector.normalized * currentThrust;
		physicsEngine.AddForce (thrustVector);
	}
}
