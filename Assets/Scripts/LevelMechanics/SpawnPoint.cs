using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == true)
        {
            int index;
            int.TryParse(this.transform.name, out index);
            LevelManager.currentLevelIndex = index;
        }
    }
}
