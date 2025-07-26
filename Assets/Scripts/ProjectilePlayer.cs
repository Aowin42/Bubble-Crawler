using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    /// <summary>
    /// Player bullet and logic for spawning projectiles.
    /// For creating different bullets we could look into data containers 
    /// (i.e. SO's (Scriptable Objects) for different bullet types and so on)
    /// </summary>

    private Vector3 mousePos;
    private Camera MainCam;
    private Rigidbody2D rb;

    private float bulletSpeed;

    public float damage;


    public void SetDamage(float dmg)
    /// Sets the Damage of the bullet. Used when instantiating bullets
    {
        damage = dmg;
    }

    public void SetSpeed(float speed)
    { 
        bulletSpeed = speed;
    }


    // Start is called before the first frame update
    void Start()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = MainCam.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = mousePos - transform.position;  // Creates the direction from where the bullet is instantiated
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ArenaEdge"))
        {
            Destroy(gameObject);
        }
    }
}
