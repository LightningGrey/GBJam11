using System.Collections;
using System.Collections.Generic;
using GBTemplate;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
	private GBConsoleController gb;
	
	public int selectionIndex = 0;

	// Start is called before the first frame update
	void Start()
	{
		gb = GBConsoleController.GetInstance();
	}

	// Update is called once per frame
	void Update()
	{
		if (gb.Input.DownJustPressed)
		{
			selectionIndex = selectionIndex >= 3 ? 0 : ++selectionIndex;
			//arrow.transform.localPosition -= new Vector3(0f, 22f);
		}
		else if (gb.Input.UpJustPressed)
		{
			selectionIndex = selectionIndex <= 0 ? 3 : --selectionIndex;
			//arrow.transform.localPosition += new Vector3(0f, 22f);
		}
		
		if (gb.Input.ButtonStartJustPressed)
		{
			switch(selectionIndex)
			{
				case 3:
					StartCoroutine(Exit());
					break;
				default:
					//Exit();
					break;
			}
		}

	}
	
	public IEnumerator Exit()
	{
		//DOTween.KillAll();
		GBManager.Instance.activeControl = false;
		
		yield return GBManager.Instance.gb.Display.StartCoroutine(GBManager.Instance.gb.Display.FadeToBlack(2f));
		
		gameObject.SetActive(false);
		
	}
	
	
}
