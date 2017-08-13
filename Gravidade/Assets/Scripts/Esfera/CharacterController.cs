using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float moveSpeed = 15;
	private Vector3 moveDir;

	// Use this for initialization
	void Update () {
		moveDir = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
	}
}
