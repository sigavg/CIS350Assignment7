/* Luke Lesimple
 * Assignment 7
 * spawns enemies and shifts waves
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public int enemyCount;
    public static int waveCount = 0;

    public GameObject player;

    //Win
    public Text winscreen;
    public static bool win;
    public int wincond = 10;

    //Wave
    public Text wavetext;

    //Tutorial
    public Text tutorial;
    public bool tutviewed = false;

    //Lose
    public Text losescreen;
    public static int against;

    //Start
    void Start()
    {
        win = false;
        winscreen.enabled = false;

        tutorial.enabled = true;
        
        losescreen.enabled = false;

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !tutviewed)
        {
            Time.timeScale = 1;
            tutorial.enabled = false;
            SpawnEnemyWave(waveCount);
            tutviewed = true;

            
        }

        wavetext.text = "Wave: " + waveCount;

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (waveCount >= wincond)
        {
            win = true;
            winscreen.enabled = true;
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Challenge 4");
            }
        }

        if(enemyCount == 0 && tutviewed)
        {
            if(against == waveCount)
            {
                losescreen.enabled = true;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene("Challenge 4");
                }
            }
            else if (!win)
            {
                SpawnEnemyWave(waveCount);
            }
        }
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        against = 0;
        waveCount++;

        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < waveCount; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        
        ResetPlayerPosition(); // put player back at start

    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

}
