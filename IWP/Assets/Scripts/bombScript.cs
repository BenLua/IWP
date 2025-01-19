using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    public int damage = 10;
    private int damageRadius = 4;
    private Enemy enemyScript;
    private bombTower bombTowerScript;
    public ParticleSystem explosionEffect;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter(Collision collision)
    {
        //// Check if the arrow hit an enemy
        //if (collision.gameObject.CompareTag("NormalEnemy"))
        //{
        //    // Get the EnemyScript attached to the enemy object
        //    enemyScript = collision.gameObject.GetComponent<Enemy>();

        //    if (enemyScript != null)
        //    {
        //        // Call the TakeDamage function from EnemyScript
        //        enemyScript.TakeDamage(damage);
        //    }

        //    // Destroy the arrow after it hits
        //    Destroy(gameObject);
        //}

        // Check if the arrow hit an enemy
        if (collision.gameObject.CompareTag("ArcherTower"))
        {
            // Get the EnemyScript attached to the enemy object
            bombTowerScript = collision.gameObject.GetComponent<bombTower>();

            if (bombTowerScript != null)
            {
                // Call the TakeDamage function from EnemyScript
                bombTowerScript.TakeDamage(damage);
            }

            // Destroy the arrow after it hits
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("NormalEnemy") || collision.gameObject.CompareTag("Floor"))
        {
            // Apply damage to nearby enemies
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);

            // Loop through the colliders and apply damage to enemies
            foreach (Collider col in hitColliders)
            {
                enemyScript = col.gameObject.GetComponent<Enemy>();

                if (enemyScript != null)
                {
                    // Call the TakeDamage function from EnemyScript
                    enemyScript.TakeDamage(damage);
                }
            }

            // Play explosion particle effect
            if (explosionEffect != null)
            {
                Debug.Log(collision.gameObject.name);
                explosionEffect.transform.position = collision.gameObject.transform.position;
                var render = explosionEffect.GetComponent<Renderer>();
                Material m = render.material;
                m.renderQueue = 3001;
                explosionEffect.Play(); // Play the explosion effect
            }

            // Destroy the bomb object after a short delay (enough time for the explosion effect to play)
            Destroy(gameObject);
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, damageRadius);
    }
}
