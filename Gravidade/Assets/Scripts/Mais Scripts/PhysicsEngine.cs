using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsEngine : MonoBehaviour {
	public float mass;				//[Kg]
	public Vector3 velocityVector; 	// [m s^-1]
	public Vector3 netForceVector;	//N[Kg m s^-2]

	private List<Vector3> forceVectorList = new List<Vector3>();
	private PhysicsEngine[] physicsEngineArray;

	private const float g = 6.673e-11f;

	// Use this for initialization
	void Start () {
		SetupThrustTrails ();
		physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		RenderTrails ();
		CalculateGravity ();
		//UpdatePosition ();
	}

	public void AddForce(Vector3 forceVector){
		forceVectorList.Add (forceVector);
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

	void UpdatePosition(){
		//Soma as forças
		netForceVector = Vector3.zero;
		foreach(Vector3 forceVector in forceVectorList){
			netForceVector += forceVector;
		}

		forceVectorList = new List<Vector3> ();//Esvazia a lista

		//Calcula a mudança de posição de acordo com a força
		Vector3 accelerationVector = netForceVector / mass;
		velocityVector += accelerationVector * Time.deltaTime;
		transform.position += velocityVector * Time.deltaTime;
	}


	//Desenhar Linhas:
	public bool showTrails = true;

	private LineRenderer lineRenderer;
	private int numberOfForces;

	// Use this for initialization
	void SetupThrustTrails () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.SetColors(Color.yellow, Color.yellow);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.useWorldSpace = false;
	}

	// Update is called once per frame
	void RenderTrails () {
		if (showTrails) {
			lineRenderer.enabled = true;
			numberOfForces = forceVectorList.Count;
			lineRenderer.SetVertexCount(numberOfForces * 2);
			int i = 0;
			foreach (Vector3 forceVector in forceVectorList) {
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i+1, -forceVector);
				i = i + 2;
			}
		} else {
			lineRenderer.enabled = false;
		}
	}
}

