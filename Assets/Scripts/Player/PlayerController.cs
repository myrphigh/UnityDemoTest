using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : Singleton<PlayerController>
{
    public GameObject renderedObject;
    public Material[] inputMaterial;

    public GameObject[] skillPointHolders;

    [SerializeField]
    public static int playerStateId = 1;
    public static float playerHealth;
    public static float playerMaxHealth;
    public int[] playerSkillPoints;

    public static int inTouchWithFloorId = 2;

    public Slider switchSlider;
    public Slider healthSlider;
    void Start()
    {
        playerHealth = healthSlider.value;
        playerMaxHealth = healthSlider.maxValue;
    }
    void Update()
    {
        switchSlider.value -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && switchSlider.value == 0)
        {
            SwitchState();
        }
        if (playerHealth == 0f)
        {
            LevelManager.LevelRestart();
        }

        if(playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }

        healthSlider.value = playerHealth; 
    }

    void updatePlayerState()
    {
        renderedObject.GetComponent<SkinnedMeshRenderer>().material = inputMaterial[playerStateId];
        switchSlider.value = switchSlider.maxValue;
    }

    public void SwitchState()
    {
        int skillPoint;
        if (playerStateId == 0)
        {
            int.TryParse(skillPointHolders[1].GetComponent<TextMeshProUGUI>().text, out skillPoint);
            if (skillPoint > 0)
            {
                skillPoint--;
                playerStateId = 1;
                skillPointHolders[1].GetComponent<TextMeshProUGUI>().text = skillPoint.ToString();
            }
        }
        else
        {
            int.TryParse(skillPointHolders[0].GetComponent<TextMeshProUGUI>().text, out skillPoint);
            if (skillPoint > 0)
            {
                skillPoint--;
                playerStateId = 0;
                skillPointHolders[0].GetComponent<TextMeshProUGUI>().text = skillPoint.ToString();
            }
        }

        updatePlayerState();
    }

    public static void PlayerRestart()
    {
        playerHealth = playerMaxHealth;  
    }

    void UpdateTouchingResult()
    {
        if(inTouchWithFloorId != 2)
        {
            if(inTouchWithFloorId != playerStateId)
            {
                playerHealth -= Time.deltaTime;
            }
            else
            {
                playerHealth += Time.deltaTime;
            }
        }
    }
}
