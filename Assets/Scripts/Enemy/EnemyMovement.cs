using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
	Rigidbody enemyRBody;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
		enemyRBody = GetComponent<Rigidbody> ();
    }


    void Update ()
    {
		Vector3 directionToPlayer = player.position - enemyRBody.transform.position;
		Quaternion newRotation = Quaternion.LookRotation(directionToPlayer);
		enemyRBody.MoveRotation(newRotation);
    }
}
