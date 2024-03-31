using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class CreditsCutscene : MonoBehaviour
{
		
	public List<GameObject> backgrounds;
	
	[Header("Text References")]
	public TextMeshProUGUI textboxText;
	public List<string> textLines;
	public int counter = 0;
	

	// Start is called before the first frame update
	void Start()
	{
		
		StartCoroutine(TextDisplayCoroutine());
	}

	// Update is called once per frame
	void Update()
	{
		foreach (GameObject background in backgrounds)
		{
			background.transform.position -= new Vector3(0.65f * Time.deltaTime, 0.0f);	
			if (background.transform.position.x <= -1.6f)
			{
				background.transform.position = new Vector3(1.6f, 0.0f);	
			}
			
		}
	}
	
	//TODO: extra message congratulating you for 100%
	IEnumerator TextDisplayCoroutine()
	{
		
		while (counter < textLines.Count)
		{
			textboxText.gameObject.SetActive(false);
			yield return new WaitForSeconds(1.0f);
		
			textboxText.text = textLines[counter];
			textboxText.gameObject.SetActive(true);
			
			// better case handling is possible but we're just at the end I'm finishing this
			if (counter == 1)
			{
				textboxText.text = textboxText.text.Replace("{batteryVal}", 
				(GBManager.Instance.batteriesCollectedlv1.Count + GBManager.Instance.batteriesCollectedlv2.Count).ToString());
				textboxText.text = textboxText.text.Replace("{partVal}", 
				(GBManager.Instance.partsCollectedlv1.Count + GBManager.Instance.partsCollectedlv2.Count).ToString());
			}
			else if (counter == 2)
			{
				textboxText.alignment = TextAlignmentOptions.Center;
			}
				
			yield return new WaitForSeconds(4.0f);
			counter++;
			
		}

	}

	
}
