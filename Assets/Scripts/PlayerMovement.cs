using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;

public class PlayerMovement : MonoBehaviour
{
	private GBConsoleController gb;
	private Camera cam;
	public float speed = 0.5f;
	
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
			transform.position += new Vector3(0f, 1f) * speed * Time.deltaTime;
		}
		if (gb.Input.Down)
		{
			transform.position -= new Vector3(0f, 1f) * speed * Time.deltaTime;
		}
		if (gb.Input.Left)
		{
			transform.position -= new Vector3(1f, 0f) * speed * Time.deltaTime;
		}
		if (gb.Input.Right)
		{
			transform.position += new Vector3(1f, 0f) * speed * Time.deltaTime;
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log(other.gameObject);
	}

}
