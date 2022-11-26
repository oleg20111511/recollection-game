using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float xMovement {get; private set;}
    public bool jumpInput {get; private set;}
    public bool meleeAttackInput {get; private set;}
    public bool rangeAttackInput {get; private set;}

    void Update()
    {
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            xMovement = 1;
        } else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            xMovement = -1;
        } else
        {
            xMovement = 0;
        }

        jumpInput = Input.GetKeyDown(KeyCode.W);
        meleeAttackInput = Input.GetKeyDown(KeyCode.Space);
        rangeAttackInput = Input.GetKeyDown(KeyCode.F);
    }
}
