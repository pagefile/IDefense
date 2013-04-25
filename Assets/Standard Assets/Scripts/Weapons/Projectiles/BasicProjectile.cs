using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour
{
	//=========================================
	// Inspector Variables
	//=========================================
	
	public float	LifeTime;	// Life in seconds
	public float	Speed;		// speed on spawn
	public float	damage;		// how many damages it does
	
	
	//===========================================
	// Unity Overrides
	//===========================================
	
	void Start ()
	{
		rigidbody.velocity = rigidbody.transform.forward * Speed;
	}
	
	void Update ()
	{
		if(LifeTime <= 0.0f)
		{
			Destroy(gameObject);
		}
		LifeTime -= Time.deltaTime;
	}
	
	void OnCollisionEnter(Collision col)
	{
		// Should refactor stuff later to have a DamageableObject base class
		VehicleShip hit = col.gameObject.GetComponent<VehicleShip>();
		if(hit != null)
		{
			hit.Damage(damage);	
		}
		Destroy (gameObject);
	}
}
