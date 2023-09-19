using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
	public Transform playerPos;
	public LineRenderer line;
	public EdgeCollider2D edges;
	
	
	void Awake()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// if (playerPos.position.x - line.GetPosition(line.positionCount-1).x != 0f && 
		// 	playerPos.position.y - line.GetPosition(line.positionCount-1).y != 0f)
		// {
		// 	line.positionCount++;
		// }
		line.SetPosition(line.positionCount-1, playerPos.position);
		SetEdgeColliders();
	}
	
	void SetEdgeColliders()
	{
		List<Vector2> edgesList = new List<Vector2>();
		
		for (int i = 0; i < line.positionCount; i++)
		{
			var point = line.GetPosition(i);
			edgesList.Add(new Vector2(point.x, point.y - 0.7f));
		}
		
		edges.SetPoints(edgesList);
	}
}
