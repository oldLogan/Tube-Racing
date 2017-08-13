using UnityEngine;
using System.Collections;

public class CilindroAttractor : MonoBehaviour {


	public float gravity = -10;

	public void Attract(Transform body){
		Vector3 gravityBody = new Vector3(body.position.x, body.position.y, 0);
		Vector3 gravityTransform = new Vector3 (transform.position.x, transform.position.y,0);

		Vector3 gravityUp = (gravityBody - gravityTransform).normalized;
		Vector3 bodyUp = body.up;

		body.GetComponent<Rigidbody>().AddForce (gravityUp * gravity);
		Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp (body.rotation,targetRotation,50 * Time.deltaTime);
	}
}
