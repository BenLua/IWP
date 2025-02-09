using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerTower : MonoBehaviour
{
    public Transform basePoint;
    public Transform shootingPoint;
    public float TowerRange = 10f;
    public LayerMask targetLayer;
    public GameObject arrowPrefab;
    public int shotSpeed;
    public int towerHP = 100;

    private bool canShoot = true;

    public float ShootingDelay;
    public float OriginalDelay;

    void Start()
    {
        OriginalDelay = ShootingDelay;
    }

    void Update()
    {
        // Perform the overlap check at the center of the sphere with the given radius
        Collider[] collidersInRange = Physics.OverlapSphere(basePoint.position, TowerRange, targetLayer);

        Collider nearestCollider = null;
        float nearestDistance = TowerRange + 20;

        // Check if any colliders were detected
        if (collidersInRange.Length > 0)
        {
            foreach (Collider collider in collidersInRange)
            {
                // Calculate distance of all the colliders in range
                float distance = Vector3.Distance(basePoint.position, collider.transform.position);

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
            //Debug.Log("No objects within the tower range.");
        }
    }

    // Coroutine to shoot with a delay
    private IEnumerator ShootAtNearestWithDelay(Collider nearestEnemy)
    {
        canShoot = false; // Disable shooting until the delay is over

        // Calculate the direction towards the nearest collider
        Vector3 direction = (nearestEnemy.transform.position - shootingPoint.position).normalized;

        // Spawn the projectile at the specified position (e.g., in front of the tower)
        Quaternion spawnRotation = Quaternion.LookRotation(-direction); // Rotate the arrow to face the target

        // Spawn the projectile at the current position
        GameObject projectile = Instantiate(arrowPrefab, shootingPoint.position, spawnRotation);

        // Get the Rigidbody component of the projectile (ensure it's not null)
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Lock rotation on the Rigidbody to prevent it from spinning
            rb.freezeRotation = true;

            // Apply force to the Rigidbody in the direction of the nearest collider
            rb.AddForce(direction * shotSpeed, ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogWarning("The projectile does not have a Rigidbody component!");
        }

        // Wait for 2 seconds before allowing the next shot
        yield return new WaitForSeconds(ShootingDelay);

        // Re-enable shooting after the delay
        canShoot = true;
    }

    // Draw the sphere in the Scene view for debugging purposes
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(basePoint.position, TowerRange);
    }

    //Special Feature: if tower is 0 hp the tower will be "DISABLED" until it is "HEALED" or after a period of time has passed======================================================================================
    //==============================================================================================================================================================================================================
    //==============================================================================================================================================================================================================
    public string CheckCurrentState()
    {
        float originalRange;
        originalRange = TowerRange;
        if (originalRange <= 0)
        {
            Debug.Log("Range cannot be changed from 0 to 0");
            return " ";
        }


        if (towerHP <= 0)
        {
            TowerRange = 0;
            return "dead";
        }
        else if (towerHP > 0)
        {
            TowerRange = originalRange;
            return "alive";
        }

        return " ";
    }

    public void TakeDamage(int damage)
    {
        towerHP -= damage;
        if (CheckCurrentState() == "dead")
        {
            //do code to give visual sign that tower is dead
            Debug.Log("Tower Dead");
        }
    }
}
