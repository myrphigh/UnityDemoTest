using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using BehaviorDesigner;

public class BaseAttack : Action
{
    public GameObject attackObject;

    public float damage = 3f;
    public bool isWaited;
    public float waitTime;

    public SharedFloat timerTarget;

    private float Timer = 0f;
    public override void OnStart()
    {
        Timer = 0f;
        if(attackObject == null)
        {
            Debug.Log("failed attack");
            return;
        }
        if (isWaited && timerTarget.Value >= waitTime)
        {
            GameObject attackingObject = GameObject.Instantiate(attackObject, transform.position, transform.rotation);
            attackingObject.transform.SetParent(null, true);
            timerTarget.Value = 0f;
        }
        else
        {
            GameObject attackingObject = GameObject.Instantiate(attackObject, transform.position, transform.rotation);
            attackingObject.transform.SetParent(transform, true);
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }
    IEnumerator ShootBullet()
    {
            yield return new WaitForSeconds(waitTime);
            GameObject attackingObject = GameObject.Instantiate(attackObject, transform.position, transform.rotation);
            attackingObject.transform.SetParent(transform, true);
    }
}
