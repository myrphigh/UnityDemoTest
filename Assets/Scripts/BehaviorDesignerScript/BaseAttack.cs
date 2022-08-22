using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BaseAttack : Action
{
    public GameObject attackObject;

    public float damage = 3f;

    public override void OnStart()
    {
        if(attackObject == null)
        {
            Debug.Log("failed attack");
            return;
        }
        GameObject attackingObject = GameObject.Instantiate(attackObject,transform.position,transform.rotation);
        attackingObject.transform.SetParent(transform,true);
    }

}
