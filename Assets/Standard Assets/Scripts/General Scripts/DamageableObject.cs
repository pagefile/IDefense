using UnityEngine;
using System.Collections;

public class DamageableObject : BaseBehaviour
{
	//=========================================
	// Inspector Variables
	//=========================================
	public float		MaxHealth;
	
	
	// Private variables
	private float		curHealth;
	
	
	// Accessors
	private float		Health
	{
		get { return curHealth; }	
	}
	
	
	//=========================================
	// Unity Overrides
	//=========================================
	
	// Use this for initialization
	void Start ()
	{
		curHealth = MaxHealth;
	}
	
	// public class methods
	public void Damage(float d)
	{
		curHealth -= d;	
	}
	
	// TODO: Either in Update or FixedUpdate add this to the dead objects list - AG 2/18/13
}
