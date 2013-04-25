using UnityEngine;
using System.Collections;

// Very simple contraint script. Currenntly tries to constrain the axis completely. More
// functionality will be added later

// Unifinished. Not what I wanted for the ships. - AG 2/11/13
public class RollConstraint : MonoBehaviour
{
	public float		AntiRollForce;
	public bool			Disabled;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void FixedUpdate()
	{
		if(!rigidbody || Disabled)
		{
			return;	
		}
		
		// Get the angle between the ship's right and the world up. Anything non-zero (with a dead zone)
		// needs correcting
		float dot = Vector3.Dot (Vector3.up, rigidbody.transform.right);
		rigidbody.AddRelativeTorque(Vector3.forward * AntiRollForce * rigidbody.mass * -dot);
	}
	
	Vector3 CalculateAxisForce(Vector3 axis, Vector3 crossAxis, float force)
	{
		Vector3 torqueAxis = Vector3.Cross(crossAxis, axis);
		torqueAxis *= rigidbody.mass * rigidbody.mass * force;
		torqueAxis = rigidbody.transform.InverseTransformPoint(torqueAxis);
		torqueAxis.x = 0.0f;
		return torqueAxis;
	}
}
