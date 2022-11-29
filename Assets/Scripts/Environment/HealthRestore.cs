using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealthRestore : MonoBehaviour
{
    public float restorationAmount = 2.5f;
    public bool isFake = false;
    private static bool isExplained = false;
    public bool disableExplanation = false;  // Editor doesn't support static assignment, so setting this to true on one instance is a way to set it true on all instances in start


    private void Start()
    {
        if (disableExplanation)
        {
            isExplained = true;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CharacterController2D>().RestoreLife(restorationAmount);
            
            if (isFake && !isExplained)
            {
                isExplained = true;
                GameController.instance.popupController.DelayedText("Anomaly: hearts cause damage and spikes restore HP", 0.1f);
            }

            gameObject.SetActive(false);
        }
    }

    IEnumerator DelayedExplanation()
    {
        yield return new WaitForSeconds(1f);
        GameController.instance.popupController.DisplayText("Anomaly: hearts cause damage and spikes restore HP");
    }
}
