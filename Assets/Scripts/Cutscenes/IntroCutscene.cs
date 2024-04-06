using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using GBTemplate;

public class IntroCutscene : MonoBehaviour
{
	
	
	[SerializeField]
	private float endLocation;
	
	[SerializeField]
	private float duration;
	
	[SerializeField]
	private AudioClip cutsceneTheme;
	
	
	
	
	private GBConsoleController gb;
	
	private Tweener textScrollTween;
	
	
	
	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		gb.Sound.StopMusic();
		gb.Sound.PlayMusicOneShot(cutsceneTheme);
		
		textScrollTween = transform.DOLocalMoveY(endLocation, duration).SetAutoKill(false).SetEase(Ease.Linear).OnComplete(
			() => GBManager.Instance.LoadNewScene(SceneManager.GetActiveScene(), "LevelSelect")
		);
	}

	// Update is called once per frame
	void Update()
	{
		if (GBManager.Instance.activeControl)
		{
			if (gb.Input.ButtonAJustPressed)
			{
				DOTween.timeScale = 4.0f;
			}
			if (gb.Input.ButtonAJustReleased)
			{
				DOTween.timeScale = 1.0f;
			}
			
			if (gb.Input.ButtonStartJustPressed)
			{
				GBManager.Instance.LoadNewScene(SceneManager.GetActiveScene(), "LevelSelect");
			}
		}
	}
	
	
	private void OnDisable()
	{
		DOTween.KillAll();
	}
}
