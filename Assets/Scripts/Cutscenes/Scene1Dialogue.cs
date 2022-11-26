using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scene1Dialogue : CutsceneController
{
    public Canvas dialogueBoxCanvas;
    public TextMeshProUGUI allyDialogueBoxText;
    private List<string> lines;

    private void Awake()
    {
        lines = new List<string>();
        lines.Add("It's finally time. The treasure is almost ours.");
        lines.Add("Next action is up to you.");
        lines.Add("Neutralize the remaining guards up ahead and get that chest");
    }


    public override void Begin()
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
