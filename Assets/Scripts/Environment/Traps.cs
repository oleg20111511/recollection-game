using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public float damage = 2f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CharacterController2D>().ApplyDamage(damage, transform.position);
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
        }
    }
}
