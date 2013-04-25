using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour
{
	public float				FireRate;		// Seconds per round
	public Rigidbody			Projectile;		// Projectile to fire
	public Transform			MuzzlePoint;	// Where the projectile spawns (and direction it fires)
	
	private float	reloadTime;
	
	private bool	ctrlTriggerDown;
	
	// Use this for initialization
	void Start ()
	{
		reloadTime = 0.0f;
		if(FireRate <= 0.0f)
		{
			FireRate = 1.0f;	
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		reloadTime -= Time.deltaTime;
		if(reloadTime <= 0.0f && ctrlTriggerDown)
		{
			if(MuzzlePoint == null)
			{
				// well shit. we ain't got no shooty hole
				reloadTime = float.MaxValue;		// No point in trying to shoot again
				return;
			}
			// Fire ze missiles!
			Instantiate(Projectile, MuzzlePoint.transform.position, MuzzlePoint.transform.rotation);
			reloadTime = FireRate;
		}
	}
	
	// Control interface
	public void SetTriggerDown(bool down)
	{
		ctrlTriggerDown = down;
	}
}
