using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public float IdleSpeed;
	public float MinSpeed;
	public float MaxSpeed;
	public float TimeToAccel;
	public float TurnRate;
	
	public Camera ShipCam;
	public float IdleFOV;
	public float MinFOV;
	public float MaxFOV;
	public float FOVLerpTime;
	
	private float fThrottle;
	private float fPitch;
	private float fYaw;
	
	// Use this for initialization
	void Start () 
	{
		fThrottle = 0.0f;
		fPitch = 0.0f;
		fYaw = 0.0f;
		
		if(IdleSpeed <= 0.0f)
		{
			IdleSpeed = 100.0f;	
		}
		if(MinSpeed <= 0.0f)
		{
			MinSpeed = 10.0f;	
		}
		if(MaxSpeed <= 0.0f)
		{
			MaxSpeed = 200.0f;	
		}
		
		if(IdleFOV <= 0.0f)
		{
			IdleFOV = 60.0f;	
		}
		if(MinFOV <= 0.0f)
		{
			MinFOV = IdleFOV;	
		}
		if(MaxFOV <= 0.0f)
		{
			MaxFOV = IdleFOV;	
		}
		if(TimeToAccel <= 0.0f)
		{
			TimeToAccel = 1.0f;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Get input
		fThrottle = (-Input.GetAxis("Throttle") + 1.0f) * 0.5f;
		fPitch = Input.GetAxis ("Vertical");
		fYaw = Input.GetAxis ("Horizontal");
		
		// Need to put this lerp stuff into a function - AG 2/11/13
		
		// Calculate FOV
		float min, max, lerp;
		if(fThrottle <= 0.5f)
		{
			min = MinFOV;
			max = IdleFOV;
			lerp = fThrottle * 2.0f;
		}
		else
		{
			min = IdleFOV;
			max = MaxFOV;
			lerp = (fThrottle - 0.5f) * 2.0f;
		}
		float targetFOV = (min + ((max - min) * lerp));
		ShipCam.fieldOfView = (ShipCam.fieldOfView + ((targetFOV - ShipCam.fieldOfView) * Time.deltaTime) / FOVLerpTime);
	}
	
	void FixedUpdate()
	{
		// Calculate Forces
		// Forward force
		float massSq = rigidbody.mass * rigidbody.mass;
		
		// Calculate speed based on throttle
		float min, max, lerp;
		if(fThrottle <= 0.5f)
		{
			min = MinSpeed;
			max = IdleSpeed;
			lerp = fThrottle * 2.0f;
		}
		else
		{
			min = IdleSpeed;
			max = MaxSpeed;
			lerp = (fThrottle - 0.5f) * 2.0f;
		}
		
		Vector3 f = (Vector3.forward * (min + ((max - min) * lerp)) * massSq) / TimeToAccel;
		
		// Turning forces
		float p = fPitch * massSq * rigidbody.angularDrag * TurnRate;		// pitch
		float y = fYaw * massSq * rigidbody.angularDrag * TurnRate;			// yaw
		
		// upright force
		Vector3 upTorqueAxis = Vector3.Cross(Vector3.up, rigidbody.transform.up);
		upTorqueAxis *= massSq * 100.0f;
		upTorqueAxis = rigidbody.transform.InverseTransformPoint(upTorqueAxis);
		upTorqueAxis.x = 0.0f;
		
		
		// apply forces
		rigidbody.AddRelativeForce(f * Time.deltaTime);
		rigidbody.AddRelativeTorque(Vector3.right * p * Time.deltaTime);
		rigidbody.AddTorque(Vector3.up * y * Time.deltaTime);
		//rigidbody.AddRelativeTorque(-upTorqueAxis * Time.deltaTime);
	}
}
