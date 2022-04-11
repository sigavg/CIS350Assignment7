/* Luke Lesimple
 * Assignment 7
 * spawn manager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRange = 9;
    public int enemyCount;
    public int waveNum = 1;

    public Text winscreen;
    public static bool win;
    public int wincond = 10;

    public Text wavetext;

    public Text tutorial;

    // Start is called before the first frame update
    void Start()
    { 
        win = false;
        winscreen.enabled = false;
        spawnEnemyWave(waveNum);
        spawnPowerup(1);

        Time.timeScale = 0;
    }

    private void spawnEnemyWave(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void spawnPowerup(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }


    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            tutorial.enabled = false;
        }

        wavetext.text = "Wave: " + waveNum;

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount <= 0)
        {
            if (waveNum >= wincond)
            {
                win = true;
                winscreen.enabled = true;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene("Prototype 4");
                }
            }
            else
            {
                waveNum++;
                spawnEnemyWave(waveNum);
                spawnPowerup(1);
            }
        }
    }
}