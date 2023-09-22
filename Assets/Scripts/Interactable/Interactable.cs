using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Interactable : MonoBehaviour
{
	
	public Collider2D interactRange;
	public bool canInteract = false;
	
	
	
	void Interact() {}

}
