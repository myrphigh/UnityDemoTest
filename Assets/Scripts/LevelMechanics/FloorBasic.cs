using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBasic : MonoBehaviour
{
    // Start is called before the first frame update
    public int floorTypeId;

    public bool inTouch;
    //damage = true healing = false
    public bool damagingOrHealing;

    private bool exitStatus = false;
    IEnumerator exitHolder;
    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.inTouchWithFloorId = floorTypeId;
            exitStatus = false;
            Debug.Log("Player Enter" + floorTypeId);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exitStatus = true;
            StartCoroutine(exitProcess());
            Debug.Log("Player Exit" + floorTypeId);
        }
    }

    IEnumerator exitProcess()
    {
        yield return new WaitForSeconds(0.5f);
        if (exitStatus)
        {
            PlayerController.inTouchWithFloorId = 2;
        }
    }
}
