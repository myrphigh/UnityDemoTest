using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public static int currentLevelIndex = 0;

    [SerializeField]
    public GameObject[] spawnPointOfEachLevel;

    public static Vector3[] spawnPointPositions;

    public static GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawnPointPositions = new Vector3[spawnPointOfEachLevel.Length];
        foreach(GameObject entry in spawnPointOfEachLevel)
        {
            int index;
            int.TryParse(entry.transform.name,out index);
            spawnPointPositions[index] = entry.transform.position;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelRestart();
        }
    }

    public static  void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.transform.position = spawnPointPositions[currentLevelIndex];
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        PlayerController.PlayerRestart();
    }
}
