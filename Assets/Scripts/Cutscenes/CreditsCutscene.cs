using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

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
				var batteryTotal = GBManager.Instance.batteriesCollectedlv1.Count + GBManager.Instance.batteriesCollectedlv2.Count;
				var partTotal = GBManager.Instance.partsCollectedlv1.Count + GBManager.Instance.partsCollectedlv2.Count;
				
				textboxText.text = textboxText.text.Replace("{batteryVal}", batteryTotal.ToString());
				textboxText.text = textboxText.text.Replace("{partVal}", partTotal.ToString());
				
				if (partTotal == 10 && batteryTotal == 6)
				{
					textboxText.text += "\nYou collected everything, well done!";
				}
				else if (partTotal == 10)
				{
					textboxText.text += "\nYou collected all parts!";
				}
				else if (batteryTotal == 6)
				{
					textboxText.text += "\nYou collected all batteries!";
				}
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
