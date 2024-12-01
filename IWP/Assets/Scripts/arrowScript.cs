using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public int damage = 10;
    private Enemy enemyScript;
    private archerTower archerTowerScript;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the arrow hit an enemy
        if (collision.gameObject.CompareTag("NormalEnemy"))
        {
            // Get the EnemyScript attached to the enemy object
            enemyScript = collision.gameObject.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                // Call the TakeDamage function from EnemyScript
                enemyScript.TakeDamage(damage);
            }

            // Destroy the arrow after it hits
            Destroy(gameObject);
        }

        // Check if the arrow hit an enemy
        if (collision.gameObject.CompareTag("ArcherTower"))
        {
            // Get the EnemyScript attached to the enemy object
            archerTowerScript = collision.gameObject.GetComponent<archerTower>();

            if (archerTowerScript != null)
            {
                // Call the TakeDamage function from EnemyScript
                archerTowerScript.TakeDamage(damage);
            }

            // Destroy the arrow after it hits
            Destroy(gameObject);
        }
    }
}
