using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;
using Cinemachine;
using UnityEngine.Events;

public class ScreenTransition : MonoBehaviour
{

	private GBConsoleController gb;
	private GameObject player;
	//public CinemachineVirtualCameraBase vcam;
	public Transform spawnPosition;
	
	
	// load area from manager
	public int currentAreaID;
	public int nextAreaID;
	public static event UnityAction<int, int> EnterTrigger;
	public static event UnityAction<int> UnloadTrigger;

	
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
	
	// change camera to number priority or make new camera manager
	public IEnumerator LoadNewArea()
	{
		yield return gb.Display.StartCoroutine(gb.Display.FadeToWhite(1f));

		//vcam.MoveToTopOfPrioritySubqueue();
		EnterTrigger?.Invoke(nextAreaID, currentAreaID);
		player.transform.position = spawnPosition.position;
	
		yield return gb.Display.StartCoroutine(gb.Display.FadeFromWhite(2f));
		
		UnloadTrigger?.Invoke(currentAreaID);
		
		GBManager.Instance.activeControl = true;
		
		//GBManager.Instance.activeControl = true;
	}
	
}
