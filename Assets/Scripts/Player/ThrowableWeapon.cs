using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : MonoBehaviour
{
	public CharacterController2D playerController;
	public Vector2 direction;
	public bool hasHit = false;
	public float speed = 10f;
	public float dmgValue = 2f;

    public void StartMovement()
    {
		GetComponent<Rigidbody2D>().velocity = direction * speed;
		if (direction.x < 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			GameObject enemy = collision.gameObject;

			if (!playerController.wrongKnockback)
			{
				enemy.GetComponent<Enemy>().GetHit(dmgValue, transform.position);
			}
			else
			{
				enemy.GetComponent<Enemy>().ApplyDamage(dmgValue);
				playerController.Knockback(enemy.transform.position, 1500f);

				if (!playerController.knockbackExplained)
				{
					playerController.ExplainKnockback();
				}
			}

			Destroy(gameObject);
		}
		else if (!collision.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
