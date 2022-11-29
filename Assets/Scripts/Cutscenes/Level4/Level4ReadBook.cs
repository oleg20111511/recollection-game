using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level4ReadBook : CutsceneController
{
    public Canvas dialogueBoxCanvas;
    public TextMeshProUGUI dialogueTextBox;
    private List<string> lines;

    private void Awake()
    {
        lines = new List<string>();
        lines.Add("A book about physics? Something about force...");
        lines.Add("Stuff written here doesn't add up with what I've seen before. Might be another fault in my cognition.");
        lines.Add("I better remember this info");
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
            StartCoroutine(DrawTextLine(dialogueTextBox, lines[0]));
            lines.RemoveAt(0);
        }
        else
        {
            End();
        }
    }

    private void End()
    {
        dialogueBoxCanvas.enabled = false;
        isPlaying = false;
        GameController.instance.SetState(GameState.Gameplay);
        GameController.instance.popupController.DisplayText("You have restored your understanding of knockback. Now your hits will land without any anomalies");
    }
}
