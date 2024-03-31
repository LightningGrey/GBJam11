using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalLevelCutscene : MonoBehaviour
{
		
	public GameObject ship;
	public float endLocationIncomplete;
	public float endLocationClear;
	public float duration;
	
	[Header("Text References")]
	public TextMeshProUGUI textboxText;
	public List<string> textLines = new();
	
	private Tweener movementTweener;
	
	// Start is called before the first frame update
	void Start()
	{
		// if (GBManager.Instance.colorize)
		// {
		// 	GBManager.Instance.gb.Display.UpdateColorPalette(0);
		// }
		if (GBManager.Instance.partsCollectedlv1.Count + GBManager.Instance.partsCollectedlv2.Count < 7)
		{
			movementTweener = ship.transform.DOLocalMoveX(endLocationIncomplete, duration).SetEase(Ease.OutSine).OnComplete(
				() => NotEnough());
		}
		else
		{
			movementTweener = ship.transform.DOLocalMoveX(endLocationClear, duration + 2f).SetEase(Ease.Linear).OnComplete(
				() => ProgressToCredits());
		}

	}

	// Update is called once per frame
	void Update()
	{
		
	}
	
	void NotEnough()
	{
		StartCoroutine(ReadCoroutine(textLines));
	}
	
	public IEnumerator ReadCoroutine(List<string> textLines)
	{
		
		for (int i = 0; i < textLines.Count; i++)
		{
			textboxText.maxVisibleCharacters = 0;
			textboxText.text = textLines[i];

			foreach (char letter in textboxText.text)
			{
				textboxText.maxVisibleCharacters++;
				yield return new WaitForSeconds(0.03f);
			}

			yield return new WaitUntil(() => GBManager.Instance.gb.Input.ButtonAJustPressed);
		}

		textboxText.text = "";
		textboxText.maxVisibleCharacters = 50; //random high number for not cutting off visible letters
		
		GBManager.Instance.LoadNewScene(SceneManager.GetActiveScene(), "LevelSelect");
	}
	
	void ProgressToCredits()
	{
		GBManager.Instance.LoadNewScene(SceneManager.GetActiveScene(), "CreditsScene");
	}
	
}
