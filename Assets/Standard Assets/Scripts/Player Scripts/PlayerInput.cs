using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	// VERY temporary hack. HHHRRRNGGNGGNGG
	public VehicleShip		controlledShip;
	
	// I need a better place for these	- AG 2/11/13
	public float			IdleFOV;
	public float			MinFOV;
	public float			MaxFOV;
	public float			FOVLerpTime;
	
	private float			ipYaw;
	private float			ipPitch;
	private float			ipThrottle;
	private bool			ipFire1;
	private bool			ipSkate;
	
	// Use this for initialization
	void Start ()
	{
		ipYaw = 0.0f;
		ipPitch = 0.0f;
		ipThrottle = 0.0f;
	}
	
	//===========================================
	// Unity Overrides
	//===========================================
	
	// Update is called once per frame
	void Update ()
	{
		// Get input
		
		// This axis is coded for the 360 controller. Will need to add options later for inverting axese and what not
		ipThrottle = (-Input.GetAxis("Throttle") + 1.0f) * 0.5f;
		
		ipPitch = Input.GetAxis ("Pitch");
		ipYaw = Input.GetAxis ("Yaw");
		ipSkate = Input.GetButton("Skate");
		ipFire1 = Input.GetButton ("Fire1");
		
		// Gonna have to map it somewhere.
		if(controlledShip != null)
		{
			controlledShip.Throttle = ipThrottle;
			controlledShip.Pitch = ipPitch;
			controlledShip.Yaw = ipYaw;
			controlledShip.Skating = ipSkate;
			controlledShip.SetFiring(ipFire1);
		}

		// Calculate FOV
		float min, max, lerp;
		if(ipThrottle <= 0.5f)
		{
			min = MinFOV;
			max = IdleFOV;
			lerp = ipThrottle * 2.0f;
		}
		else
		{
			min = IdleFOV;
			max = MaxFOV;
			lerp = (ipThrottle - 0.5f) * 2.0f;
		}
		float targetFOV = (min + ((max - min) * lerp));
		camera.fieldOfView = (camera.fieldOfView + ((targetFOV - camera.fieldOfView) * Time.deltaTime) / FOVLerpTime);
	}
}
