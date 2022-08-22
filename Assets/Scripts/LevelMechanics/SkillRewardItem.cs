using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRewardItem : MonoBehaviour
{

    public GameObject player;
    public bool playerGetting;
    public float Speed = 5f;
    public int pointType;
    public int pointCount;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerGetting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGetting)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.transform.position) < 0.5f)
            {
                player.GetComponent<PlayerController>().AddSkillPoint(pointType, pointCount);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerGetting=true;
        }
    }
}
