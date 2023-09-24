using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GBTemplate;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private GBConsoleController gb;
	
	public AudioClip selectSFX;
	//public GameObject text;
	public GameObject options;
	public GameObject arrow;
	private int selectionIndex = 0;
	public Coroutine currentCoroutine;
	public string nextScene = "MainGameplayScene";
	
	
	
	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		
		GBManager.Instance.activeControl = false;
		
		//StartCoroutine(UIFlash());

		// mainMenu.transform.DOLocalMoveY(-36f, 1f, true).SetEase(Ease.Linear).OnComplete(
		// 	() => arrow.SetActive(true)).OnComplete(
		// 		//() => GBManager.Instance.activeControl = true).OnComplete(
		// 			() => currentCoroutine = StartCoroutine(UIFlash()));

	}

	// Update is called once per frame
	void Update()
	{
		if (GBManager.Instance.activeControl)
		{
			// if (gb.Input.DownJustPressed && selectionIndex == 0)
			// {
			// 	selectionIndex++;
			// 	arrow.transform.localPosition -= new Vector3(0f, 22f);
			// }
			// else if (gb.Input.UpJustPressed && selectionIndex == 1)
			// {
			// 	selectionIndex--;
			// 	arrow.transform.localPosition += new Vector3(0f, 22f);
			// }
			
			
			//TODO: uncomment later if want options
			// if (gb.Input.ButtonSelectPressedTime > 2f && gb.Input.DownPressedTime > 2f && !options.activeSelf)
			// {
				
			// 	StartCoroutine(Options());
			// }
			

			if (gb.Input.ButtonStartJustPressed)
			{
				gb.Sound.PlaySound(selectSFX);
				if (selectionIndex == 0)
				{
					StartGame();
				}
			}
		}

	}
	
	// public IEnumerator UIFlash()
	// {
	// 	yield return new WaitForSeconds(0.3f);
		
	// 	GBManager.Instance.activeControl = true;

	// 	do
	// 	{
	// 		arrow.gameObject.SetActive(false);
	// 		yield return new WaitForSeconds(0.3f);
				
	// 		arrow.gameObject.SetActive(true);
	// 		yield return new WaitForSeconds(0.3f);	
	// 	} 
	// 	while (!gb.Input.ButtonStartJustPressed);

	
	// }
	
	
	void StartGame()
	{
		//DOTween.KillAll();
		
		GBManager.Instance.activeControl = false;
		GBManager.Instance.LoadNewScene(SceneManager.GetActiveScene(), "LevelSelect");
	}
	
	public IEnumerator Options()
	{
		//DOTween.KillAll();
		GBManager.Instance.activeControl = false;
		
		yield return GBManager.Instance.gb.Display.StartCoroutine(GBManager.Instance.gb.Display.FadeToBlack(2f));
		
		options.SetActive(true);
		
		yield return GBManager.Instance.gb.Display.StartCoroutine(GBManager.Instance.gb.Display.FadeFromBlack(2f));
		
		GBManager.Instance.activeControl = true;
		
	}
	
	
}
