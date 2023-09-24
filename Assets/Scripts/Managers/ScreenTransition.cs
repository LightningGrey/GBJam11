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
	public CinemachineVirtualCameraBase vcam;
	public Transform spawnPosition;
	
	
	// load area from manager
	public int currentAreaID;
	public int nextAreaID;
	public static event UnityAction<int> enterTrigger;
	public static event UnityAction<int> unloadTrigger;

	
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
			enterTrigger?.Invoke(nextAreaID);
			StartCoroutine(LoadNewArea());
		}
	}
	
	public IEnumerator LoadNewArea()
	{
		yield return gb.Display.StartCoroutine(gb.Display.FadeToWhite(1f));

		vcam.MoveToTopOfPrioritySubqueue();
		player.transform.position = spawnPosition.position;
	
		yield return gb.Display.StartCoroutine(gb.Display.FadeFromWhite(2f));
		
		unloadTrigger?.Invoke(currentAreaID);
		
		GBManager.Instance.activeControl = true;
	}
	
}
