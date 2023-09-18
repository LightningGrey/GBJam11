using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GBTemplate;

public class TilemapRope : MonoBehaviour
{
	public Tilemap tilemap;
	public Transform playerPos;
	public List<TileBase> ropeTiles = new List<TileBase>();
	public Vector3Int lastTile;
	private GBConsoleController gb;
	
	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		Vector3Int lastTile = tilemap.WorldToCell(playerPos.position);
	}

	// Update is called once per frame
	void Update()
	{
		Vector3Int tile = tilemap.WorldToCell(playerPos.position + new Vector3(0f, 0.04f, 0f));
		
		
		if (!tilemap.HasTile(tile))
		{
			// switch()
			// {
			//	tilemap.SetTile(tile, ropeTiles[1]);
			//}
			tilemap.SetTile(tile, ropeTiles[1]);
		}
		
		// if (lastTile != tile && tilemap.HasTile(tile))
		// {
		// 	tilemap.SetTile(tile, null);
		// 	lastTile = tile;
		// }
		// else if (lastTile != tile && !tilemap.HasTile(tile))
		// {
		// 	tilemap.SetTile(tile, ropeTiles[1]);
		// 	lastTile = tile;
		// }
		
	}
}
