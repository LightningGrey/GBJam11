using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


// TODO: maybe change this into multiple scripts cause this is bad?
public class GameplayManager : MonoBehaviour
{
	
	public static GameplayManager Instance { get; set; }
	private GBConsoleController gb;
	
	[Header("References")]
	//public List<string> sceneList = new List<string>();
	
	public AudioClip levelMusic;
	public AudioClip clearAudio;
	
	
	public static event UnityAction deathTrigger;
	public static event UnityAction clearTrigger;

	
	[Header("Energy Meter")]
	public int energyMeter = 60;
	public float energyTimer = 1f;
	public TextMeshProUGUI energyMeterText;
	private float speedScale = 1f;
	
	
	[Header("Item Collection")]
	public List<int> batteriesCollected = new List<int>();
	public List<int> partsCollected = new List<int>();
	

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
	
	
	// Start is called before the first frame update
	void Start()
	{
		 //Getting the instance of the console controller, so we can access its functions
		gb = GBConsoleController.GetInstance();
		
		//SceneManager.LoadScene(sceneList[GBManager.Instance.currentLevel], LoadSceneMode.Additive);
		
		gb.Sound.PlayMusic(levelMusic);
		
		energyMeterText.text = energyMeter.ToString();
	}


	// Update is called once per frame
	void Update()
	{
		if (GBManager.Instance.activeControl)
		{
			if (energyMeter <= 0f)
			{
				Time.timeScale = 0f;
				GBManager.Instance.activeControl = false;				
				
				deathTrigger?.Invoke();
				gb.Sound.StopMusic();
				
				Time.timeScale = 1;
			}
			else
			{
				energyTimer -= Time.deltaTime * speedScale;

				if (energyTimer <= 0f)
				{
					// energyMeter--;
					// if (energyMeter < 0) { energyMeter = 0;}
					// energyMeterText.text = energyMeter.ToString();

					DepleteEnergy();
	
					energyTimer = 1f;
				}
			}
			
		}

	}
	
	public void ClearLevel()
	{
		GBManager.Instance.activeControl = false;

		GBManager.Instance.ObtainParts();
		StartCoroutine(ClearLevelTransition());
		
		// gb.Sound.StopMusic();
		// gb.Sound.StopAllSounds();
		
		// gb.Sound.PlaySound(clearAudio);
	}
	
	public IEnumerator ClearLevelTransition()
	{
			
		gb.Sound.StopMusic();
		gb.Sound.StopAllSounds();
		
		gb.Sound.PlayMusicOneShot(clearAudio);
		
		yield return new WaitForSeconds(2f);
		
		clearTrigger?.Invoke();

		yield return new WaitWhile (()=>gb.Sound.IsMusicPlaying());
		
		if (GBManager.Instance.colorize)
		{
			GBManager.Instance.gb.Display.UpdateColorPalette(1);
		}
		
		GBManager.Instance.LoadNewScene(SceneManager.GetActiveScene(), "LevelSelect");
	}

	public void ChangeSpeed(float speed)
	{
		speedScale = speed;
	}
	
	public void DepleteEnergy(int amount = 1)
	{
		energyMeter = Mathf.Max(energyMeter - amount, 0);
		energyMeterText.text = energyMeter.ToString();
		
		//timer = 1f;
	}
	
	public void Collect(bool battery, int ID)
	{
		if (battery)
		{
			batteriesCollected.Add(ID);
			energyMeter += 20;
		}
		else 
		{
			partsCollected.Add(ID);
		}
		
		
	}
	
	
}
