using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
	public Slider skillMeter;
	public TimeChange tc;
	public int playerState = 0;
	private bool Skill1IsOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && skillMeter.value>0)
        {
        	EnterSkill1();
        } 
        else if(Input.GetKeyUp(KeyCode.LeftShift) && Skill1IsOn){
        	QuitSkill1();
        } 
            else if(skillMeter.value<=0 && Skill1IsOn){
        	QuitSkill1();
        }
        if(Skill1IsOn)
        {
        	skillMeter.value -= Time.deltaTime;
        }
        if(playerState == 1)
        {
        	skillMeter.value = 10f;
        	playerState = 0;
        }
    }
    void EnterSkill1()
    {
    	Debug.Log("es1");
    	//tc.slowMotion();
    	Skill1IsOn = true;
        playerState = 2;
    }

    void QuitSkill1()
    {
    	Debug.Log("qs1");
    	//tc.stopSlowMotion();
    	Skill1IsOn = false;
        playerState = 0;
    }

    public void Reset()
    {
    	playerState = 1;
    }

    public int showPlayerState()
    {
        return playerState;
    }

    public void reward(float rewardPoint)
    {
        skillMeter.value += rewardPoint;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Gift")
        {
            reward(10f);
            Destroy(collision.gameObject);
        }
    }
}
