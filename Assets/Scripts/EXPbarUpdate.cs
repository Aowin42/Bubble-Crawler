using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPbarUpdate : MonoBehaviour
{
    /// <summary>
    /// Script for updating the EXP bar gameobject in the UI
    /// </summary>

    private PlayerStats stats;
    private float barRatio;

    private float displayTime = 3;

    public GameObject expBar;

    public Image Mask;


    private Coroutine expBarRoutine;


    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<PlayerStats>();
    }


    public void DisplayExpBar()
    { 
        // We first check if we have already started a co-routine. If we have we refresh it by starting a new one.
        if (expBarRoutine != null)
        {
            StopCoroutine(expBarRoutine);
        }

        expBarRoutine = StartCoroutine(ShowExpBar());

    }


    public IEnumerator ShowExpBar()     // Example Coroutine (Basically imagine multithreading with non-blocking signals)
    {
        // Step 1: Do something
        expBar.SetActive(true);

        // Step 2: Wait (non-blocking)
        yield return new WaitForSeconds(displayTime);

        // Step 3: Do something after the wait
        expBar.SetActive(false);        
    }


    // Update is called once per frame
    void Update()
    {
        barRatio = (float)stats.exp / stats.expToLevel;
        Mask.fillAmount = barRatio;
    }
}
