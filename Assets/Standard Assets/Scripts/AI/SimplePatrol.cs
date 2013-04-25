using UnityEngine;
using System.Collections;

public class SimplePatrol : MonoBehaviour
{
	public PatrolPoint[]	patrolPoints;
	public GameObject		spawnShip;
	
	public int				maxAI;
	
	// LET'S BLOW THIS THING UP
	// Note: Let's make a seperate spawner for AI.
	public float			SpawnTime;
	private float			nextSpawnTimer;
	
	
	// Use this for initialization
	void Start ()
	{	
		if(patrolPoints.Length == 0)
		{
			return;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		nextSpawnTimer -= Time.deltaTime;
		if(nextSpawnTimer <= 0.0f)
		{
			GameObject newAI = (GameObject)Instantiate(spawnShip, transform.position, transform.rotation);
			AIPatrol aiScript = newAI.AddComponent<AIPatrol>();
			aiScript.patrolTarget = patrolPoints[0];
			aiScript.deadZone = 0.8f;
			nextSpawnTimer = SpawnTime;
		}
		if(patrolPoints.Length == 0)
		{
			return;
		}
	}
	
	void OnDestroy()
	{
		//EventManager inst = (EventManager)EventManager.Instance;
		//inst.Unsubscribe(_Event.Event_ID.OnAIDeath);
	}
	
	void OnAIDeath(_Event e)
	{
		Debug.Log ("OnAIDeath()");	
	}
}
