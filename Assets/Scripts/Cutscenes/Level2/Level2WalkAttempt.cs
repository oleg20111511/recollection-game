using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Level2WalkAttempt : CutsceneController
{
    public PlayerInput player;
    public PlayerMovement playerMovement;
    public GameObject firstFollowPoint;

    private PlayableDirector director;
    private bool listenForPlayerInput = false;  // false before setup, then true until input, then false after fall animation start

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }


    public void Setup()
    {
        listenForPlayerInput = true;
    } 


    private void Update()
    {
        // Trigger for cutscene start is player attempting to move
        // The intro cutscene must play before trigger becomes active
        if (listenForPlayerInput && player.xMovement != 0) {
            listenForPlayerInput = false;
            StartCoroutine(DelayedPlay());
        }
    }


    IEnumerator DelayedPlay()
    {
        yield return new WaitForSeconds(0.5f);
        Play();
    }


    public override void Begin()
    {
        playerMovement.runSpeed /= 4;
        director.Play();
    }


    public void OnFallDown()
    {
        GameController.instance.SetState(GameState.Cutscene);
    }


    public void OnDistortion()
    {
        player.alterControls();
    }


    public void End()
    {
        isPlaying = false;
        playerMovement.runSpeed *= 4;
        GameController.instance.SetState(GameState.Gameplay);
        GameController.instance.popupController.DisplayText("Looks like you forgot how some things should be in this world. Take a walk around and tell your ally about your experience.");
        firstFollowPoint.SetActive(true);
        Terminate();
    }
}
