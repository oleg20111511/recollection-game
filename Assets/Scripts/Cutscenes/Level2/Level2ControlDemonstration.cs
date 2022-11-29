using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class Level2ControlDemonstration : CutsceneController
{
    public Canvas dialogueBoxCanvas;
    public TextMeshProUGUI allyDialogueBoxText;
    public PlayerInput player;
    public LevelEnd endTrigger;

    private List<string> lineGroup1;
    private List<string> lineGroup2;
    private PlayableDirector director;
    private CutsceneState state = CutsceneState.NotStarted;

    private void Awake()
    {
        lineGroup1 = new List<string>();
        lineGroup1.Add("Uhhh, that hit to your head dind't go without consequences, that's for sure.");
        lineGroup1.Add("Did you forget how this universe works or something?");
        lineGroup1.Add("Here, I'll show you how to walk properly, watch closely");

        lineGroup2 = new List<string>();
        lineGroup2.Add("You should now know how to move, but you should take a look around. Maybe there's more to that injury of yours...");

        director = GetComponent<PlayableDirector>();
    }


    private void SetState(CutsceneState newState)
    {
        state = newState;
    }


    private void Update()
    {
        if (!isPlaying)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (state == CutsceneState.FirstDialogue)
            {
                DrawFirstGroupLine();
            }
            else if (state == CutsceneState.SecondDialogue)
            {
                DrawSecondGroupLine();
            }
        }
    }


    public override void Begin()
    {
        GameController.instance.SetState(GameState.Cutscene);
        dialogueBoxCanvas.enabled = true;
        SetState(CutsceneState.FirstDialogue);
        DrawFirstGroupLine();
    }

    
    private void DrawFirstGroupLine()
    {
        if (lineGroup1.Count > 0)
        {
            StartCoroutine(DrawTextLine(allyDialogueBoxText, lineGroup1[0]));
            lineGroup1.RemoveAt(0);
        }
        else
        {
            StartTimeline();
        }
    }

    private void StartTimeline()
    {
        dialogueBoxCanvas.enabled = false;
        SetState(CutsceneState.TimelineExecution);
        director.Play();
    }


    public void OnTimelineEnded()
    {
        dialogueBoxCanvas.enabled = true;
        SetState(CutsceneState.SecondDialogue);
        DrawSecondGroupLine();
    }


    private void DrawSecondGroupLine()
    {
        if (lineGroup2.Count > 0)
        {
            StartCoroutine(DrawTextLine(allyDialogueBoxText, lineGroup2[0]));
            lineGroup2.RemoveAt(0);
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
        
        player.restoreControls();

        GameController.instance.SetState(GameState.Gameplay);
        GameController.instance.popupController.DisplayText("Control over your body is back to the usual, familiar way, but there are still some things you did't restore yet...\nYou can now explore the area ahead.");

        endTrigger.active = true;

        Terminate();
    }
}



enum CutsceneState {
    NotStarted,
    FirstDialogue,
    TimelineExecution,
    SecondDialogue
}
