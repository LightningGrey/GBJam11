using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	
	[Header("References")]
	public SpriteRenderer sprite;
	public Rigidbody2D rb;
	public AudioClip hitSFX;
	
	
	[Header("Variables")]
	public bool hitstun;
	public bool iframes = false;
	public float hitstunTimer = 0.4f;
	public Coroutine currentCoroutine;
	public Interactable currentInteractable;
	
	
	
	private void OnEnable()
	{
		GameplayManager.deathTrigger += CloseFlash;
		PlayerInput.interact += OnInteract;
	}
	private void OnDisable()
	{
		GameplayManager.deathTrigger -= CloseFlash;
		PlayerInput.interact -= OnInteract;
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
		if (other.collider.CompareTag("Obstacles") && !iframes)
		{
			OnHit(other);
		}
	}
	
	void OnCollisionStay2D(Collision2D other)
	{
		if (other.collider.CompareTag("Obstacles") && !iframes)
		{
			OnHit(other);
		}
	}
	
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Interactable") && !iframes)
		{
			currentInteractable = other.gameObject.GetComponent<Interactable>();
		}
		
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		currentInteractable = null;
	}
	
	
	void OnHit(Collision2D other)
	{
		var asteroidClass = other.gameObject.GetComponent<Asteroid>();		
		GameplayManager.Instance.DepleteEnergy(asteroidClass.damage);
		
		GBManager.Instance.gb.Sound.PlaySound(hitSFX);
		hitstun = true;
		iframes = true;
		
		if (GameplayManager.Instance.energyMeter > 0)
		{
			// movement	
			var direction = (other.transform.position - gameObject.transform.position).normalized;
			gameObject.transform.position -= direction * asteroidClass.knockbackDist;
			
			currentCoroutine = StartCoroutine(SpriteFlash());
		}
	
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
	
		iframes = false;
	}

	public void CloseFlash()
	{
		if (currentCoroutine != null)
		{
			StopCoroutine(currentCoroutine);
		}
	}
	
	
	void OnInteract()
	{
		if (currentInteractable != null)
		{
			switch(currentInteractable.type)
			{
				case InteractableType.BATTERY:
				{
					GameplayManager.Instance.Collect(true);
					currentInteractable.gameObject.SetActive(false);
					StartCoroutine(UIManager.Instance.UICollectText(true));
					break;
				}
				case InteractableType.PART:
				{
					GameplayManager.Instance.Collect(false);
					currentInteractable.gameObject.SetActive(false);
					StartCoroutine(UIManager.Instance.UICollectText(false));
					break;
				}
				case InteractableType.READABLE:
				{
					//GameplayManager.Instance.Collect(true);
					break;
				}
				case InteractableType.SHUTTLE:
				{
					GameplayManager.Instance.ClearLevel();
					break;
				}
			}
			
		}
		
	}

}
