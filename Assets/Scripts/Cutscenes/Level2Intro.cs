using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level2Intro : CutsceneController
{
    public SpriteRenderer enemy;
    public Canvas dialogueBoxCanvas;
    public TextMeshProUGUI allyDialogueBoxText;
    private List<string> lines;

    private void Awake()
    {
        lines = new List<string>();
        lines.Add("Well, that was unfortunate.");
        lines.Add("The mission's a failure. Are you ok?");
        lines.Add("Can you walk?");  
    }


    public override void Begin()
    {
        enemy.sortingLayerName = "Default";
        GameController.instance.SetState(GameState.Cutscene);
    }
    

    public void EnemyOutOfCartReceiver()
    {
        enemy.sortingLayerName = "Units";
    }


    public void TimelineEnd()
    {
        dialogueBoxCanvas.enabled = true;
        DrawNextLine();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPlaying) {
            DrawNextLine();
        }
    }


    private void DrawNextLine()
    {
        if (lines.Count > 0)
        {
            StartCoroutine(DrawTextLine(allyDialogueBoxText, lines[0]));
            lines.RemoveAt(0);
        } else {
            End();
        }
    }


    private void End()
    {
        dialogueBoxCanvas.enabled = false;
        isPlaying = false;
        GameController.instance.SetState(GameState.Gameplay);
    }
}
