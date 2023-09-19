using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GBTemplate;

public enum Direction 
{
	UP,
	DOWN,
	LEFT,
	RIGHT
}


public class TilemapRope : MonoBehaviour
{
	public Tilemap tilemap;
	public Transform playerPos;
	public List<TileBase> ropeTiles = new List<TileBase>();
	public int tileIndex = 1;
	
	public Direction lastDirection = Direction.DOWN;
	public Direction currentDirection = Direction.DOWN;
	
	public Vector3Int lastTile = Vector3Int.zero;
	public Vector3Int currentTile = Vector3Int.zero;
	private GBConsoleController gb;
	
	
	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		lastTile = tilemap.WorldToCell(playerPos.position);
		currentTile = tilemap.WorldToCell(playerPos.position);
	}

	// Update is called once per frame
	void Update()
	{
			
		if (gb.Input.Up)
		{
			currentTile = tilemap.WorldToCell(playerPos.position);// + new Vector3(0f, -0.08f, 0f));
			tileIndex = 1;
			currentDirection = Direction.UP;
		}
		if (gb.Input.Down)
		{
			currentTile = tilemap.WorldToCell(playerPos.position);// + new Vector3(0f, 0.08f, 0f));
			tileIndex = 1;
			currentDirection = Direction.DOWN;
		}
		if (gb.Input.Left)
		{
			currentTile = tilemap.WorldToCell(playerPos.position);// + new Vector3(0.08f, 0f, 0f));
			tileIndex = 0;
			currentDirection = Direction.LEFT;
		}
		if (gb.Input.Right)
		{
			currentTile = tilemap.WorldToCell(playerPos.position);// + new Vector3(-0.08f, 0f, 0f));
			tileIndex = 0;
			currentDirection = Direction.RIGHT;
		}
		
		Debug.Log(lastTile);
		
		
		if (!tilemap.HasTile(currentTile))
		{
			tilemap.SetTile(currentTile, ropeTiles[tileIndex]);
		}
		
		if (tilemap.HasTile(currentTile))
		{
			if (lastDirection == Direction.DOWN)
			{
				if (currentDirection == Direction.RIGHT)
				{
					tilemap.SetTile(lastTile, ropeTiles[3]);
				}
				else if (currentDirection == Direction.LEFT)
				{
					tilemap.SetTile(lastTile, ropeTiles[2]);	
				}
			}
			
			if (lastDirection == Direction.UP)
			{
				if (currentDirection == Direction.RIGHT)
				{
					tilemap.SetTile(lastTile, ropeTiles[5]);
				}
				else if (currentDirection == Direction.LEFT)
				{
					tilemap.SetTile(lastTile, ropeTiles[4]);	
				}
			}
			
			if (lastDirection == Direction.LEFT)
			{
				if (currentDirection == Direction.UP)
				{
					tilemap.SetTile(lastTile, ropeTiles[3]);
				}
				else if (currentDirection == Direction.DOWN)
				{
					tilemap.SetTile(lastTile, ropeTiles[5]);	
				}
			}
			
			if (lastDirection == Direction.RIGHT)
			{
				if (currentDirection == Direction.UP)
				{
					tilemap.SetTile(lastTile, ropeTiles[2]);
				}
				else if (currentDirection == Direction.DOWN)
				{
					tilemap.SetTile(lastTile, ropeTiles[4]);	
				}
			}
		}
		
		lastTile = currentTile;
		lastDirection = currentDirection;
		
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
