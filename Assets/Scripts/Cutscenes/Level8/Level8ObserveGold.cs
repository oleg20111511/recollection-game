using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level8ObserveGold : CutsceneController
{
    public Canvas dialogueBoxCanvas;
    public TextMeshProUGUI dialogueTextBox;
    private List<string> lines;

    private void Awake()
    {
        lines = new List<string>();
        lines.Add("What a weirdly placed pile of gold...");
        lines.Add("That reminds me, I've started running low on cash very quickly");
        lines.Add("Maybe coins are not meant to be placed on the places I visit...");
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
        GameController.instance.popupController.DisplayText("Anomaly reverted: you will now collect coins instead of placing them");
    }
}
