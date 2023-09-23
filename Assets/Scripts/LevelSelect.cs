using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GBTemplate;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
	
	private GBConsoleController gb;
	
	public GameObject currentImage;
	public List<Sprite> levelSprites;


	public int levelIndex = 0;

	public string levelName;

	public GameObject stageTitle;
	public GameObject stageInfo;
	
	private Tweener movementTween;

	
	// Start is called before the first frame update
	void Start()
	{
		
		gb = GBConsoleController.GetInstance();
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
			}
			if (gb.Input.RightJustPressed)
			{
				levelIndex++;
				if (levelIndex > 2) {levelIndex = 0;}
				UpdateLevelSelect();
			}
			
			if (gb.Input.ButtonBJustPressed)
			{
				stageTitle.SetActive(!stageTitle.activeSelf);
				stageInfo.SetActive(!stageInfo.activeSelf);
			}
		}
		
	}
	
	void UpdateLevelSelect()
	{
		
		currentImage.GetComponent<Image>().sprite = levelSprites[levelIndex];
		
		// fixes a minor
		DOTween.KillAll();
		currentImage.transform.localPosition = new Vector3(0f, -24f, 0f);
		
		movementTween = currentImage.transform.DOLocalMoveY(-16f, 1f).SetAutoKill(false).
			SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		
	}
	
	
}
