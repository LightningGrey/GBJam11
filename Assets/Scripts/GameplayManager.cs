using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;
using TMPro;

public class GameplayManager : MonoBehaviour
{
	
	private GBConsoleController gb;
	public List<AudioClip> levelMusic = new List<AudioClip>();
	private int levelParam = 0;
	
	
	[Header("EnergyMeter")]
	public int energyMeter = 1000;
	public float timer = 1f;
	public TextMeshProUGUI energyMeterText;
	
	
	// Start is called before the first frame update
	void Start()
	{
		 //Getting the instance of the console controller, so we can access its functions
		gb = GBConsoleController.GetInstance();
		gb.Sound.PlayMusic(levelMusic[0]);
		gb.Sound.UpdateMusicVolume(10.5f);
		//gb.Display.UpdateColorPalette(1);
	}


	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;
		
		if (timer <= 0f)
		{
			energyMeter--;
			energyMeterText.text = energyMeter.ToString();
			
			timer = 1f;
		}

	}
	

}
