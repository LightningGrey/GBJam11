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
	
	public Coroutine flashCoroutine;
	
	public Coroutine textCoroutine;
	
	public Interactable currentInteractable;
	
	
	
	private void OnEnable()
	{
		GameplayManager.deathTrigger += CloseFlash;
		PlayerInput.interact += OnInteract;
		ScreenTransition.TransitionIntoTrigger += DisablePlayer;
		ScreenTransition.TransitionFromTrigger += EnablePlayer;
	}
	
	private void OnDisable()
	{
		GameplayManager.deathTrigger -= CloseFlash;
		PlayerInput.interact -= OnInteract;
		ScreenTransition.TransitionIntoTrigger -= DisablePlayer;
		ScreenTransition.TransitionFromTrigger -= EnablePlayer;
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
			//Debug.Log(currentInteractable);
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
			
			flashCoroutine = StartCoroutine(SpriteFlash());
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
		if (flashCoroutine != null)
		{
			StopCoroutine(flashCoroutine);
		}
	}
	
	public void EnablePlayer(Transform transformParam)
	{
		transform.position = transformParam.position;
		rb.isKinematic = false;
	}
	
	public void DisablePlayer()
	{
		rb.isKinematic = true;
	}
	
	
	void OnInteract()
	{
		if (currentInteractable != null)
		{
			switch(currentInteractable.type)
			{
				case InteractableType.BATTERY:
				{
					GameplayManager.Instance.Collect(true, currentInteractable.objectID);
					currentInteractable.gameObject.SetActive(false);
					
					UIManager.Instance.UICollectText(true);
					
					
					break;
				}
				case InteractableType.PART:
				{
					GameplayManager.Instance.Collect(false, currentInteractable.objectID);
					currentInteractable.gameObject.SetActive(false);
					
					UIManager.Instance.UICollectText(false);
					
					break;
				}
				case InteractableType.READABLE:
				{
					UIManager.Instance.Read(currentInteractable.interactString);
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
