using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 30;
	public int startingShield = 70;
    public int currentHealth;
	public int currentShield;
    public Slider healthSlider;
	public Slider shieldSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
	public Color flashColour = new Color (1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
	float damageTimer;
	float timeToRegenShield = 6f;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
		currentShield = startingShield;
    }


    void Update ()
    {
		damageTimer += Time.deltaTime;
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
		checkAndRegenShield ();
        damaged = false;
    }

	private void checkAndRegenShield() 
	{
		if (damageTimer >= timeToRegenShield && currentShield < startingShield) {
			currentShield ++;
			shieldSlider.value = currentShield;
		}
	}
	public void HandleDamageEvent(DamageEvent e) {
		TakeDamage (e.Value);
	}
    private void TakeDamage (int amount)
    {

		damageTimer = 0;

        damaged = true;

		currentShield -= amount;
		if (currentShield < 0) {
			currentHealth += currentShield;
			currentShield = 0;

		}
		shieldSlider.value = currentShield;
        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

	public void Fall() {
		Death ();
	}

    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}
