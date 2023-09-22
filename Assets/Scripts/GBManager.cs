using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GBTemplate;
using System;
using Unity.VisualScripting;

public class GBManager : MonoBehaviour
{
	
	public static GBManager Instance { get; set; }
	public GBConsoleController gb;
	
	public string loadScene = "";
	
	
	[Header("Global Flags")]
	public bool activeControl = true;
	
	
	void Awake()
	{
		 if(Instance != null)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
		
		DontDestroyOnLoad(this.gameObject);
	}
	
	
	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		//gb.Display.UpdateColorPalette(1);
		
		gb.Sound.UpdateGlobalVolume(10.5f);
		
		//if(loadScene != "")
		//{
			SceneManager.LoadScene(loadScene);
		//}
		
	}

	public void ReloadScene()
	{
		StartCoroutine(ReloadSceneCoroutine());
	}
	
	public IEnumerator ReloadSceneCoroutine()
	{
		yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(1f));

		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(2f));
		
		activeControl = true;
	}

}
