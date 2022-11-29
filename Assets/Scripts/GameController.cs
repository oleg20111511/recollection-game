using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance {get; private set;}
    public event System.Action<GameState> OnGameStateChanged;
    public GameState state {get; private set;}
    public PopupController popupController;

    void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void SetState(GameState newState) {
        state = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}


public enum GameState {
    Gameplay,
    Cutscene
}
