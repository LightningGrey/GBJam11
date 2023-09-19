using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
	
	public SpriteRenderer activeSprite;
	public List<Sprite> gameplaySprites = new List<Sprite>();
	
	
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	
	public void UpdateGameplaySprite(bool up, bool down, bool left, bool right)
	{
		if (left)
		{
			activeSprite.sprite = gameplaySprites[1];
			activeSprite.flipX = true;
		}
		else
		{
			if (up)
			{
				activeSprite.sprite = gameplaySprites[2];
				activeSprite.flipX = false;
			}
			else if (down)
			{
				activeSprite.sprite = gameplaySprites[0];
				activeSprite.flipX = false;
			}
			else if (right)
			{
				activeSprite.sprite = gameplaySprites[1];
				activeSprite.flipX = false;
			}
		}

		
		
	}
	
}
