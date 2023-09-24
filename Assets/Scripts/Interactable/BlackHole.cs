using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
	
	public BoxCollider2D range;
	public float pullStrength = 0.01f;
	
	public void Start()
	{

	}
	
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			var direction = (other.transform.position - gameObject.transform.position).normalized;
			var distance = Vector3.Distance(other.transform.position, gameObject.transform.position);
		
			var distMultiplier = Mathf.Clamp(range.size.x - distance, 0.01f, 0.6f);
			
			other.gameObject.transform.position -= distMultiplier * pullStrength * direction;
		}
		
	}
	
	
}
