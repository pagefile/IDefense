using UnityEngine;
using System.Collections;

public class DeadObjectsList : MonoBehaviour
{
	// Utility class
	private class _deadobj
	{
		public GameObject		obj;
		public float			timeleft;
		
		public _deadobj(GameObject o)
		{
			obj = o;
			timeleft = 1.0f;
		}
	}
	
	// Singleton
   protected static DeadObjectsList instance;
 
   /**
      Returns the instance of this singleton.
   */
   public static DeadObjectsList Instance
   {
      get
      {
         if(instance == null)
         {
            instance = (DeadObjectsList) FindObjectOfType(typeof(DeadObjectsList));
 
            if (instance == null)
            {
               Debug.LogError("An instance of " + typeof(DeadObjectsList) + 
                  " is needed in the scene, but there is none.");
            }
         }
 
         return instance;
      }
   }
	
	// private variables
	private ArrayList		_deadObjects;
	
	// Unity overrides
	void Start ()
	{
		_deadObjects = new ArrayList(100);
	}
	
	void LateUpdate()
	{
		for(int i = 0; i < _deadObjects.Count; i++)
		{
			_deadobj dead = (_deadobj)_deadObjects[i];
			dead.timeleft -= Time.deltaTime;
			if(dead.timeleft <= 0.0f)
			{
				DestroyObject (dead.obj);
				_deadObjects.RemoveAt (i);
				Debug.Log ("Dead Object Deleted");
			}
		}
	}
	
	// Public methods
	public void AddDeadObject(GameObject obj)
	{
		_deadObjects.Add(new _deadobj(obj));
		obj.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
		gameObject.SetActive(false);
	}
}
