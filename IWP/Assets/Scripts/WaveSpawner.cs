using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform baseEnemy;
    public Transform attackingEnemy;
    public Transform enemySpawn;
    public float timeBetweenWaves = 5f;
    public float countdown = 2f;
    public float enemyCountdown = 0.5f;
    public TMP_Text WaveText;

    private int waveNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check when round ends
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        WaveText.text = "WAVES: " + waveNumber.ToString();

        for (int i = 0; i < waveNumber; i++)
        {
            if (waveNumber >= 5)
            {
                if (i % 2 == 0)
                    SpawnEnemy(baseEnemy);
                else if (i % 2 == 1)
                    SpawnEnemy(attackingEnemy);
                else
                    Debug.LogError("spawning of enemies are not working");
            }
            else
                SpawnEnemy(baseEnemy);

            yield return new WaitForSeconds(enemyCountdown);
        }
    }

    void SpawnEnemy(Transform enemy)
    {
        Instantiate(enemy, enemySpawn);
    }


}
