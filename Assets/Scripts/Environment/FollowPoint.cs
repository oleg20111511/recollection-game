using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public GameObject nextFollowPoint = null;
    public CutsceneController nextCutscene = null;

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            if (nextFollowPoint != null)
            {
                nextFollowPoint.SetActive(true);
            }
            else if (nextCutscene != null)
            {
                nextCutscene.Play();
            }
            
            gameObject.SetActive(false);
        }
    }
}
