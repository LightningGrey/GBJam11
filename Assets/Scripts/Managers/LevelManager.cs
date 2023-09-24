using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	
	public GameObject player;
	//public List<CinemachineVirtualCameraBase> vcams = new List<CinemachineVirtualCameraBase>();
	
	public List<Interactable> batteries = new List<Interactable>();
	public List<Interactable> parts = new List<Interactable>();
	
	
	// Start is called before the first frame update
	void Awake()
	{
		if (SceneManager.GetActiveScene().name == "Level2")
			GBManager.Instance.gb.Display.UpdateColorPalette(4);
		
		
		// player = GameObject.FindGameObjectWithTag("Player");	
		
		// // foreach(CinemachineVirtualCameraBase cam in vcams)
		// // {
		// // 	cam.Follow = player.transform;
		// // }
		// vcams[0].Follow = player.transform;
		// //vcams[0].MoveToTopOfPrioritySubqueue();
	
		
	}
	
	

	// Update is called once per frame
	void Update()
	{
		
	}
}
