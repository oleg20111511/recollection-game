using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;


public class Level2Intro : CutsceneController
{
    public SpriteRenderer enemy;
    public Canvas dialogueBoxCanvas;
    public TextMeshProUGUI allyDialogueBoxText;
    public Level2WalkAttempt nextCutscene;

    private PlayableDirector director;
    private List<string> lines;
    private bool isInDialogue = false;

    private void Awake()
    {
        lines = new List<string>();
        lines.Add("Well, that was unfortunate.");
        lines.Add("The mission's a failure. Are you ok?");
        lines.Add("Can you walk?");  
    }


    private void Start()
    {
        Play();
    }


    public override void Begin()
    {
        enemy.sortingLayerName = "Default";
        GameController.instance.SetState(GameState.Cutscene);
        director = GetComponent<PlayableDirector>();
        director.Play();
    }
    

    public void EnemyOutOfCartReceiver()
    {
        enemy.sortingLayerName = "Units";
    }


    public void TimelineEnd()
    {
        dialogueBoxCanvas.enabled = true;
        isInDialogue = true;
        DrawNextLine();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInDialogue) {
            DrawNextLine();
        }
    }


    private void DrawNextLine()
    {
        if (lines.Count > 0)
        {
            StartCoroutine(DrawTextLine(allyDialogueBoxText, lines[0]));
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
        nextCutscene.Setup();
        GameController.instance.SetState(GameState.Gameplay);
        GameController.instance.popupController.DisplayText("Something seems unusual. Try walking around to check your condition.");
        Terminate();
    }
}
