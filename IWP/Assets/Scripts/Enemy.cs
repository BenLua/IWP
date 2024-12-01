using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50;

    public float speed = 5f;

    private Transform target;
    private int waypointIndex = 0;
    private Vector3 direction;

    public Material OriginalMaterial;
    public Material Red;

    private void Start()
    {
        target = Waypoints.waypoints[0];
    }

    private void Update()
    {
        direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            Destroy(gameObject);
            DealDamageToPlayer();
            return;
        }

        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    void DealDamageToPlayer()
    {
        PlayerStats.Lives--;
    }

    public void TakeDamage(int damage)
    {
        // Get the MeshRenderer and store the original material if it's not set
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if (OriginalMaterial == null)
        {
            OriginalMaterial = renderer.material;
        }

        // Decrease health
        health -= damage;

        // Flash the enemy red
        StartCoroutine(FlashRed(renderer));

        // If health is less than or equal to 0, destroy the enemy
        if (health <= 0)
        {
            PlayerStats.Money += 50;
            Destroy(gameObject);
        }
    }

    // Coroutine to handle the flashing effect
    private IEnumerator FlashRed(MeshRenderer renderer)
    {
        // Set the material to red
        renderer.material = Red;

        // Wait for 0.1 seconds (you can adjust this time to make it faster/slower)
        yield return new WaitForSeconds(0.1f);

        // Reset the material back to the original one
        renderer.material = OriginalMaterial;
    }
}
