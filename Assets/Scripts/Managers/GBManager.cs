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
	
	[Header("References")]
	public List<string> sceneList = new List<string>();
	
	[Header("Global Flags")]
	public bool colorize = true;
	public bool activeControl = true;
	public int currentLevel = 0;
	
	
	[Header("Collectibles Info")]
	public List<int> batteriesCollectedlv1 = new List<int>();
	public int batterieslv1 = 3;
	public List<int> partsCollectedlv1 = new List<int>();
	public int partslv1 = 4;
	
	public List<int> batteriesCollectedlv2 = new List<int>();
	public int batterieslv2 = 2;
	public List<int> partsCollectedlv2 = new List<int>();
	public int partslv2 = 4;
	
	
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
		gb.Display.UpdateColorPalette(1);
		
		gb.Sound.UpdateGlobalVolume(10.5f);
		
		if (loadScene != "")
		{
			SceneManager.LoadScene(loadScene);
		}
	
	}
	
	// public void SetActiveControl(bool setter = true)
	// {
	// 	activeControl = setter;
	// }


	public void LoadNewScene(Scene currentScene, string nextScene)
	{
		StartCoroutine(LoadSceneCoroutine(currentScene, nextScene));
	}
	
	public IEnumerator LoadSceneCoroutine(Scene currentScene, string nextScene)
	{
		yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(2f));
		yield return new WaitForSeconds(1f);

		SceneManager.LoadScene(nextScene);

		yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(2f));
		yield return new WaitForSeconds(0.5f);
		
		activeControl = true;
	}
	

	public void ReloadScene()
	{
		StartCoroutine(ReloadSceneCoroutine());
	}
	
	public IEnumerator ReloadSceneCoroutine()
	{
		yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(2f));
		yield return new WaitForSeconds(1f);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
		yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(2f));
		yield return new WaitForSeconds(0.5f);
		
		activeControl = true;
	}
	
	
	//hardcoding in the crunch
	public void ObtainParts()
	{
		if (currentLevel == 0)
		{
			batteriesCollectedlv1.AddRange(GameplayManager.Instance.batteriesCollected);	
			partsCollectedlv1.AddRange(GameplayManager.Instance.partsCollected);
		}
		else if (currentLevel == 1)
		{
			batteriesCollectedlv2.AddRange(GameplayManager.Instance.batteriesCollected);	
			partsCollectedlv2.AddRange(GameplayManager.Instance.partsCollected);
		}
		
		
	}
	
	

}
