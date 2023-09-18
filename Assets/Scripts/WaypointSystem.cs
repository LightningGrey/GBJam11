using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
	
	public Transform playerPos;
	
	public List<Vector3> locations = new List<Vector3>();
	
	
	// Start is called before the first frame update
	void Start()
	{
		locations.Add(playerPos.position);
	}

	// Update is called once per frame
	void Update()
	{
		// if (playerPos.position.x - locations[locations.Count-1].x != 0f && 
		// 	playerPos.position.y - locations[locations.Count-1].y != 0f)
		// {
		// 	locations.Add(playerPos.position);
		// 	Debug.Log(locations.Count + " " + locations[locations.Count-1]);
		// }	
	}
	
	
}
