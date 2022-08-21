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

    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.inTouchWithFloorId = floorTypeId;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.inTouchWithFloorId = 2;
        }
    }
}
