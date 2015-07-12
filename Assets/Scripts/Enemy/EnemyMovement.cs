using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
	Rigidbody enemyRBody;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
		enemyRBody = GetComponent<Rigidbody> ();
    }


    void Update ()
    {
		Vector3 directionToPlayer = player.position - enemyRBody.transform.position;
		Quaternion newRotation = Quaternion.LookRotation(directionToPlayer);
		enemyRBody.MoveRotation(newRotation);
    }
}
