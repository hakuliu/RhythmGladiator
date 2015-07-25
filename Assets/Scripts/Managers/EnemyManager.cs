using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public int enemyCount;

    void Start ()
    {
	
    }

	public void countDownEnemy() 
	{
		enemyCount--;
		if (enemyCount <= 0) {

		}
	}
	public void InvokeIfWinning() {
		//loadNext ();
	}

	private void loadNext() {
		int i = Application.loadedLevel;
		Application.LoadLevel(i+1);
	}
}
