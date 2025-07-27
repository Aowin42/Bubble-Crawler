using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExpBubble : MonoBehaviour
{
    /// <summary>
    /// Script for the EXP drops. Simply a number for each gameobject
    /// </summary>
    [SerializeField]
    public int exp;
    private PlayerStats stats;

    private EXPbarUpdate expBarUpdater;

    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<PlayerStats>();
        expBarUpdater = GameObject.FindGameObjectWithTag("UIManager").GetComponent<EXPbarUpdate>();
    }



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            // Adding Exp to Player stats and removing exp Orb.
            expBarUpdater.DisplayExpBar();
            stats.exp += exp;
            Destroy(gameObject);
        }   

    }
}
