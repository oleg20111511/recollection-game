using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float dmgValue = 4;
	public int manaCost = 5;
	public GameObject throwableObject;
	public Transform attackCheck;

	private Rigidbody2D rb2d;
	private Animator animator;
	private CharacterController2D controller;
	public bool canAttack = true;
	public bool isTimeToCheck = false;
	private PlayerInput input;


	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		input = GetComponent<PlayerInput>();
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController2D>();
	}

    // Update is called once per frame
    void Update()
    {
		if (input.meleeAttackInput && canAttack)
		{
			MeleeAttack();
		}

		if (input.rangeAttackInput && controller.mana >= manaCost)
		{
			RangeAttack();
		}
	}

	void  MeleeAttack()
	{
		canAttack = false;
		animator.SetBool("IsAttacking", true);
		StartCoroutine(AttackCooldown());
	}


	void RangeAttack()
	{
		GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f,-0.2f), Quaternion.identity) as GameObject; 
		Vector2 direction = new Vector2(transform.localScale.x, 0);
		throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction; 
		throwableWeapon.name = "ThrowableWeapon";
		throwableWeapon.SendMessage("StartMovement");

		controller.SetMana(controller.mana - manaCost);
	}


	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(0.25f);
		canAttack = true;
	}

	public void DoDashDamage()
	{
		dmgValue = Mathf.Abs(dmgValue);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
				{
					dmgValue = -dmgValue;
				}
				collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
			}
		}
	}
}
