using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;
using Cinemachine;

public class ScreenTransition : MonoBehaviour
{
	private GBConsoleController gb;
	private GameObject player;
	public CinemachineVirtualCameraBase vcam;
	public Transform spawnPosition;
	
	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			GBManager.Instance.activeControl = false;
			StartCoroutine(LoadNewArea());
		}
	}
	
	public IEnumerator LoadNewArea()
	{
		yield return gb.Display.StartCoroutine(gb.Display.FadeToWhite(1f));

		vcam.MoveToTopOfPrioritySubqueue();
		player.transform.position = spawnPosition.position;

		yield return gb.Display.StartCoroutine(gb.Display.FadeFromWhite(2f));
		
		GBManager.Instance.activeControl = true;
	}
	
}
