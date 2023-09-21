using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
	
	public SpriteRenderer activeSprite;
	public List<Sprite> gameplaySprites = new List<Sprite>();
	public Animator animationController;
	
	
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	
	//TODO: UPDATE SPRITE
	public void UpdateGameplaySprite(bool up, bool down, bool left, bool right)
	{
		if (left)
		{
			activeSprite.flipX = true;
		}
		else if (right)
		{
			activeSprite.flipX = false;
		}
		
		
		if (up)
		{
			animationController.Play("Up");
		}
		else if (down)
		{
			if (left || right)
			{
				//activeSprite.sprite = gameplaySprites[1];	
				animationController.Play("DownRight");
			}
			else
			{
				animationController.Play("Down");
			}
		}
		else if (right)
		{
			animationController.Play("Right");
		}
		else if (left)
		{
			animationController.Play("Left");
		}	


		// if (left)
		// {
		// 	activeSprite.flipX = true;
		// }
		// else if (right)
		// {
		// 	activeSprite.flipX = false;
		// }
		
		
		
	}
	
}
