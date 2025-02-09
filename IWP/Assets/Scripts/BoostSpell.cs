using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpell : MonoBehaviour
{
    public LayerMask TowerLayer;
    private archerTower archerTowerScript;
    private bombTower bombTowerScript;

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
                bombTowerScript = collider.GetComponent<bombTower>();
                if (archerTowerScript != null)
                {
                    if (canShoot)
                        StartCoroutine(BoostShootingSpeed());
                }
                else if (bombTowerScript != null)
                {
                    if (canShoot)
                        StartCoroutine(BoostBombShootingSpeed());
                }
                else
                {
                    Debug.LogError("No TowerScript Withion Range");
                }
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

        archerTowerScript.ShootingDelay = archerTowerScript.OriginalDelay / boostEffect;
        

        yield return new WaitForSeconds(spellDuration);
        archerTowerScript.ShootingDelay = archerTowerScript.OriginalDelay;
        
        Destroy(gameObject);
    }

    private IEnumerator BoostBombShootingSpeed()
    {
        canShoot = false;

        bombTowerScript.ShootingDelay = bombTowerScript.OriginalDelay / boostEffect;


        yield return new WaitForSeconds(spellDuration);
        bombTowerScript.ShootingDelay = bombTowerScript.OriginalDelay;

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spellRange);
    }
}
