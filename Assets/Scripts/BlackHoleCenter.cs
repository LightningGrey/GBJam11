using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCenter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			GameplayManager.Instance.OnDeath();
		}
		
	}
}
