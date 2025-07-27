using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatChangeButton : MonoBehaviour
{
    /// <summary>
    /// Decisions available to buttons for changing stats
    /// </summary>

    [SerializeField]
    private ButtonState buttonState;

    [SerializeField]
    private StatType type;

    private PlayerStats stats;

    private Button thisButton;

    private UIManager UI;


    enum ButtonState
    {
        Add,
        Subtract
    }


    void OnEnable()
    {
        stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<PlayerStats>();
        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        thisButton = gameObject.GetComponent<Button>();
        bool enabled = CheckButtonEnabled();

        if (enabled)
        {
            thisButton.interactable = true;
        }
    }


    [SerializeField]
    public float statChange = 1f;


    /// <summary>
    /// FIX THIS PIECE OF SHIT CODE THAT DOESN'T ACCOUNT FOR WHERE WE START THE CHANGING OF STATS FROM
    /// </summary>
    private bool CheckButtonEnabled()

    {
        if (stats.statPntRemaining == 0 && buttonState == ButtonState.Add)   // Disable if this is adding to stats 
                                                                             // and we have no more remaining
        {
            return false;
        }

        else if (stats.statPntRemaining == stats.statTotalReceived && buttonState == ButtonState.Subtract) // Disable if we have as many points 
                                                                                                           // as have been received in total
        {
            return false;
        }

        else if (stats.GetStatValue(type) == stats.GetStatCopy(type) && buttonState == ButtonState.Subtract)
        {
            return false;
        }

        // Checks if value is zero and disables subtract mode
        else if (stats.GetStatValue(type) == 0 && buttonState == ButtonState.Subtract)
        {
            return false;
        }

        return true;
    }



    public void ChangeStat()
    {

        if (buttonState == ButtonState.Add)
        {
            stats.ModifyStat(type, statChange);
            stats.statPntRemaining--;
        }

        else if (buttonState == ButtonState.Subtract)
        {
            stats.ModifyStat(type, -statChange);
            stats.statPntRemaining++;
        }
        UI.UpdateLevelUpScreen();

    }
    
    void Update()
    {
        // Checks for when the specific button should be enabled or disabled
        if (CheckButtonEnabled())
        {
            thisButton.interactable = true;
        }
        else
        { thisButton.interactable = false; }    
    }
}
