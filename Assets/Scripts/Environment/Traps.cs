using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public float damage = 2f;
    public List<Transform> recoveryZones;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CharacterController2D>().GetHit(damage, transform.position);
            StartCoroutine(col.gameObject.GetComponent<CharacterController2D>().Stun(0.5f));
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
        }
    }
}
