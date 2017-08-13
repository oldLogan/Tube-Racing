using UnityEngine;
using System.Collections;

public class Gravidade : MonoBehaviour {

	private PhysicsEngine[] physicsEngineArray;

	private const float g = 6.673e-11f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CalculateGravity(){
		foreach(PhysicsEngine physicsEngineA in physicsEngineArray){
			foreach(PhysicsEngine physicsEngineB in physicsEngineArray){
				if (physicsEngineA != physicsEngineB) {
					Vector3 distancia = physicsEngineA.transform.position - physicsEngineB.transform.position;
					float distQuadrado = Mathf.Pow(distancia.magnitude, 2f);
					float magnitudeGravidade = g * physicsEngineA.mass * physicsEngineB.mass / distQuadrado;
					Vector3 vetorGravidade = magnitudeGravidade * distancia.normalized;

					physicsEngineA.AddForce (vetorGravidade);
				}
			}
		}
	}
}
