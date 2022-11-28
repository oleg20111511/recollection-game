using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelEnd : MonoBehaviour
{
    public string nextLevelName;
    public bool active = true;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && active)
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
