using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
	public float restartDelay = 5f;
	public AudioSource bgm;
	public Text timer;

    Animator anim;
	float restartTimer;
	float timeleft;

    void Awake()
    {
		timeleft = bgm.clip.length;
        anim = GetComponent<Animator>();
    }
	

    void Update()
    {
		timeleft -= Time.deltaTime;
		timer.text = "Time: " + timeToString(timeleft);
		if (timeleft <= 0) {
			gameover ();
		}
        if (playerHealth.currentHealth <= 0)
        {
			gameover();
		}
    }
	void gameover() {
		anim.SetTrigger("GameOver");
		
		restartTimer += Time.deltaTime;
		
		if(restartTimer >= restartDelay)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	string timeToString(float seconds) {
		int minutes = (int)(seconds / 60);
		seconds = seconds % 60;
		int secints = (int)seconds;
		return "" + minutes + ":" + secints;
	}
}
