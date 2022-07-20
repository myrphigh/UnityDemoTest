using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
	public Skill sk;
	private GameObject ob;
	void OnTriggerEnter(Collider other)
	{
		ob = other.gameObject;
		if(ob.tag == "Enemy")
		{
			float temp = ob.GetComponent<Enemy>().returnPoint();
			sk.reward(temp);
			Destroy(ob);
		}
	}
}
