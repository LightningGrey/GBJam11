using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GBTemplate;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
	
	private GBConsoleController gb;
	
	public GameObject currentImage;
	public List<Sprite> levelSprites;
	public AudioClip menuMusic;
	public AudioClip selectSFX;



	public int levelIndex = 0;

	public List<string> levelNames;


	// text
	public TextMeshProUGUI stageTitle;
	public GameObject stageInfo;
	public TextMeshProUGUI batteryText;
	public TextMeshProUGUI partsText;
	
	
	
	private Tweener movementTween;

	
	// Start is called before the first frame update
	void Start()
	{
		
		gb = GBConsoleController.GetInstance();
		gb.Sound.StopMusic();
		gb.Sound.PlayMusic(menuMusic);
		
		levelIndex = GBManager.Instance.currentLevel;	
		
		UpdateLevelSelect();
	}

	// Update is called once per frame
	void Update()
	{
		if (GBManager.Instance.activeControl)
		{
			if (gb.Input.LeftJustPressed)
			{
				levelIndex--;
				if (levelIndex < 0) {levelIndex = levelSprites.Count-1;}
				UpdateLevelSelect();
				gb.Sound.PlaySound(selectSFX);
			}
			if (gb.Input.RightJustPressed)
			{
				levelIndex++;
				if (levelIndex > 2) {levelIndex = 0;}
				UpdateLevelSelect();
				gb.Sound.PlaySound(selectSFX);
			}
			
			
			if (gb.Input.ButtonAJustPressed)
			{
				DOTween.KillAll();
				GBManager.Instance.activeControl = false;
				GBManager.Instance.currentLevel = levelIndex;	
				GBManager.Instance.LoadNewScene(SceneManager.GetActiveScene(), 
					GBManager.Instance.sceneList[levelIndex]);
				gb.Sound.PlaySound(selectSFX);
			}
			
			
			if (gb.Input.ButtonBJustPressed)
			{
				stageTitle.enabled = !stageTitle.enabled;
				if (stageTitle.enabled) 
				{
					stageTitle.text = levelNames[levelIndex];
				}
				
				stageInfo.SetActive(!stageInfo.activeSelf);
				if (stageInfo.activeSelf)
				{
					UpdatePartsText();
				}
			}
		}
		
	}
	
	void UpdateLevelSelect()
	{

		currentImage.GetComponent<Image>().sprite = levelSprites[levelIndex];
		if (stageTitle.enabled)
		{
			stageTitle.text = levelNames[levelIndex];
		}
		if (stageInfo.activeSelf)
		{
			UpdatePartsText();
		}


		// fixes a minor
		DOTween.KillAll();
		currentImage.transform.localPosition = new Vector3(0f, -24f, 0f);
		
		movementTween = currentImage.transform.DOLocalMoveY(-16f, 1f).SetAutoKill(false).
			SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		
	}
	
	
	public void UpdatePartsText()
	{
		switch (levelIndex)
		{
			case 0:
				batteryText.text = GBManager.Instance.batteriesCollectedlv1.Count + "/" + GBManager.Instance.batterieslv1;
				partsText.text = GBManager.Instance.partsCollectedlv1.Count + "/" + GBManager.Instance.partslv1;
				break;

			case 1:
				batteryText.text = GBManager.Instance.batteriesCollectedlv2.Count + "/" + GBManager.Instance.batterieslv2;
				partsText.text = GBManager.Instance.partsCollectedlv2.Count + "/" + GBManager.Instance.partslv2;
				break;

			default:
				batteryText.text = "--/--";
				partsText.text = "--/--";
				break;
		}
	}
	
	
}
