using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ManaRestore : MonoBehaviour
{
    public int restorationAmount = 20;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CharacterController2D>().RestoreMana(restorationAmount);
            gameObject.SetActive(false);
        }
    }
}
