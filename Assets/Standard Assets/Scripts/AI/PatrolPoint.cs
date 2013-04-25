using UnityEngine;
using System.Collections;

public class PatrolPoint : MonoBehaviour
{
	public float		radius;
	public PatrolPoint	next;
	
	public float RadiusSqr
	{
		get { return radius * radius; }	
	}
}
