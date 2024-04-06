using System;
using System.Collections;
using System.Collections.Generic;
using GBTemplate;
using TMPro;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
	private GBConsoleController gb;
	
	public List<GameObject> optionStrings = new();
	public List<TextMeshProUGUI> optionValues = new();
	
	[Header("Arrow References")]
	public GameObject leftColor;
	public GameObject rightColor;
	public GameObject leftVolume;
	public GameObject rightVolume;
	
	
	[Header("Other Variables")]
	public int selectionIndex = 0;
	public float flashTimer = 0.3f;
	

	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
		
		
		optionValues[0].text = GBManager.Instance.currentPalette.ToString();
		optionValues[1].text = GBManager.Instance.colorize ? "On" : "Off";
		optionValues[2].text = gb.Sound.CurrentMusicVolume.ToString();
		
	}

	// Update is called once per frame
	void Update()
	{
		if (gb.Input.DownJustPressed)
		{
			optionStrings[selectionIndex].SetActive(true);
			flashTimer = 0.3f;
			selectionIndex = selectionIndex >= 3 ? 0 : ++selectionIndex;
			//arrow.transform.localPosition -= new Vector3(0f, 22f);
		}
		else if (gb.Input.UpJustPressed)
		{
			optionStrings[selectionIndex].SetActive(true);
			flashTimer = 0.3f;
			selectionIndex = selectionIndex <= 0 ? 3 : --selectionIndex;
			//arrow.transform.localPosition += new Vector3(0f, 22f);
		}
		else if (gb.Input.LeftJustPressed)
		{
			switch (selectionIndex)
			{
				case 0:
					PaletteSelector(GBManager.Instance.currentPalette <= 1 ? gb.Display.Palettes.Length : --GBManager.Instance.currentPalette);
					break;
				case 1:
					ColorizeToggle();
					break;
				case 2:
					if (gb.Sound.CurrentMusicVolume > 0.0f)
					{
						if (gb.Sound.CurrentMusicVolume == 10.0f)
						{
							rightVolume.SetActive(true);
						}
						VolumeToggle(--gb.Sound.CurrentMusicVolume);

						if (gb.Sound.CurrentMusicVolume == 0.0f)
						{
							leftVolume.SetActive(false);
						}
					}
					break;
				default:
					break;
			}
		}
		else if (gb.Input.RightJustPressed)
		{
			switch (selectionIndex)
			{
				case 0:
					PaletteSelector(GBManager.Instance.currentPalette >= gb.Display.Palettes.Length ? 1 : ++GBManager.Instance.currentPalette);
					break;
				case 1:
					ColorizeToggle();
					break;
				case 2:
					if (gb.Sound.CurrentMusicVolume < 10.0f)
					{
						if (gb.Sound.CurrentMusicVolume == 0.0f)
						{
							leftVolume.SetActive(true);
						}
						VolumeToggle(++gb.Sound.CurrentMusicVolume);
						
						if (gb.Sound.CurrentMusicVolume == 10.0f)
						{
							rightVolume.SetActive(false);
						}
					}
					break;
				default:
					break;
			}
		}


		if (gb.Input.ButtonStartJustPressed)
		{
			if (selectionIndex == 3)
			{
				StartCoroutine(Exit());
			}
		}
		
		
		flashTimer -= Time.deltaTime;
		if (flashTimer <= 0.0f)
		{
			optionStrings[selectionIndex].SetActive(!optionStrings[selectionIndex].activeSelf);	
			flashTimer = 0.3f;
		}

	}

	public IEnumerator Exit()
	{
		//DOTween.KillAll();
		GBManager.Instance.activeControl = false;
		
		optionStrings[selectionIndex].SetActive(true);
		selectionIndex = 0;
		
		yield return GBManager.Instance.gb.Display.StartCoroutine(GBManager.Instance.gb.Display.FadeToBlack(2f));
		
		gameObject.SetActive(false);
		
	}
	
	public void PaletteSelector(int newPalette)
	{
		GBManager.Instance.currentPalette = newPalette;
		
		//automatically change if Colorize is off
		if (!GBManager.Instance.colorize)
		{
			gb.Display.UpdateColorPalette(newPalette - 1);
		}
		
		optionValues[selectionIndex].text = newPalette.ToString();
	}
	
	public void ColorizeToggle()
	{
		GBManager.Instance.colorize = !GBManager.Instance.colorize;
		optionValues[selectionIndex].text = GBManager.Instance.colorize ? "On" : "Off";
		
		leftColor.SetActive(GBManager.Instance.colorize);
		rightColor.SetActive(!GBManager.Instance.colorize);
		
		if (GBManager.Instance.colorize)
		{
			gb.Display.UpdateColorPalette(0);
		}
		else
		{ 
			gb.Display.UpdateColorPalette(GBManager.Instance.currentPalette-1);
		}
	}

	private void VolumeToggle(float newVolume)
	{
		gb.Sound.UpdateMusicVolume(newVolume);
		gb.Sound.UpdateSoundVolume(newVolume);
		optionValues[selectionIndex].text = newVolume.ToString();
		
	}

}
