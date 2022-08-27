using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BaseAttack : Action
{
    public GameObject attackObject;

    public float damage = 3f;
    public bool isWaited;
    public float waitTime;

    public override void OnStart()
    {
        if(attackObject == null)
        {
            Debug.Log("failed attack");
            return;
        }
        if (isWaited)
        {
            StartCoroutine(ShootBullet());
        }
        else
        {
            GameObject attackingObject = GameObject.Instantiate(attackObject, transform.position, transform.rotation);
            attackingObject.transform.SetParent(transform, true);
        }
    }
    IEnumerator ShootBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject attackingObject = GameObject.Instantiate(attackObject, transform.position, transform.rotation);
            attackingObject.transform.SetParent(transform, true);
        }
    }
}
