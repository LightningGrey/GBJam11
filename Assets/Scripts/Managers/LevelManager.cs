using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	
	public GameObject player;
	public List<Interactable> items = new List<Interactable>();
	
	public List<GameObject> areaManager = new List<GameObject>();
	
	
	void OnEnable()
	{
		ScreenTransition.enterTrigger += LoadArea;
	}
	
	void OnDisable()
	{
		ScreenTransition.enterTrigger -= LoadArea;
	}
	
	
	// Start is called before the first frame update
	void Start()
	{
		
		//we're hardcoding cause rip
		if (GBManager.Instance.currentLevel == 0)
		{
			// if (GBManager.Instance.colorize)
			// {
			// 	GBManager.Instance.gb.Display.UpdateColorPalette(1);
			// }
			
			if (items.Count > 0)
			{
				for (int i = items.Count-1; i >= 0; i--)
				{

					if (GBManager.Instance.batteriesCollectedlv1.Count > 0)
					{
						if (items[i].type == InteractableType.BATTERY &&
						GBManager.Instance.batteriesCollectedlv1.Contains(items[i].objectID))
						{
							items[i].gameObject.SetActive(false);
							items.RemoveAt(i);
						}

					}
					if (GBManager.Instance.partsCollectedlv1.Count > 0)
					{
						if (items[i].type == InteractableType.PART &&
						GBManager.Instance.partsCollectedlv1.Contains(items[i].objectID))
						{
							items[i].gameObject.SetActive(false);
							items.RemoveAt(i);
						}

					}
				}

			}


		}


		if (GBManager.Instance.currentLevel == 1)
		{
			if (GBManager.Instance.colorize)
			{
				GBManager.Instance.gb.Display.UpdateColorPalette(4);
			}
			
			if (items.Count > 0)
			{
				for (int i = items.Count-1; i >= 0; i--)
				{

					if (GBManager.Instance.batteriesCollectedlv2.Count > 0)
					{
						if (items[i].type == InteractableType.BATTERY &&
						GBManager.Instance.batteriesCollectedlv2.Contains(items[i].objectID))
						{
							items[i].gameObject.SetActive(false);
							items.RemoveAt(i);
						}

					}
					if (GBManager.Instance.partsCollectedlv2.Count > 0)
					{
						if (items[i].type == InteractableType.PART &&
						GBManager.Instance.partsCollectedlv2.Contains(items[i].objectID))
						{
							items[i].gameObject.SetActive(false);
							items.RemoveAt(i);
						}

					}
				}

			}
			
		}
		
		
	}
	
	
	void LoadArea(int areaID)
	{
		areaManager[areaID].SetActive(true);
		
		for (int i = 0; i < areaManager.Count; i++)
		{
			if (i != areaID)
			{
				areaManager[i].SetActive(false);
			}
		}
		
		
	}
}
