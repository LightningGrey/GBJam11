using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
	private GBConsoleController gb;
	private Camera cam;
	public float speed = 0.5f;
	public Vector3 direction = Vector3.zero;
	public Tilemap tilemap;
	
	public bool timeBuffer = false;
	public float timer = 1f;
	
	
	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log(timeBuffer);
		if (!timeBuffer)
		{
			if (gb.Input.Up)
			{
				direction = new Vector3(0f, 1f);
			}
			if (gb.Input.Down)
			{
				direction = new Vector3(0f, -1f);
			}
			if (gb.Input.Left)
			{
				direction = new Vector3(-1f, 0f);
			}
			if (gb.Input.Right)
			{
				direction = new Vector3(1f, 0f);
			}

			if (direction != Vector3.zero)
			{
				Movement();	
			}
		}
		else
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				timer = 0.3f;
				timeBuffer = false;
			}
		}
		
	}
	
	void Movement()
	{
		transform.position += direction * 0.16f;
		direction = Vector3.zero;
		timeBuffer = true;
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log(other.gameObject);
	}

}
