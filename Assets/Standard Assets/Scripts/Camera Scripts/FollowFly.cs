using UnityEngine;
using System.Collections;

public class FollowFly : MonoBehaviour
{
	[SerializeField] private Transform  target;
    [SerializeField] private float      distance = 15f;
    [SerializeField] private float      height = 5f;
	
	public Vector3		LookAtOffset;
	
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
