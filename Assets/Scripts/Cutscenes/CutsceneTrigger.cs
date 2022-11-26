using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CutsceneTrigger : MonoBehaviour
{
    public CutsceneController cutscene;

    void OnTriggerEnter2D (Collider2D col) {
        if (col.gameObject.CompareTag("Player"))
        {
            GameController.instance.SetState(GameState.Cutscene);
            cutscene.Play();
        }
    }
}
