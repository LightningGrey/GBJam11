using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;
using DG.Tweening;
using TMPro;

public class TitleScreen : MonoBehaviour
{
	
	private GBConsoleController gb;
	public AudioClip titleMusic;
	public Coroutine currentCoroutine;

	
	public Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();
	
	
	[Header("References")]
	public GameObject jamLogo;
	public GameObject credits;
	public GameObject coverImage;
	public GameObject titleScreen;
	public TextMeshProUGUI titleScreenText;
	public GameObject mainMenu;
	
	
	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		gb.Sound.PlayMusic(titleMusic);
		
		coroutineQueue.Enqueue(PlayJamIntro());
		coroutineQueue.Enqueue(PlayCreditsIntro());
	 	coroutineQueue.Enqueue(PlayTitleScreen());
		
		
		//coroutineCount++;
		currentCoroutine = StartCoroutine(coroutineQueue.Dequeue());
	}
	
	void Update()
	{
		if ((gb.Input.ButtonAJustPressed || gb.Input.ButtonStartJustPressed) && currentCoroutine != null 
			&& coroutineQueue.Count > 0)
		{
			StopAllCoroutines();
			gb.Display.InterruptFade();
			
			coverImage.SetActive(false);
			jamLogo.SetActive(false);
			credits.SetActive(false);
			mainMenu.SetActive(true);
			
			currentCoroutine = StartCoroutine(TextFlash());
			
			//currentCoroutine = StartCoroutine(coroutineQueue.Dequeue());
			
		}

	}

	public IEnumerator PlayJamIntro()
	{
		yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(10f));
		
		coverImage.SetActive(false);
		
		yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(1f));
		
		yield return new WaitForSeconds(1f);
		
		currentCoroutine = StartCoroutine(coroutineQueue.Dequeue());
		
	}
	
	public IEnumerator PlayCreditsIntro()
	{
		yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(1f));
		
		coverImage.SetActive(false);
		jamLogo.SetActive(false);
		yield return new WaitForSeconds(1f);
		
		yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(1f));
		
		yield return new WaitForSeconds(1f);
		
		currentCoroutine = StartCoroutine(coroutineQueue.Dequeue());
		
	}
	
	public IEnumerator PlayTitleScreen()
	{
		yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(1f));
		
		coverImage.SetActive(false);
		jamLogo.SetActive(false);
		credits.SetActive(false);
		
		yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(1f));
		
		yield return new WaitForSeconds(1f);
		
		mainMenu.SetActive(true);

		//titleScreenText.DOColor(Color.black, 0.3f).SetAutoKill(false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Flash);
		currentCoroutine = StartCoroutine(TextFlash());
	}

	public IEnumerator TextFlash()
	{	
		yield return new WaitForSeconds(0.3f);
		
		GBManager.Instance.activeControl = true;

		do
		{
			titleScreenText.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.3f);

			titleScreenText.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.3f);	
		} 
		while (!gb.Input.ButtonStartJustPressed);


	}



}
