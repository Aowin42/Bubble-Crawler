using UnityEngine;

public enum GameState
    {
        Loading,
        MenuOpen,
        PlayerDead,
        GameRunning,
    }


public class GameStateManager : MonoBehaviour
{

    public GameState currentState;

    void Start()
    {
        currentState = GameState.GameRunning;
        ChangeGameState(currentState);
    }



    public void ChangeGameState(GameState nextState)
    {
        currentState = nextState;

        if (currentState == GameState.MenuOpen)
        {
            Time.timeScale = 0f;        // Pauses the logic of the game momentarily
        }

        else if (currentState == GameState.PlayerDead)
        {
            Time.timeScale = 1f;        // Returning Playability to Player
            Debug.Log("From GameStateController: 'Player Died' ");
            Debug.LogError("WARNING! Unused part of GameStateController");
        }

        else if (currentState == GameState.GameRunning)
        {
            Time.timeScale = 1f;        // Returning Playability to Player
        }

        else
        {
            Debug.LogError("WARNING: Current GameState is not set in GameStateController! Consider making other changes first.");
        }

    }

}