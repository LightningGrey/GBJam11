using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GBTemplate;
using System;

public class GameplaySceneManager : MonoBehaviour
{
	
	public static GameplaySceneManager Instance { get; set; }
	private GBConsoleController gb;
	
	
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
