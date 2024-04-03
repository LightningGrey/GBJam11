using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
	
	public SpriteRenderer activeSprite;
	public List<Sprite> gameplaySprites = new List<Sprite>();
	public Animator animationController;
	
	
	private void OnEnable()
	{
		GameplayManager.deathTrigger += UpdateDeathSprite;
		GameplayManager.clearTrigger += ClearAnimation;
	}
	private void OnDisable()
	{
		GameplayManager.deathTrigger -= UpdateDeathSprite;
		GameplayManager.clearTrigger -= ClearAnimation;
	}
	
	
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
			if (left || right)
			{	
				animationController.Play("UpRight");
			}
			else
			{
				animationController.Play("Up");
			}
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
	
	public void UpdateDeathSprite()
	{
		//animationController.updateMode = AnimatorUpdateMode.UnscaledTime;
		animationController.Play("Death");
	}
	public void DeathAnimationFinish()
	{
		//animationController.updateMode = AnimatorUpdateMode.Normal;
	 	//GBManager.Instance.ReloadScene();
	}
	
	public void ClearAnimation()
	{
		animationController.Play("Clear");
	}
	
}
