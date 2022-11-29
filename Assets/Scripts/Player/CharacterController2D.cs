using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
	private Rigidbody2D rb2d;
	private Animator animator;
	private PlayerMovement movement;
	private Attack attack;
	public bool isFacingRight {get; private set;} = true;

	public HealthBar healthBar;
	public HealthBar manaBar;
	public bool invincible = false;
	public float maxLife = 10f;
	public int maxMana = 100;

	public float life {get; private set;}
	public int mana {get; private set;}

	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		movement = GetComponent<PlayerMovement>();
		attack = GetComponent<Attack>();
		SetLife(maxLife);
		SetMana(maxMana);
	}


	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		isFacingRight = !isFacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public void ApplyDamage(float damage, Vector3 position) 
	{
		if (invincible)
		{
			return;
		}

		animator.SetBool("Hit", true);

		life -= damage;
		SetLife(life - damage);
		
		Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
		rb2d.velocity = Vector2.zero;
		rb2d.AddForce(damageDir * 10);
		if (life <= 0)
		{
			StartCoroutine(WaitToDead());
		}
		else
		{
			StartCoroutine(Stun(0.25f));
			StartCoroutine(MakeInvincible(1f));
		}
	}


	public void RestoreLife(float amount)
	{
		float newLife = life + amount;
		if (newLife > maxLife)
		{
			newLife = maxLife;
		}
		SetLife(newLife);
	}


	public void SetLife(float newLife)
	{
		life = newLife;
		healthBar.SetHealth(newLife, maxLife);
	}


	public void RestoreMana(int amount)
	{
		int newMana = mana + amount;
		if (newMana > maxMana)
		{
			newMana = maxMana;
		}
		SetMana(newMana);
	}


	public void SetMana(int newMana)
	{
		mana = newMana;
		manaBar.SetHealth(newMana, maxMana);
	}


	public IEnumerator Stun(float time) 
	{
		movement.DisableMovement();
		yield return new WaitForSeconds(time);
		movement.EnableMovement();
	}


	IEnumerator MakeInvincible(float time) 
	{
		invincible = true;
		yield return new WaitForSeconds(time);
		invincible = false;
	}


	IEnumerator WaitToDead()
	{
		animator.SetBool("IsDead", true);
		movement.DisableMovement();
		invincible = true;
		attack.enabled = false;

		yield return new WaitForSeconds(0.4f);
		rb2d.velocity = new Vector2(0, rb2d.velocity.y);
	}
}
