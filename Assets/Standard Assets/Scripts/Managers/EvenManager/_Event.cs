using UnityEngine;
using System.Collections;

public class _Event
{
	public enum Event_ID
	{
		None,
		OnShipDeath,
		OnAIDeath,
	};
	
	public Event_ID		eventID;
	public GameObject	eventObj;
	public GameObject	eventSender;
	
	public _Event(Event_ID id, GameObject obj, GameObject sender)
	{
		eventID = id;
		eventObj = obj;
		eventSender = sender;
	}
	
	public _Event()
	{
		eventID = Event_ID.None;
		eventObj = eventSender = null;	
	}
}
