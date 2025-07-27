using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Currently just the controller for how the elements on the Level up screen is updated
    /// </summary>

    [Header("Level Up screen Information")]
    [SerializeField]
    private TMP_Text statCounterText;

    [SerializeField]
    private TMP_Text hpText, dmgText;

    [SerializeField]
    private GameObject LevelUpScreen; 

    private PlayerStats stats;

    void Start()
    {
        LevelUpScreen.SetActive(false);
        stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<PlayerStats>();
    }

    /// <summary>
    /// Updates the UI elements on the LVL up Screen 
    /// </summary>
    public void UpdateLevelUpScreen()
    {
        hpText.text = stats.GetStatValue(StatType.hitPoints).ToString();
        dmgText.text = stats.GetStatValue(StatType.damage).ToString();
        statCounterText.text = "Stat points remaining: " + stats.statPntRemaining.ToString();
    }


    public void EnableLevelUpScreen()
    {
        UpdateLevelUpScreen();
        LevelUpScreen.SetActive(true);
    }
    

}
