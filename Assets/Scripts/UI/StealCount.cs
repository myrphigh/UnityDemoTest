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
