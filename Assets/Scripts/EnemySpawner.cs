using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    /// <summary>
    /// 1. Looks at the player position and makes a circle around the player which is not spawnable
    /// 2. Picks random locations inside the remaining area of the arena and spawns enemies at random
    /// 3. Contains a public list for the enemies that allows for checking when the player has cleared the room. 
    /// </summary>

    public EnemyRoom enemies;

    private Vector2 ArenaCenter;
    private float arenaRadius;

    private float playerSafeRadius;

    private Vector2 playerPos;


    void CheckSpawnableLocation(Vector2 loc)
    { 

        // if (loc )

    }



    // Start is called before the first frame update
    void Start()
    {
        // Get the radius
        GameObject Arena = GameObject.FindGameObjectWithTag("Arena");
        arenaRadius = Arena.transform.lossyScale.x * Arena.GetComponent<CircleCollider2D>().radius;


        // Get the positions
        ArenaCenter = Arena.transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;


        // Do Spawning for loop
        // for (enemyType)
            // for (enemyCount)
    }
}
