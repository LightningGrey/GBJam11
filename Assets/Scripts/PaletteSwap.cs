using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;

public class PaletteSwap : MonoBehaviour
{
	
	private GBConsoleController gb;
	
	
	
	// Start is called before the first frame update
	void Start()
	{
		 //Getting the instance of the console controller, so we can access its functions
		gb = GBConsoleController.GetInstance();
		gb.Display.UpdateColorPalette(1);
	}


	// Update is called once per frame
	void Update()
	{
		
	}
}
