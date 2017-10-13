using UnityEngine;
using System.Collections;
using System.Collections.Generic; //allows us to use lists

public class AICarScript : MonoBehaviour {

	public Transform pathGroup;
	public float maxSteer = 15.0f;
	public float maxTorque = 50;
    public float currentSpeed;
    public float topSpeed = 150;
    public float decelerationSpeed = 10;

	public WheelCollider WheelFL;
	public WheelCollider WheelFR;
	public WheelCollider wheelRL;
    public WheelCollider wheelRR;
	public int currentPathObj;
	//public float dir; //test variable
	
	private List<Transform> path; //we use a list so that it can have a dynamic size, meaning the size can change when we need it to
	
	
    public Vector3 centerOfMass;

    public float remainDistance;

    private Rigidbody rb;
	
	void Start ()
	{
		path = new List<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
		
		GetPath();
	}
	
	void GetPath ()
	{
		/*
		Transform[] path_objs = pathGroup.GetComponentsInChildren<Transform>();
		path = new List<Transform>();
		
		for (int i = 0; i < path_objs.Length; i++)
		{
			if (path_objs[i] != pathGroup)
			{
				path.Add(path_objs[i]);
			}
		}
		*/
		Transform[] childObejects = pathGroup.GetComponentsInChildren<Transform>();

        for (int i = 0; i < childObejects.Length; i++)
        {
            Transform temp = childObejects[i];
            if (temp.gameObject.GetInstanceID() != GetInstanceID())
                path.Add(temp);
        }

        Debug.Log(childObejects.Length);
	}
	
	void Update ()
	{
		GetSteer();
		Move();
	}
	
	void GetSteer ()
	{
		Vector3 steerVector = transform.InverseTransformPoint(new Vector3(path[currentPathObj].position.x, transform.position.y, path[currentPathObj].position.z));
		float newSteer = maxSteer * (steerVector.x / steerVector.magnitude);
		//dir = steerVector.x / steerVector.magnitude;
		WheelFL.steerAngle = newSteer;
		WheelFR.steerAngle = newSteer;
		
		if (steerVector.magnitude <= remainDistance)
        {
            currentPathObj++;
        }

        if (currentPathObj >= path.Count)
        {
            currentPathObj = 0;
        }
	}
	
	void Move()
    {
        currentSpeed = 2 * (22 / 7) * wheelRL.radius * wheelRL.rpm * 60 / 1000;
        currentSpeed = Mathf.Round(currentSpeed);

        if (currentSpeed <= topSpeed)
        {
            wheelRL.motorTorque = maxTorque;
            wheelRR.motorTorque = maxTorque;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
        else
        {
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
            wheelRL.brakeTorque = decelerationSpeed;
            wheelRR.brakeTorque = decelerationSpeed;
        }
    }
}
