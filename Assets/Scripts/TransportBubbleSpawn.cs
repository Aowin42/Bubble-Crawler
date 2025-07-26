using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportBubbleSpawn : MonoBehaviour
{
    /// <summary>
    /// Small script for activating the "BubbleTransporter" when exiting the main bubble of the arena
    /// Was intended as usable together with the TransitionController
    /// </summary>


    [SerializeField]
    private GameObject BubbleTransporter;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BubbleTransporter.SetActive(false);

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BubbleTransporter.SetActive(true);

        }
    }

}
