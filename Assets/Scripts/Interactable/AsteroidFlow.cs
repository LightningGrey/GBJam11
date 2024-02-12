using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class AsteroidFlow : MonoBehaviour
{
	
	public BoxCollider2D range;
	//public float pullStrength = 0.01f;
	
	public float xFlow;
	public float yFlow;
	
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			var direction = (other.transform.position - gameObject.transform.position).normalized;
			
			other.gameObject.transform.position -= new Vector3(xFlow, yFlow);
		}
		
	}
	
	
}
