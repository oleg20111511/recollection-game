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
		animator.SetBool("IsUsingMagic", true);
	}


	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(0.25f);
		canAttack = true;
	}


	public void DoDashDamage()
	{
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				GameObject enemy = collidersEnemies[i].gameObject;
				DamageEnemy(enemy);
			}
		}
	}


	public void LaunchProjectile()
	{
		// Instantiate projectile
		Vector3 projectileSpawnPoint = transform.position + new Vector3(transform.localScale.x * 0.5f,-0.2f);
		GameObject projectile = Instantiate(throwableObject, projectileSpawnPoint, Quaternion.identity) as GameObject; 
		ThrowableWeapon projectileController = projectile.GetComponent<ThrowableWeapon>();
		
		// Configure instance
		projectile.name = "ThrowableWeapon";

		Vector2 direction = new Vector2(transform.localScale.x, 0);
		projectileController.direction = direction; 
		projectileController.playerController = controller;

		projectileController.StartMovement();

		// Decrease player's mana
		controller.SetMana(controller.mana - manaCost);
	}


	public void DamageEnemy(GameObject enemy)
	{
		if (!controller.wrongKnockback)
		{
			enemy.GetComponent<Enemy>().GetHit(dmgValue, transform.position);
		}
		else
		{
			enemy.GetComponent<Enemy>().ApplyDamage(dmgValue);
			controller.Knockback(enemy.transform.position, 1500f);

			if (!controller.knockbackExplained)
			{
				controller.ExplainKnockback();
			}
		}
	}
}
