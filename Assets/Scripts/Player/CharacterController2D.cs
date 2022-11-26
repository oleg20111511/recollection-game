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

	public bool invincible = false;
	public float life = 10f;

	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		movement = GetComponent<PlayerMovement>();
		attack = GetComponent<Attack>();
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


	IEnumerator Stun(float time) 
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
