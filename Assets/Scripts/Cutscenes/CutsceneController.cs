using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class CutsceneController : MonoBehaviour
{
    public bool isPlaying {get; protected set;} = false;

    public void Play()
    {
        isPlaying = true;
        Begin();
    }


    public abstract void Begin();

    public IEnumerator DrawTextLine(TextMeshProUGUI textHolder, string text)
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
