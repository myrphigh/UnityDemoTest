using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StealCount : MonoBehaviour
{
    
    public GameObject player;
    public Slider slider;
    private void OnEnable()
    {
        slider.maxValue = player.GetComponent<PlayerController>().stealSec;
        slider.value = slider.maxValue;
        if(PlayerController.playerStateId == 0)
        {
            transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Vector4(0.3764706f, 0.2352941f, 1,1);
        }
        else
        {
            transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Vector4(1, 0.3411765f, 0.3764706f, 1);
        }
    }
    private void Awake()
    {
        slider = GetComponent<Slider>();
        player = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value -= Time.deltaTime;
        if(slider.value <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
