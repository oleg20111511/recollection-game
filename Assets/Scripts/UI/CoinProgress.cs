using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinProgress : MonoBehaviour
{
    public TextMeshProUGUI textContainer;
    public bool anomalyEnabled = false;

    private int coinsAmount = 0;

    public int coinsCollected {get; private set;} = 0;

    public bool anomalyExplained = false;

    // Start is called before the first frame update
    void Start()
    {
        coinsAmount = GameObject.FindGameObjectsWithTag("Coin").Length;
        if (anomalyEnabled)
        {
            coinsCollected = coinsAmount;
        }
        SetCoinAmount(coinsCollected);
    }


    public void PickUpCoin()
    {
        if (!anomalyEnabled)
        {
            coinsCollected++;
        }
        else
        {
            coinsCollected--;
            if (!anomalyExplained)
            {
                anomalyExplained = true;
                GameController.instance.popupController.DisplayText("Anomaly: you place coins on level instead of collecting them");
            }
        }
        
        SetCoinAmount(coinsCollected);
    }


    public void SetCoinAmount(int newAmount)
    {
        string newText = string.Format("{0}/{1}", newAmount, coinsAmount);
        textContainer.text = newText;
    }
}
