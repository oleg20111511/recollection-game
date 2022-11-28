using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupController : MonoBehaviour
{
    public TextMeshProUGUI textContainer;
    public GameObject popupFrame;

    public void DisplayText(string newText)
    {
        popupFrame.SetActive(true);
        textContainer.text = newText;
        GameController.instance.SetState(GameState.Cutscene);
    }

    public void ClosePopup()
    {
        popupFrame.SetActive(false);
        GameController.instance.SetState(GameState.Gameplay);
    }

}
