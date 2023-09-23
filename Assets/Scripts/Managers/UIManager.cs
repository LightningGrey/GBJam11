using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	
	public static UIManager Instance { get; set; }
	
	public TextMeshProUGUI itemGetText;
	
	private string batteryGet = "Battery obtained!";
	private string partGet = "Shuttle part obtained!";
	
	
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
		
	}
	
	
	
	public IEnumerator UICollectText(bool battery)
	{
		itemGetText.text = battery ? batteryGet : partGet;
		
		yield return new WaitForSeconds(1f);
		
		itemGetText.text = "";
	}
	
}
