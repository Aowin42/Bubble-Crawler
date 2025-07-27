using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


/// <summary>
/// Small definition of the Stat Class as well as the functions loaded onto the StatManager.
/// PlayerStats can be seen as the global reference for statupdates made during play.
/// Should be a global data container for reference functions called that modifies the player stats
/// and other aspects, but not the current state
/// </summary>


[System.Serializable]
public class Stat
{
    public StatType type;
    public float value;

    public float valueCopy; // Each stat also has a copy of what its value was before the level up.
}

public class PlayerStats : MonoBehaviour
{

    public List<Stat> stats;
    public GameObject mainWeapon;       // Container for the players main weapon

    [SerializeField]
    public int level, exp = 0, expToLevel = 50, statPointsOnLvlUp = 3, statPntRemaining = 0;




    public int statTotalReceived = 0;



    public void MakeStatCopy()
    { 
        for (int i = 0; i < stats.Count; i++)
        {
            stats[i].valueCopy = stats[i].value;
        }
    }


    public void ModifyStat(StatType type, float delta)
    /// <Summary>
    /// This function is working with the buttons in the UI 
    /// in order to create a way that we remember how many points we started with and for now can only 
    /// increase stats until none are left and not decrease further down than zero. 
    /// This is checked in the specific button Script
    /// <Summary>
    {
        var stat = stats.Find(s => s.type == type);
        if (stat != null)
        {
            stat.value += delta;
        }
    }

    public float GetStatValue(StatType type)
    {
        return stats.Find(s => s.type == type).value;
    }
    
    public float GetStatCopy(StatType type)
    {
        return stats.Find(s => s.type == type).valueCopy;
    }


    public void OnDeath()
    {
        Destroy(gameObject);
    }


    public void DoLevelUp()
    {
        MakeStatCopy();                         // Copies the stats to tell the buttons how far down they go
        statPntRemaining = statPointsOnLvlUp;  // Setting the number of statpoints left
        statTotalReceived += statPointsOnLvlUp;
        exp -= expToLevel;
        level++;
        expToLevel += (expToLevel * 0.25).ConvertTo<int>();     // Increasing the exp counter by 25% and rounding down

    }
}
