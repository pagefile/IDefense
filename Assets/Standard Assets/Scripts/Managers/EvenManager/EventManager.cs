using UnityEngine;
using System.Collections;

// Very simple and non-robust event manager. Gonna improve this later - AG 2/15/13

public class EventManager : MonoBehaviour
{
	public delegate	void EventCallback(_Event e);
	
	private ArrayList		eventList;
	private Hashtable		subscribers;
	
	// Use this for initialization
	void Start ()
	{
		eventList = new ArrayList(100);
		subscribers = new Hashtable();
	}
	
	void LateUpdate ()
	{
		foreach(_Event e in eventList)
		{
			// Dispatch the events
			EventCallback call = (EventCallback)subscribers[e.eventID];
			if(call != null)
			{
				call(e);	
			}
		}
		eventList.Clear();
	}
	
	public void Subscribe(_Event.Event_ID id, EventCallback call)
	{
		subscribers.Add (id, call);
	}
	
	public void Unsubscribe(_Event.Event_ID id)
	{
		subscribers.Remove(id);	
	}
	
	public void SendEvent(_Event e)
	{
		eventList.Add (e);	
	}
}
