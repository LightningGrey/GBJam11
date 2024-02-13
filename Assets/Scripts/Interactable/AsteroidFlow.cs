using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class AsteroidFlow : MonoBehaviour
{
	
	bool activeFlow = false;
	
	
	public GameObject player;
	
	public BoxCollider2D range;
	//public float pullStrength = 0.01f;
	
	public float xFlow;
	public float yFlow;
	
	// private void OnTriggerEnter2D(Collider2D other)
	// {
	// 	if (other.CompareTag("Player") && GBManager.Instance.activeControl)
	// 	{
	// 		//var direction = (other.transform.position - gameObject.transform.position).normalized;
			
	// 		other.gameObject.transform.position -= new Vector3(xFlow, yFlow);
	// 	}
		
	// }
	void Start()
	{
		player = GameObject.FindWithTag("Player");
	}
	
	
	
	private void FixedUpdate() 
	{
		if (activeFlow && GBManager.Instance.activeControl)
		{
			player.transform.position -= new Vector3(xFlow, yFlow);	
		}
		
	}
	
	

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag("Player"))
		{
			activeFlow = true;
		}
		
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		
		if (other.CompareTag("Player"))
		{
			activeFlow = false;
		}
		
	}
	
	
}
