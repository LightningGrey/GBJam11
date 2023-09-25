using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;
using DG.Tweening;
using UnityEngine.Events;


public class PlayerInput : MonoBehaviour
{
	private GBConsoleController gb;
	
	
	[Header("References")]
	private Player player;
	public PlayerSprite spriteHandler;
	public Rigidbody2D rb;
	
	
	[Header("Variables")]
	public float speed = 18f;
	public float speedScale = 1f;
	public Vector2 direction = Vector2.zero;
	//public bool dead = false;
	
	
	public static event UnityAction interact;



	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GBManager.Instance.activeControl && !player.hitstun && !UIManager.Instance.paused)
		{

			//movement code
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


			if (gb.Input.ButtonAJustPressed)
			{
				interact?.Invoke();
			}

			if (gb.Input.ButtonBJustPressed)
			{
				speedScale = 2f;
				GameplayManager.Instance.ChangeSpeed(3f);
			}
			else if (gb.Input.ButtonBJustReleased)
			{
				speedScale = 1f;
				GameplayManager.Instance.ChangeSpeed(1f);
			}


			if (gb.Input.ButtonStartJustPressed)
			{
			
				UIManager.Instance.PauseMenu();
		
			}


			spriteHandler.UpdateGameplaySprite(gb.Input.Up, gb.Input.Down,
				gb.Input.Left, gb.Input.Right);
		}
		else
		{
			if (speedScale > 1f)
			{
				speedScale = 1f;
				GameplayManager.Instance.ChangeSpeed(1f);
			}
		}

	}

	void FixedUpdate() 
	{
		rb.velocity = new Vector2(direction.x, direction.y) * speed * 
			speedScale * Time.fixedDeltaTime;
		
		// var velocity = rb.velocity;
		// 
		// rb.MovePosition(Vector2.SmoothDamp(transform.position, new Vector2(rb.transform.position.x, rb.transform.position.y),
		// ref velocity, 1f));
		
		//rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref refVel, smoothVal);

		direction = Vector2.zero;
	}

	

}
