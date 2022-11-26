using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scene1Dialogue : CutsceneController
{
    public Canvas dialogueBoxCanvas;
    public TextMeshProUGUI textHolder;

    private List<string> lines;

    private void Awake()
    {
        lines = new List<string>();
        lines.Add("It's finally time. The treasure is almost ours.");
        lines.Add("Next action is up to you.");
        lines.Add("Neutralize the remaining guards up ahead and get that chest");
    }


    public override void Play()
    {
        dialogueBoxCanvas.enabled = true;
        StartCoroutine(DrawText(lines[0]));
    }

    IEnumerator DrawText(string text)
    {
        string displayedText = "";
        textHolder.text = displayedText;
        for (int i = 0; i < text.Length; i++) {
            displayedText += text[i];
            textHolder.text = displayedText;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
