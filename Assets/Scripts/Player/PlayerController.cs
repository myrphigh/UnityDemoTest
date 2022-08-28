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
    public Material strongMaterial;

    public GameObject[] skillPointHolders;

    [SerializeField]
    public static int playerStateId = 1;
    public static float playerHealth;
    public static float playerMaxHealth;
    public int[] playerSkillPoints;

    public static int inTouchWithFloorId = 2;
    public static bool stealState;
    public static bool strongState;
    public float stealSec = 5f;

    public Slider switchSlider;
    public Slider healthSlider;
    public Slider stealthSlider;

    public float healSpeed = 1f;
    public float damagingSpeed = 1f;
    void Start()
    {
        stealState = false;
        strongState = false;

        playerHealth = healthSlider.value;
        playerMaxHealth = healthSlider.maxValue;
    }
    void Update()
    {
        switchSlider.value -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && switchSlider.value == 0 && stealState == false && strongState == false)
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

        if(strongState && Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0f, 50f, 0f));
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
        inTouchWithFloorId = 2;
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
    public float TryBigJump(float targetH)
    {
        if(playerStateId == 1)
        {
            if(inTouchWithFloorId == 1 && strongState)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb.velocity.magnitude > 0.2f)
                {
                    //UseSkillPoint(1);
                    return targetH;
                }
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

    public bool TryStrong()
    {
        if (playerStateId == 1)
        {
            int skillPoint;
            int.TryParse(skillPointHolders[1].GetComponent<TextMeshProUGUI>().text, out skillPoint);
            if (skillPoint > 0 && inTouchWithFloorId == 1 && strongState == false)
            {
                UseSkillPoint(1);
                strongState = true;
                StartCoroutine(StrongC());
                return true;
            }
        }
        return false;
    }

    IEnumerator StrongC()
    {
        stealthSlider.gameObject.SetActive(true);
        renderedObject.GetComponent<SkinnedMeshRenderer>().material = strongMaterial;
        yield return new WaitForSeconds(stealSec);
        strongState = false;
        renderedObject.GetComponent<SkinnedMeshRenderer>().material = inputMaterial[playerStateId];
        Debug.Log("End strong");
    }

    IEnumerator StealthC()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerStealth");
        stealthSlider.gameObject.SetActive(true);
        renderedObject.GetComponent<SkinnedMeshRenderer>().material = stealthMaterial;
        yield return new WaitForSeconds(stealSec);
        gameObject.layer = LayerMask.NameToLayer("PlayerPurple");
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
