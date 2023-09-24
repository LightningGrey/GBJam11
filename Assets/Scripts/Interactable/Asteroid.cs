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
	public int speed;
	public Vector3 endLocation;

	
	
	public void OnEnable()
	{
		if (type == AsteroidType.XLOOP)
		{
			
		}
		if (type == AsteroidType.YLOOP)
		{
			
		}
	}
	
	public void OnDisable()
	{
		DOTween.KillAll();
	}
	
	
}
