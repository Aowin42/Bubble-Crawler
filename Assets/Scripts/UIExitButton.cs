using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExitButton : MonoBehaviour
{
    /// <summary>
    /// The activation for exiting the levelup UI
    /// </summary>

    private PlayerStats stats;

    [SerializeField]
    private GameObject Menu;

    [SerializeField]
    private Button menuExitButton;

    private GameStateManager GSM;


    void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("StatManager").GetComponent<PlayerStats>();
        menuExitButton.interactable = false;
        GSM = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateManager>();
    }

    void Update()
    {
        if (stats.statPntRemaining == 0)
        {
            menuExitButton.interactable = true;
        }
    }

    public void ExitMenu()
    {
        //Potentially save variables during this or something else
        Menu.SetActive(false);
        GSM.ChangeGameState(GameState.GameRunning);
    }




}