using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level6ReadScroll : CutsceneController
{
    public Canvas dialogueBoxCanvas;
    public TextMeshProUGUI dialogueTextBox;
    public Image hpFill;
    public Image manaFill;
    private List<string> lines;

    private void Awake()
    {
        lines = new List<string>();
        lines.Add("The scroll of truths? \"HP bar is supposed to be red\"?");
        lines.Add("I don't understand what this is about. Maybe my head's getting worse...");
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
        GameController.instance.popupController.DisplayText("Anomaly reverted: HP and Mana bars will be displayed in their original colors");

        // Swap colors
        Color manaColor = hpFill.color;
        hpFill.color = manaFill.color;
        manaFill.color = manaColor;
    }
}
