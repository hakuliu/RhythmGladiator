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
			int i = Application.loadedLevel;
			//Application.LoadLevel(i+1);
		}
	}
}
