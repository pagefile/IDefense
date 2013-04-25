using UnityEngine;
using System.Collections;

// Having the physics for the ship engine in a seperate script should make it easier to allow
// both AI and players to control the same ship prefab as they will be going through the same
// interface.
public class VehicleShip : DamageableObject
{
	//=========================================
	// Inspector Variables
	//=========================================
	
	public float		IdleSpeed;
	public float		MinSpeed;
	public float		MaxSpeed;
	public float		TimeToAccel;
	public float 		TurnRate;
	public float		TurnSpeedRatio;
	public MachineGun	Weapon;
	public Transform 	GunMount;
	
	
	// Ship controls
	private float		ctrlThrottle;
	private float		ctrlStickPitch;
	private float		ctrlStickYaw;
	private	bool		ctrlSkating;
	
	
	// ship attachments
	private MachineGun 	gun;
	
	
	// preserve data through skating
	private float		normalDrag;

	
	void Start ()
	{
		// Init private variables
		ctrlThrottle = 0.5f;
		ctrlStickPitch = 0.0f;
		ctrlStickYaw = 0.0f;
		ctrlSkating = false;
		normalDrag = rigidbody.drag;
		
		// add the gun
		if(GunMount != null)
		{
			gun = (MachineGun)Instantiate (Weapon);
			gun.transform.parent = GunMount;
			gun.transform.position = GunMount.position;
			gun.transform.rotation = GunMount.rotation;
		}
		
	}
	
	//=========================================
	// Unity Overrides
	//=========================================
	void FixedUpdate()
	{
		if(!rigidbody)
		{
			// Nothing to do here
			return;
		}
		
		ApplyEngineForces();
	}
	
	// Applies forward thrust as well as turning forces.
	void ApplyEngineForces()
	{
		float mass = rigidbody.mass;
		Vector3 f;	// forward force
		float p, y; // turning multipliers
		
		// Calculate speed based on throttle
		float min, max, lerp;
		if(ctrlThrottle <= 0.5f)
		{
			min = MinSpeed;
			max = IdleSpeed;
			lerp = ctrlThrottle * 2.0f;
		}
		else
		{
			min = IdleSpeed;
			max = MaxSpeed;
			lerp = (ctrlThrottle - 0.5f) * 2.0f;
		}
		
		// Turning forces
		p = ctrlStickPitch * mass * TurnRate;		// pitch
		y = ctrlStickYaw * mass * TurnRate;			// yaw
		
		// Are we skating?
		if(ctrlSkating)
		{
			// Enable gravity and reduce drag to 0
			rigidbody.useGravity = true;
			rigidbody.drag = 0.0f;
			
		}
		else
		{
			rigidbody.useGravity = false;
			rigidbody.drag = normalDrag;
			
			// calculate forward thrust
			f = (Vector3.forward * (min + ((max - min) * lerp)) * mass) / TimeToAccel;
			rigidbody.AddRelativeForce(f);
				
			// Apply turn speed ratio. Intended to lower turn rate at higher speeds
			// if fThrottle == 1, (TurnSpeedRatio * fThrottle) must == TurnSpeedRatio
			// if fThrottle == 0, (TurnSpeedRatio * fThrottle) must == 1
			p *= 1 - (1 - TurnSpeedRatio) * ctrlThrottle;
			y *= 1 - (1 - TurnSpeedRatio) * ctrlThrottle;
		}
		
		// apply forces
		rigidbody.AddRelativeTorque(Vector3.right * p);		// pitch
		rigidbody.AddRelativeTorque(Vector3.up * y);		// yaw
	}
	
	
	// Accessors
	public float Throttle
	{
		get { return ctrlThrottle; }
		set { ctrlThrottle = value; }
	}
	
	public float Pitch
	{
		get { return ctrlStickPitch; }
		set { ctrlStickPitch = value; }
	}
	
	public float Yaw
	{
		get { return ctrlStickYaw; }
		set { ctrlStickYaw = value; }
	}
	
	public bool Skating
	{
		get { return ctrlSkating; }
		set
		{
			ctrlSkating = value;
			rigidbody.useGravity = value;
			rigidbody.drag = value ? 0.0f : normalDrag;
		}
	}
	
	// Fire gun. Dunno if I want this as an accessor or not yet.
	public void SetFiring(bool f)
	{
		if(gun != null)
		{
			gun.SetTriggerDown(f);	
		}
	}
}
