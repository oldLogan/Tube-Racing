using UnityEngine;
using System.Collections;

public class Gravidade : MonoBehaviour {

	public float mass;	
	private Corpo[] CorposArray;
	private const float g = 6.673e-11f;

	// Use this for initialization
	void Start () {
		CorposArray = GameObject.FindObjectsOfType<Corpo>();
	}
	
	// Update is called once per frame
	void Update () {
		CalculateGravity ();
	}

	void CalculateGravity(){
		foreach(Corpo corpo in CorposArray){
			Vector3 distancia = corpo.transform.position - transform.position;
			corpo.GetComponent<Rigidbody>().AddForce(distancia.normalized * (distancia.magnitude));
			Quaternion targetRotation = Quaternion.FromToRotation (corpo.transform.up, new Vector3(0, -distancia.y, 0)) * corpo.transform.rotation;
			corpo.transform.rotation = Quaternion.Slerp (corpo.transform.rotation, targetRotation, 50 * Time.deltaTime);
		}
	}
}

/*
		foreach(Gravidade physicsEngineA in physicsEngineArray){
			foreach(Gravidade physicsEngineB in physicsEngineArray){
				if (physicsEngineA != physicsEngineB) {
					Vector3 distancia = physicsEngineA.transform.position - physicsEngineB.transform.position;
					float distQuadrado = Mathf.Pow(distancia.magnitude, 2f);
					float magnitudeGravidade = g * physicsEngineA.mass * physicsEngineB.mass / distQuadrado;
					Vector3 vetorGravidade = magnitudeGravidade * distancia.normalized;
					Debug.Log (vetorGravidade);
					transform.position += vetorGravidade.normalized; 
					//physicsEngineA.GetComponent<Rigidbody>().AddForce(vetorGravidade);
				}
			}
		}
*/
