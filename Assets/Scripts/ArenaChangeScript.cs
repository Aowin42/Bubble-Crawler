using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaChangeScript : MonoBehaviour
{
    /// <summary>
    /// Unfinished Script. Was intended to control how different subarenas are loaded and generated. 
    /// </summary>


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLAYER TRANSITIONING TO NEW ARENA");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLAYER ARRIVED AT NEW ARENA");
        }
    }
}
