using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyTypeId = 0;

    public void selfDestroy()
    {
        Destroy(gameObject);
    }
}
