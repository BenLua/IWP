using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpell : MonoBehaviour
{
    public LayerMask TowerLayer;
    private archerTower archerTowerScript;

    private int spellRange = 15;
    private bool canShoot;

    public float spellDuration;
    public float boostEffect;

    private void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        // Perform the overlap check at the center of the sphere with the given radius
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, spellRange, TowerLayer);

        // Check if any colliders were detected
        if (collidersInRange.Length > 0)
        {
            foreach (Collider collider in collidersInRange)
            {
                archerTowerScript = collider.GetComponent<archerTower>();
                if (canShoot)
                    StartCoroutine(BoostShootingSpeed());
            }
        }
        else
        {
            Debug.Log("No tower within spell range.");
        }
    }

    private IEnumerator BoostShootingSpeed()
    {
        canShoot = false;

        float originalShotDelay;

        originalShotDelay = archerTowerScript.ShootingDelay;
        archerTowerScript.ShootingDelay = archerTowerScript.ShootingDelay / boostEffect;
        Debug.Log(originalShotDelay + " " + archerTowerScript.ShootingDelay);
        

        yield return new WaitForSeconds(spellDuration);

        archerTowerScript.ShootingDelay = originalShotDelay;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spellRange);
    }
}
