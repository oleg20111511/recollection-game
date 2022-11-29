using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Coin : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D col;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        CoinProgress coinController = GameController.instance.coinController;
        GameController.instance.coinController.PickUpCoin();

        if (!coinController.anomalyEnabled)
        {
            gameObject.SetActive(false);
        }
        else
        {
            animator.SetBool("Placed", true);
            col.enabled = false;
        }
        
    }
}
