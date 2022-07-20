using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelShift : MonoBehaviour

{
	public GameObject player;
	public GameObject[] respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKeyDown(KeyCode.Keypad0))
        {
			player.transform.position = respawnPoint[0].transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
			player.transform.position = respawnPoint[1].transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
			player.transform.position = respawnPoint[2].transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
			player.transform.position = respawnPoint[3].transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
			player.transform.position = respawnPoint[4].transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
			player.transform.position = respawnPoint[5].transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
			player.transform.position = respawnPoint[6].transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad7))
        {
			player.transform.position = respawnPoint[7].transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad8))
        {
			player.transform.position = respawnPoint[8].transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad9))
        {
			player.transform.position = respawnPoint[9].transform.position;
        }
    }
}
