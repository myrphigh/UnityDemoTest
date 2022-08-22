using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
	public GameObject deathParticle;
	public GameObject deathReward;

	public GameObject parentEnemy;
	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player") && parentEnemy.GetComponent<EnemyController>().enemyTypeId != PlayerController.playerStateId)
        {
			GameObject particle = Instantiate(deathParticle,transform.position,transform.rotation);
			GameObject reward = Instantiate(deathReward, transform.position, transform.rotation);
			reward.GetComponent<Rigidbody>().AddForce(0f,4f,0f);
			Destroy(parentEnemy);
        }
	}
}
