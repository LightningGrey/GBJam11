using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public enum AsteroidType
{
	STATIC,
	XLOOP,
	YLOOP
}


public class Asteroid : MonoBehaviour
{
	
	public int damage;
	
	public float knockbackDist;
	
	public AsteroidType type = AsteroidType.STATIC;
	
	// moving ones only
	public int duration;
	
	public Vector3 originalLocation;
	public float endLocation;
	
	private Tweener movementTween;
	
	public void Start()
	{
		originalLocation = transform.localPosition;
	}
	
	
	public void OnEnable()
	{
		
		
		if (type == AsteroidType.XLOOP)
		{
			XMovement();
		}
		if (type == AsteroidType.YLOOP)
		{
			YMovement();
		}
	}
	
	public void OnDisable()
	{
		movementTween.Kill();
		transform.position = originalLocation;
	}
	
	void XMovement()
	{
		movementTween = transform.DOLocalMoveX(endLocation, duration).SetAutoKill(false).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
	}
	
	void YMovement()
	{
		movementTween = transform.DOLocalMoveY(endLocation, duration).SetAutoKill(false).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
	}
	
	
	
}
