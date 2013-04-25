using UnityEngine;
using System.Collections;

public class AIPatrol : MonoBehaviour
{
	public PatrolPoint		patrolTarget;
	public float			deadZone;

	
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{	
		// Fly towards the target!
		VehicleShip aiShip = gameObject.GetComponent<VehicleShip>();
		if(aiShip == null)
		{
			return;	
		}
		
		
		// find out where I need to go
		Vector3 toTarget = rigidbody.transform.position - patrolTarget.transform.position;
		toTarget.Normalize();
		float dot = 0.0f;

		// Am I at my destination?
		float toDistSq = (rigidbody.transform.position - patrolTarget.transform.position).sqrMagnitude;
		toDistSq *= toDistSq;
		if(toDistSq <= patrolTarget.RadiusSqr)
		{
			patrolTarget = patrolTarget.next;	
		}
		
		// See if I even need to turn
		dot = Vector3.Dot (toTarget, rigidbody.transform.forward);
		if(1 - dot <= deadZone)
		{
			// Nah dude.
			return;
		}
		
		// to dot right will let me know if I need to turn left or right
		// Negative == turn left, positive == turn right
		dot = Vector3.Dot (toTarget, rigidbody.transform.right);
		if(dot < 0.0f)
		{
			aiShip.Yaw = 0.5f;
		}
		if(dot > 0.0f)
		{
			aiShip.Yaw = -0.5f;
		}
		
		// to do up will let me know if I need to pitch up or down
		// Negative == pitch down, positive == pitch up
		dot = Vector3.Dot (toTarget, rigidbody.transform.up);
		if(dot < 0.0f)
		{
			aiShip.Pitch = -0.5f;
		}
		if(dot > 0.0f)
		{
			aiShip.Pitch = 0.5f;
		}
		
		// Set speed
		aiShip.Throttle = 0.5f;
	}
}
