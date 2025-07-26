using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using static Unity.Mathematics.math;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// Main Controller for a patrolling enemy with weapon.
    /// Eventually this could be split up into more scripts for more controlled availability
    /// </summary>



    [SerializeField]
    private float HP, fireRate, bulletSpeed, moveSpeed, maxSpeed, friction,
                  rotationSpeed, wanderRadius, alertRadius, oneDirectionTravelTime;
    [SerializeField]
    private int damage, expValue;

    [SerializeField]
    private GameObject projectile, expBubble;

    [SerializeField]
    private Transform ProjectileSpawnPoint;

    private GameObject player;
    private Vector3 playerPos;
    private Vector2 arenaCenter;

    private Vector2 direction;
    private Vector3 patrolTarget;
    private Rigidbody2D rb;


    private float shotCooldown = 0;

    private float sameDirectionMoveTime = 0f;



    enum State
    {
        Patrolling,
        Chasing,
    }
    [SerializeField]
    State currentState = State.Patrolling;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        arenaCenter = Vector3.zero;
        CalculateMoveDir();
        rb = GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Trigger between enemy and bullet gets the weapon data from the bullet,
        // subtracts the HP and removes the bullet 
        if (collision.CompareTag("PlayerBullet"))
        {
            ProjectilePlayer projectile = collision.gameObject.GetComponent<ProjectilePlayer>();
            HP -= projectile.damage;
            Destroy(projectile.gameObject);
        }
    }


    void UseWeapon(ref float shotCooldown) // adding a reference to modify the value directly
    /// If the cooldown is above the time between shots, we spawn a projectile, sets its damage and resets the counter
    {
        shotCooldown += Time.deltaTime;
        if ((shotCooldown > 1f / fireRate))
        {
            GameObject obj = Instantiate(projectile, new Vector3(ProjectileSpawnPoint.position.x,
                                            ProjectileSpawnPoint.position.y,
                                            ProjectileSpawnPoint.position.z),
                                            Quaternion.identity);

            shotCooldown = 0;   // reset cooldown
            ProjectileEnemy script = obj.GetComponent<ProjectileEnemy>();
            if (script != null)
            {
                script.SetDamage(damage);   // sets the bullet damage to enemy DMG
                script.SetSpeed(bulletSpeed);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))     { currentState = State.Chasing; } 
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))     { currentState = State.Patrolling; }
    }


    void CalculateMoveDir()
    {
        patrolTarget = arenaCenter + UnityEngine.Random.insideUnitCircle * wanderRadius;
        direction = (patrolTarget - transform.position).normalized;
        sameDirectionMoveTime = 0f;
    }



    // Update is called once per frame
    void Update()
    {
        // Spawns Exp and Kills the enemy
        if (HP <= 0)
        {
            for (int i = 0; i < expValue; i++)
            {
                Vector3 offset = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0);
                Instantiate(expBubble, transform.position + offset, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        // Triggers change from patrol to shoot
            if ((playerPos - transform.position).magnitude < alertRadius)
            {
                currentState = State.Chasing;
            }
            else
            {
                currentState = State.Patrolling;
            }


        // Switch case for what happens when a state is active
        switch (currentState)
        {
            case State.Patrolling:
                sameDirectionMoveTime += Time.deltaTime;

                // If Close calculate new patrol target
                if (sameDirectionMoveTime > oneDirectionTravelTime)
                {
                    CalculateMoveDir();
                }

                // Rotate enemy to movement direction
                float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotZ);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                break;

            case State.Chasing:

                playerPos = player.transform.position;
                // Keeps rotation towards player and uses the weapon
                direction = (playerPos - transform.position).normalized;
                Vector3 rotation = playerPos - transform.position;
                rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                targetRotation = Quaternion.Euler(0f, 0f, rotZ);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                UseWeapon(ref shotCooldown);
                break;
        }

        // Moving
        Vector2 moveForce = direction * moveSpeed;
        Vector2 frictionForce = -friction * rb.velocity.magnitude * rb.velocity.normalized;
        rb.velocity += (moveForce + frictionForce) * Time.deltaTime;


        // Caps the velocity by setting the velocity to max
        if (abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed * (1 - 2 * Convert.ToInt16(rb.velocity.x < 0)), rb.velocity.y);
        }
        if (abs(rb.velocity.y) > maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxSpeed * (1 - 2 * Convert.ToInt16(rb.velocity.y < 0)));
        }


    }
}
