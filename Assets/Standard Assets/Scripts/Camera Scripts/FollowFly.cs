using UnityEngine;
using System.Collections;

public class FollowFly : MonoBehaviour
{
	public Transform	target;
	public float		distance;
	public float		height;
	
	public Vector3		LookAtOffset;
	
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		if(!target)
		{
			return;
		}
		
		// quick n dirty for now
		Vector3 vPos = -target.transform.TransformDirection(Vector3.forward) * distance;
		Vector3 vUp = target.transform.TransformDirection (Vector3.up) * height;
		vPos += vUp;

		transform.position = target.transform.position + vPos;
		transform.LookAt(target.transform.position + LookAtOffset);
	}
}
