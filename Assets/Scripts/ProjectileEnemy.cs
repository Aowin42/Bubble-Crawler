using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    /// <summary>
    /// Enemy bullet and logic for spawning projectiles.
    /// For creating different bullets we could look into data containers 
    /// (i.e. SO's (Scriptable Objects) for different bullet types and so on)
    /// </summary>


    private Rigidbody2D rb;
    private Vector3 playerPos;
    public float bulletSpeed;
    public float damage;



    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = playerPos - transform.position;  // Creates the direction from where the bullet is instantiated
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
    }


    public void SetDamage(float dmg)
    /// Sets the Damage of the bullet. Used when instantiating bullets
    {
        damage = dmg;
    }

    public void SetSpeed(float speed)
    {
        bulletSpeed = speed;
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ArenaEdge"))
        {
            Destroy(gameObject);
        }
    }
}
