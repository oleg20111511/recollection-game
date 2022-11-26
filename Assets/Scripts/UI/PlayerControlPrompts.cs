using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlPrompts : MonoBehaviour
{
    public GameObject controlPromptsContainer;

    void Awake()
    {
        GameController.OnGameStateChanged += HideInCutscene;
    }

    void HideInCutscene(GameState state)
    {
        if (state == GameState.Cutscene) {
            controlPromptsContainer.SetActive(false);
        } else {
            controlPromptsContainer.SetActive(true);
        }
    }
}
