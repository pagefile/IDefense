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
    void Awake ()
	{
		curHealth = MaxHealth;
	}
	
	// public class methods
	public void Damage(float d)
	{
		curHealth -= d;	
	}

    public void Heal(float h) {
        curHealth += h;
        if(curHealth > MaxHealth) {
            curHealth = MaxHealth;
        }
    }
	
    public void Update() {
        if (curHealth <= 0) {
            Object.Destroy(this.transform.gameObject);
        }
    }
}
