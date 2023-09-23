using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
	[Header("References")]
	public GameObject scrollPoint;
	public Transform playerPos;

	[Header("Camera Scroll Values")]
	public float min = 0.0f;
	public float max = 1.0f;
	public float offset = 0.5f; 
	

	// Update is called once per frame
	void Update()
	{
		//move scroll point
		scrollPoint.transform.position = new Vector3(0.0f,
			Mathf.Clamp(playerPos.position.y - offset, min, max), 0.0f);
	}
	
}
