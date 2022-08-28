using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGate : MonoBehaviour
{
    public float TimerTarget = 1f;
    private float tempTimer = 0f;

    public GameObject targetEnemyGroup;
    private void Start()
    {
        targetEnemyGroup = transform.parent.GetChild(3).gameObject;
    }
    void FixedUpdate()
    {
        tempTimer += Time.deltaTime;
        if(tempTimer > TimerTarget)
        {
            tempTimer = 0f;
            if (targetEnemyGroup.transform.childCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
