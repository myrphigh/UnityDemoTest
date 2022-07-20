using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGate : MonoBehaviour
{
	public GameObject[] Enemies;
    // Start is called before the first frame update
    private bool trigger = true;
    private int len = 0;
    void Start()
    {
        len = Enemies.Length;

    }

    // Update is called once per frame
    void Update()
    {
    	trigger = true;
        for(int i = 0; i < len; i++){
        	if(Enemies[i]!=null){
        		trigger = false;
        		break;
        	}
        }
        if(trigger)
        {
        	Destroy(this.gameObject);
        }
    }
}
