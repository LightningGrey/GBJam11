using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;

public class PlayerMovement : MonoBehaviour
{
	private GBConsoleController gb;
	
	
	[Header("References")]
	private Camera cam;
	public PlayerSprite spriteHandler;
	public Rigidbody2D rb;
	
	
	[Header("Variables")]
	public float speed = 0.5f;
	public Vector2 direction = Vector2.zero;
	

	
	
	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log(cam.WorldToScreenPoint(transform.position));
		if (gb.Input.Up)
		{
			direction.y += 1f;
		}
		if (gb.Input.Down)
		{
			direction.y -= 1f;
		}
		if (gb.Input.Left)
		{
			direction.x -= 1f;
		}
		if (gb.Input.Right)
		{
			direction.x += 1f;
		}
		direction = direction.normalized;
		
		spriteHandler.UpdateGameplaySprite(gb.Input.Up, gb.Input.Down, 
			gb.Input.Left, gb.Input.Right);
			
	}
	
	void FixedUpdate() 
	{
		rb.velocity = new Vector2(direction.x * speed * Time.fixedDeltaTime, direction.y * speed * Time.fixedDeltaTime);

		direction = Vector2.zero;
	}
	
	
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log(other.gameObject);
	}

}
