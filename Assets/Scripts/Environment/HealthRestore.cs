using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealthRestore : MonoBehaviour
{
    public float restorationAmount = 2.5f;
    public bool isFake = false;
    private static bool isExplained = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CharacterController2D>().RestoreLife(restorationAmount);
            
            if (isFake && !isExplained)
            {
                isExplained = true;
                GameController.instance.popupController.DelayedText("Anomaly: hearts cause damage and spikes restore HP", 1f);
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
