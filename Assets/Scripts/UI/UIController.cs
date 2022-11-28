using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject container;

    void Awake()
    {
        GameController.OnGameStateChanged += HideInCutscene;
    }

    void HideInCutscene(GameState state)
    {
        if (state == GameState.Cutscene) {
            container.SetActive(false);
        } else {
            container.SetActive(true);
        }
    }
}
