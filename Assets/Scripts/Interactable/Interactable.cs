using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
	BATTERY,
	PART,
	READABLE
}


public class Interactable : MonoBehaviour
{
	
	public int objectID;
	
	public InteractableType type;
	
	public string text = "";
	
	
	// void Interact() 
	// {}

}
