using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Respawn : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Transform respawnPoint;

	//public GameObject camera;
	public Skill sk;
	private Rigidbody rb;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player"){
			player.transform.position = respawnPoint.transform.position;
			//GetComponent<Camera>().transform.position = respawnPoint.transform.position;
			rb = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody>();
			rb.velocity = new Vector3(0,0,0);
			sk.Reset();
		}
	}
}
