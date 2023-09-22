using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
	
	public static GameplayManager Instance { get; set; }
	private GBConsoleController gb;
	
	[Header("References")]
	public List<AudioClip> levelMusic = new List<AudioClip>();
	private int levelParam = 0;
	public static event UnityAction deathTrigger;

	
	[Header("Energy Meter")]
	public int energyMeter = 100;
	public float timer = 1f;
	public TextMeshProUGUI energyMeterText;
	private float speedScale = 1f;
	
	
	//[Header("Global Flags")]
	//public bool activeGame = true;
	//public bool dead = false;
	

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
		gb.Sound.PlayMusic(levelMusic[0]);
		
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
				timer -= Time.deltaTime * speedScale;

				if (timer <= 0f)
				{
					energyMeter--;
					energyMeterText.text = energyMeter.ToString();

					timer = 1f;
				}
			}
		}

	}

	public void ChangeSpeed(float speed)
	{
		speedScale = speed;
	}
	
}
