using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	
	public static UIManager Instance { get; set; }
	
	public TextMeshProUGUI textboxText;
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
			if (options.activeSelf)
			{
				if (GBManager.Instance.gb.Input.DownJustPressed && selectionIndex == 0)
				{
					selectionIndex++;
					arrow.transform.localPosition -= new Vector3(0f, 27f);
				}
				else if (GBManager.Instance.gb.Input.UpJustPressed && selectionIndex == 1)
				{
					selectionIndex--;
					arrow.transform.localPosition += new Vector3(0f, 27f);
				}
				
				if (GBManager.Instance.gb.Input.ButtonStartJustPressed)
				{
					GBManager.Instance.gb.Sound.PlaySound(selectSFX);
					if (selectionIndex == 1)
					{
						ExitLevel();
					}
					else 
					{
						CloseMenu();
					}
				}	
			}		
			
		}
	}
	
	
	
	
	public void UICollectText(bool battery)
	{
		if (textCoroutine != null)
		{
			StopCoroutine(textCoroutine);
		}
		
		textboxText.text = battery ? batteryGet : partGet;
		
		textCoroutine = StartCoroutine(UICollectTextRoutine());
		
	}
	
	public IEnumerator UICollectTextRoutine()
	{
		//itemGetText.text = battery ? batteryGet : partGet;
		
		yield return new WaitForSecondsRealtime(2f);
		
		textboxText.text = "";
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
			batteryText.text = GameplayManager.Instance.batteriesCollected.Count + "/" + GBManager.Instance.batterieslv1;
			partsText.text = GameplayManager.Instance.partsCollected.Count + "/" + GBManager.Instance.partslv1;
		}
		else
		{
			batteryText.text = GameplayManager.Instance.batteriesCollected.Count + "/" + GBManager.Instance.batterieslv2;
			partsText.text = GameplayManager.Instance.partsCollected.Count + "/" + GBManager.Instance.partslv2;
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
		movementTween = transform.DOLocalMoveY(128f, 2).SetUpdate(true).SetEase(Ease.Linear).OnComplete(() => CloseMenuComplete());
	}
	
	public void CloseMenuComplete()
	{
		Time.timeScale = 1;
		paused = false;
		options.SetActive(false);
	}
	
	public void Read(List<string> interactString)
	{
		if (textCoroutine != null)
		{
			StopCoroutine(textCoroutine);
		}
		StartCoroutine(ReadCoroutine(interactString));
	}
	
	public IEnumerator ReadCoroutine(List<string> interactString)
	{
		paused = true;
		
		for (int i = 0; i < interactString.Count; i++)
		{
			textboxText.maxVisibleCharacters = 0;
			textboxText.text = interactString[i];

			foreach (char letter in textboxText.text)
			{
				textboxText.maxVisibleCharacters++;
				yield return new WaitForSeconds(0.03f);
			}

			yield return new WaitUntil(() => GBManager.Instance.gb.Input.ButtonAJustPressed);
		}
		
		paused = false;
		textboxText.text = "";
		textboxText.maxVisibleCharacters = 50; //random high number for not cutting off visible letters
	}


	public void ExitLevel()
	{
		
	}
	
}
