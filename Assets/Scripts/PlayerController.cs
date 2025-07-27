using System;
using UnityEngine;
using static Unity.Mathematics.math;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Script for player collisions, movement and weapons
    /// </summary>


    private Rigidbody2D rb;

    private Camera MainCam;
    private Vector3 mousePos;

    [SerializeField]
    private Transform ProjectileSpawnPoint;

    private float shotCooldown;     // Cooldown counter 
    private PlayerStats stats;
    private UIManager UI;
    private GameStateManager gameState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<PlayerStats>();
        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        gameState = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateManager>();
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        shotCooldown = 1f / stats.GetStatValue(StatType.fireRate);
    }


    void PlayerForces()
    { 
        // Movement
        Vector2 movementForce = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementForce += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementForce += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementForce += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementForce += Vector2.right;
        }

        // The actual force of player movement
        movementForce = movementForce.normalized * stats.GetStatValue(StatType.moveSpeed);

        // Friction forces        F_fric = -kv
        Vector2 frictionForce = -stats.GetStatValue(StatType.friction) * rb.velocity.magnitude * rb.velocity.normalized;

        // Updating Velocity F_sum = F_res
        rb.velocity += new Vector2(movementForce.x + frictionForce.x,
                                   movementForce.y + frictionForce.y) * Time.deltaTime;

        // Caps the velocity by setting the velocity to max, and adjusting the sign ()
        if (abs(rb.velocity.x) > stats.GetStatValue(StatType.maxSpeed))
        {
            rb.velocity = new Vector2(stats.GetStatValue(StatType.maxSpeed) * (1 - 2 * Convert.ToInt16(rb.velocity.x < 0)), rb.velocity.y);
        }
        if (abs(rb.velocity.y) > stats.GetStatValue(StatType.maxSpeed))
        {
            rb.velocity = new Vector2(rb.velocity.x, stats.GetStatValue(StatType.maxSpeed) * (1 - 2 * Convert.ToInt16(rb.velocity.y < 0)));
        }
    }

    void WeaponControls(ref float shotCooldown) // adding a reference to modify the value directly
    {
        shotCooldown += Time.deltaTime;
        if ((shotCooldown > 1f / stats.GetStatValue(StatType.fireRate)) && Input.GetMouseButton(0))
        {


            // Spawn a projectile object, at the current location. The 
            GameObject obj = Instantiate(stats.mainWeapon, new Vector3(ProjectileSpawnPoint.position.x,
                                            ProjectileSpawnPoint.position.y,
                                            ProjectileSpawnPoint.position.z),
                        Quaternion.identity);
            shotCooldown = 0;   // reset cooldown

            ProjectilePlayer script = obj.GetComponent<ProjectilePlayer>();
            if (script != null)
            {
                script.SetDamage(stats.GetStatValue(StatType.damage));   // sets the bullet damage to enemy DMG
                script.SetSpeed(stats.GetStatValue(StatType.bulletSpeed));
            }
        }
    }





    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            var bulletDMG = -collision.gameObject.GetComponent<ProjectileEnemy>().damage;
            stats.ModifyStat(StatType.hitPoints, bulletDMG);

            Debug.Log("Damage TAKEN:" + bulletDMG);

            Debug.Log("CURRENT HP" + stats.GetStatValue(StatType.hitPoints));

            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        mousePos = MainCam.ScreenToWorldPoint(Input.mousePosition);      // Converts from pixel space to world space
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        WeaponControls(ref shotCooldown);   // Much like the float &shotcooldown form c++

        PlayerForces();     // Adding forces to player



        // Initiates Player Death
        if (stats.GetStatValue(StatType.hitPoints) <= 0)
        {
            stats.OnDeath();
        }

        // Initiates Player level up
        if (stats.exp >= stats.expToLevel)
        {
            stats.DoLevelUp();
            UI.EnableLevelUpScreen();
            gameState.ChangeGameState(GameState.MenuOpen);
        }
    }
}
