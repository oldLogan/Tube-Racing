using UnityEngine;
using System.Collections;

public class Corpo : MonoBehaviour {

	public float mass;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
