using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject container;

    void Start()
    {
        GameController.instance.OnGameStateChanged += HideInCutscene;
    }

    void HideInCutscene(GameState state)
    {
        // Debug.Log("Container:" + container + "; Instance: " + GameController.instance + "; State: " + state + "; Scene: " + SceneManager.GetActiveScene().name);
        if (state == GameState.Cutscene) {
            container.SetActive(false);
        } else {
            container.SetActive(true);
        }
    }
}
