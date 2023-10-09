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


public class Interactable : MonoBehaviour
{
	
	public int objectID;
	
	public InteractableType type;
	
	
	// void Interact() 
	// {}

}
