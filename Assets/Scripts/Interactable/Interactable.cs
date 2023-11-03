using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
	BATTERY,
	PART,
	READABLE,
	SHUTTLE
}


//string
public class Interactable : MonoBehaviour
{
	
	public int objectID;
	
	public InteractableType type;
	
	public List<string> interactString = new List<string>();

}
