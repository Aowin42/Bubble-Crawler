using UnityEngine;

public class ArenaController : MonoBehaviour
{   
    /// <summary>
    /// Trying to code a collider such that the player can't exit.
    /// This requires that the arena is in (0,0) in world space
    /// </summary>

    private GameObject player;

    private float edgeRadius;
    private float arenaScale;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        edgeRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        arenaScale = transform.lossyScale.x;
        Debug.Log(edgeRadius * arenaScale);
    }

    void Update()
    {
        Vector2 playerPos = player.transform.position;
        if (playerPos.magnitude > arenaScale * edgeRadius)
            player.transform.position = playerPos.normalized * arenaScale * edgeRadius;
    }




}
