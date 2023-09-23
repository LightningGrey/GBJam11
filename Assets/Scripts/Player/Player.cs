using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	
	public SpriteRenderer sprite;
	public Rigidbody2D rb;
	
	public bool hitstun;
	public float hitstunTimer = 0.4f;
	public Coroutine currentCoroutine;
	
	private void OnEnable()
	{
		GameplayManager.deathTrigger += CloseFlash;
	}
	private void OnDisable()
	{
		GameplayManager.deathTrigger -= CloseFlash;
	}

	// Update is called once per frame
	void Update()
	{
		
		if (GBManager.Instance.activeControl && hitstun)
		{
			hitstunTimer -= Time.deltaTime;

			if (hitstunTimer <= 0f)
			{
				hitstun = false;
				hitstunTimer = 0.4f;
				//sprite.enabled = true;
			}
		}

	}

	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Obstacles") && !hitstun)
		{
			OnHit(other);
		}
	}
	
	void OnCollisionStay2D(Collision2D other)
	{
		if (other.collider.CompareTag("Obstacles") && !hitstun)
		{
			OnHit(other);
		}
	}
	
	void OnHit(Collision2D other)
	{
		var asteroidClass = other.gameObject.GetComponent<Asteroid>();		
		GameplayManager.Instance.DepleteEnergy(asteroidClass.damage);
		hitstun = true;
			
		var direction = (other.transform.position - gameObject.transform.position).normalized;
		gameObject.transform.position -= direction * asteroidClass.knockbackDist;
		currentCoroutine = StartCoroutine(SpriteFlash());
	}
	
	
	public IEnumerator SpriteFlash()
	{
		sprite.enabled = false;
		yield return new WaitForSeconds(0.05f);
				
		sprite.enabled = true;
		yield return new WaitForSeconds(0.3f);	
		
		var flashTimer = 3;

		while(flashTimer > 0)
		{
			sprite.enabled = false;
			yield return new WaitForSeconds(0.2f);
				
			sprite.enabled = true;
			yield return new WaitForSeconds(0.2f);	
			
			flashTimer--;
		} 
	
	}

	public void CloseFlash()
	{
		if (currentCoroutine != null)
		{
			StopCoroutine(currentCoroutine);
		}
	}

}
