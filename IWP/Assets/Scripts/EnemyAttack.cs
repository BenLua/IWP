using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackSpeed = 5f;
    public LayerMask targetLayer;
    public int EnemyRange = 500;
    public GameObject arrowPrefab;
    public bool canShoot;

    //draw sphere and do attack
    void checkShootAtTower()
    {
        // Perform the overlap check at the center of the sphere with the given radius
        Collider[] collidersInRange = Physics.OverlapSphere(this.transform.position, EnemyRange, targetLayer);

        Collider nearestCollider = null;
        float nearestDistance = EnemyRange + 20;

        // Check if any colliders were detected
        if (collidersInRange.Length > 0)
        {
            foreach (Collider collider in collidersInRange)
            {
                // Calculate distance of all the colliders in range
                float distance = Vector3.Distance(this.transform.position, collider.transform.position);

                if (distance < nearestDistance)
                {
                    // Assign new nearest distance and aim at new nearest enemy
                    nearestCollider = collider;
                    nearestDistance = distance;
                }
            }

            // Shoot at the nearest enemy only if shooting is allowed
            if (canShoot)
            {
                StartCoroutine(ShootAtNearestWithDelay(nearestCollider));
            }
        }
        else
        {
            Debug.Log("No objects within the sphere.");
        }
    }

    // Coroutine to shoot with a delay
    private IEnumerator ShootAtNearestWithDelay(Collider nearestEnemy)
    {
        canShoot = false; // Disable shooting until the delay is over

        // Calculate the direction towards the nearest collider
        Vector3 direction = (nearestEnemy.transform.position - this.transform.position).normalized;

        // Spawn the projectile at the specified position (e.g., in front of the tower)
        Quaternion spawnRotation = Quaternion.LookRotation(-direction); // Rotate the arrow to face the target

        // Spawn the projectile at the current position
        GameObject projectile = Instantiate(arrowPrefab, this.transform.position, spawnRotation);

        // Get the Rigidbody component of the projectile (ensure it's not null)
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Lock rotation on the Rigidbody to prevent it from spinning
            rb.freezeRotation = true;

            // Apply force to the Rigidbody in the direction of the nearest collider
            rb.AddForce(direction * 1000, ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogWarning("The projectile does not have a Rigidbody component!");
        }

        // Wait for 2 seconds before allowing the next shot
        yield return new WaitForSeconds(2f);

        // Re-enable shooting after the delay
        canShoot = true;
    }
}
