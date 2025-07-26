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

    public Image Mask;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        barRatio = (float)stats.exp / stats.expToLevel;
        Mask.fillAmount = barRatio;
    }
}
