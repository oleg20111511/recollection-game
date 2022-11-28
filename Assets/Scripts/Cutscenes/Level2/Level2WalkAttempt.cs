using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Level2WalkAttempt : CutsceneController
{
    public PlayerInput player;

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
        yield return new WaitForSeconds(1f);
        Play();
    }


    public override void Begin()
    {
        director.Play();
    }
}
