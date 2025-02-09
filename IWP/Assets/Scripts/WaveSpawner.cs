using System.Collections;
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
    private float additionalHealth;
    public TMP_Text WaveText;

    int waveNumber2;

    private Enemy[] allEnemyScripts;

    private int waveNumber = 0;
    private bool isSpawningWave = false; // Flag to track wave spawning state

    void Update()
    {
        // Only proceed if not currently spawning a wave
        if (!isSpawningWave)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves; // Reset the countdown after the wave finishes
            }
        }

        allEnemyScripts = FindObjectsOfType<Enemy>();
    }

    IEnumerator SpawnWave()
    {
        isSpawningWave = true; // Set flag to indicate wave is being spawned
        waveNumber++;
        WaveText.text = "WAVES: " + waveNumber.ToString();

        for (int i = 0; i < waveNumber; i++)
        {
            waveNumber2++;

            Transform enemyToSpawn = baseEnemy;

            // Alternate between baseEnemy and attackingEnemy after wave 5
            if (waveNumber >= 5)
            {
                enemyToSpawn = (i % 2 == 0) ? baseEnemy : attackingEnemy;
            }

            if (waveNumber2 >= 10)
            {
                additionalHealth += 10f;
                enemyToSpawn.GetComponent<Enemy>().health += additionalHealth;
                Debug.Log(enemyToSpawn.GetComponent<Enemy>().health.ToString());
                waveNumber2 = 0;
            }

            Instantiate(enemyToSpawn, enemySpawn.position, enemySpawn.rotation);

            // Wait before spawning the next enemy
            yield return new WaitForSeconds(enemyCountdown);
        }

        // Wait for the time between waves after all enemies are spawned
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawningWave = false; // Reset the flag after the wave is finished
    }
}
