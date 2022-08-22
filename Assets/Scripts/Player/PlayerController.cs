using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : Singleton<PlayerController>
{
    public GameObject renderedObject;
    public Material[] inputMaterial;
    public Material stealthMaterial;

    public GameObject[] skillPointHolders;

    [SerializeField]
    public static int playerStateId = 1;
    public static float playerHealth;
    public static float playerMaxHealth;
    public int[] playerSkillPoints;

    public static int inTouchWithFloorId = 2;
    public static bool stealState;
    public float stealSec = 5f;

    public Slider switchSlider;
    public Slider healthSlider;
    public Slider stealthSlider;

    public float healSpeed = 1f;
    public float damagingSpeed = 1f;
    void Start()
    {
        stealState = false;
        playerHealth = healthSlider.value;
        playerMaxHealth = healthSlider.maxValue;
    }
    void Update()
    {
        switchSlider.value -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && switchSlider.value == 0 && stealState == false)
        {
            SwitchState();
        }

        if(playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
        UpdateTouchingResult();

        healthSlider.value = playerHealth;

        //detect player death
        if (playerHealth <= 0f)
        {
            LevelManager.LevelRestart();
        }
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
                gameObject.layer = LayerMask.NameToLayer("PlayerRed"); 
                UseSkillPoint(1);
            }
        }
        else
        {
            int.TryParse(skillPointHolders[0].GetComponent<TextMeshProUGUI>().text, out skillPoint);
            if (skillPoint > 0)
            {
                skillPoint--;
                playerStateId = 0;
                gameObject.layer = LayerMask.NameToLayer("PlayerPurple");
                UseSkillPoint(0);
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
                playerHealth -= Time.deltaTime * damagingSpeed;
            }
            else
            {
                playerHealth += Time.deltaTime * healSpeed;
            }
        }
    }

    public void UseSkillPoint(int id)
    {
        int temp;
        int.TryParse(skillPointHolders[id].GetComponent<TextMeshProUGUI>().text, out temp);
        skillPointHolders[id].GetComponent<TextMeshProUGUI>().text = (temp - 1).ToString();
    }
    public float TryBigJump()
    {
        if(playerStateId == 1)
        {
            int skillPoint;
            int.TryParse(skillPointHolders[1].GetComponent<TextMeshProUGUI>().text, out skillPoint);
            if(skillPoint > 0 && inTouchWithFloorId == 1)
            {
                UseSkillPoint(1);
                return 3f;
            }
        }
        return 1f;
    }

    public bool TryStealth()
    {
        if(playerStateId == 0)
        {
            int skillPoint;
            int.TryParse(skillPointHolders[0].GetComponent<TextMeshProUGUI>().text, out skillPoint);
            if (skillPoint > 0 && inTouchWithFloorId == 0 && stealState == false)
            {
                UseSkillPoint(0);
                stealState = true;
                StartCoroutine(StealthC());
                return true;
            }
        }
        return false;
    }

    IEnumerator StealthC()
    {
        stealthSlider.gameObject.SetActive(true);
        renderedObject.GetComponent<SkinnedMeshRenderer>().material = stealthMaterial;
        yield return new WaitForSeconds(stealSec);
        stealState = false;
        renderedObject.GetComponent<SkinnedMeshRenderer>().material = inputMaterial[playerStateId];
        Debug.Log("End stealth");
    }

    public void AddSkillPoint(int targetId, int amount)
    {
        int skillPoint;
        int.TryParse(skillPointHolders[targetId].GetComponent<TextMeshProUGUI>().text, out skillPoint);

        skillPoint += amount;

        skillPointHolders[targetId].GetComponent<TextMeshProUGUI>().text = skillPoint.ToString();
    }
}
