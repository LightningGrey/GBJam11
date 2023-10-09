using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	
	public static UIManager Instance { get; set; }
	
	public TextMeshProUGUI itemGetText;
	public GameObject options;
	public GameObject arrow;
	public AudioClip selectSFX;
	
	public TextMeshProUGUI batteryText;
	
	public TextMeshProUGUI partsText;
	
	public bool paused = false;
	public int selectionIndex = 0;
	
	// text
	private string batteryGet = "Battery obtained!";
	private string partGet = "Shuttle part obtained!";
	
	private Tweener movementTween;
	private Coroutine textCoroutine;
	
	
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
	}

	// Update is called once per frame
	void Update()
	{
		
		if (paused)
		 {
		// 	if (GBManager.Instance.gb.Input.DownJustPressed && selectionIndex == 0)
		// 	{
		// 		selectionIndex++;
		// 		arrow.transform.localPosition -= new Vector3(0f, 27f);
		// 	}
		// 	else if (GBManager.Instance.gb.Input.UpJustPressed && selectionIndex == 1)
		// 	{
		// 		selectionIndex--;
		// 		arrow.transform.localPosition += new Vector3(0f, 27f);
		// 	}
			
			if (GBManager.Instance.gb.Input.ButtonStartJustPressed)
			{
				GBManager.Instance.gb.Sound.PlaySound(selectSFX);
				//if (selectionIndex == 1)
				//{
				//	ExitLevel();
				//}
				//else 
				//{
					CloseMenu();
				//}
			}
			
		}
	}
	
	
	
	
	public void UICollectText(bool battery)
	{
		if (textCoroutine != null)
		{
			StopCoroutine(textCoroutine);
		}
		
		itemGetText.text = battery ? batteryGet : partGet;
		
		textCoroutine = StartCoroutine(UICollectTextRoutine());
		
		
	}
	
	public IEnumerator UICollectTextRoutine()
	{
		//itemGetText.text = battery ? batteryGet : partGet;
		
		yield return new WaitForSecondsRealtime(1f);
		
		itemGetText.text = "";
	}
	
	
	public void PauseMenu()
	{
		Time.timeScale = 0;
		//paused = true;
		GBManager.Instance.activeControl = false;
		
		movementTween = transform.DOLocalMoveY(0f, 2).SetUpdate(true).SetEase(Ease.Linear)
			.OnComplete(() => StartCoroutine(UIFlash()));
		
		// options.SetActive(true);
		// StartCoroutine(UIFlash());
	}
	
	
	public IEnumerator UIFlash()
	{ 
		yield return new WaitForSecondsRealtime(0.3f);
		
		options.SetActive(true);
		paused = true;
		
		if (GBManager.Instance.currentLevel == 0)
		{
			batteryText.text = GBManager.Instance.batteriesCollectedlv1.Count + "/" + GBManager.Instance.batterieslv1;
			partsText.text = GBManager.Instance.partsCollectedlv1.Count + "/" + GBManager.Instance.partslv1;
		}
		else
		{
			batteryText.text = GBManager.Instance.batteriesCollectedlv2.Count + "/" + GBManager.Instance.batterieslv2;
			partsText.text = GBManager.Instance.partsCollectedlv2.Count + "/" + GBManager.Instance.partslv2;
		}

		
		GBManager.Instance.activeControl = true;

		// do
		// {
		// 	arrow.gameObject.SetActive(false);
		// 	yield return new WaitForSecondsRealtime(0.3f);
				
		// 	arrow.gameObject.SetActive(true);
		// 	yield return new WaitForSecondsRealtime(0.3f);	
		// } 
		// while (!GBManager.Instance.gb.Input.ButtonStartJustPressed);

	
	}
	
	public void CloseMenu()
	{
		movementTween = transform.DOLocalMoveY(128f, 2).SetUpdate(true).SetEase(Ease.Linear)
			.OnComplete(() => options.SetActive(false)).OnComplete(() => CloseMenuComplete());
	}
	
	public void CloseMenuComplete()
	{
		Time.timeScale = 1;
		paused = false;
	}
	
	public void Read()
	{
		
	}
	
	
	public void ExitLevel()
	{
		
	}
	
}
