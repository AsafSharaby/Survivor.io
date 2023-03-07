using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
	private Rigidbody2D rb;
	private Transform target;
	private EnemyDamagePopUp damagePopUp;


	[SerializeField] private float speed;
	[SerializeField] private float attackDelay = 1f;
	[SerializeField] private int damage = 10; 

	[SerializeField] private GameObject coinPrefab;
	[SerializeField] private Slider healthBarSlider;

	private float maxHealth = 100f;
	private float currentHealth;
	private float attackTimer = 0f;
	private int random;


	void Start()
    {
		target = GameObject.FindObjectOfType<CharacterMove>().transform;
		rb = GetComponent<Rigidbody2D>();
		damagePopUp = GetComponent<EnemyDamagePopUp>();

		currentHealth = maxHealth;
		UpdateHealthBar();
	}


	private void Update()
	{
		MoveEmeny();
	}

	private void MoveEmeny()
	{
		Vector2 directionToPlayer = (target.transform.position - transform.position).normalized;
		rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * speed;

		Fliper(directionToPlayer);
	}

	private void Fliper(Vector2 directionToPlayer)
	{
		directionToPlayer = (target.transform.position - transform.position).normalized;
		float enemyFacingDirection = Mathf.Sign(transform.localScale.x);
		float directionToPlayerX = Mathf.Sign(directionToPlayer.x);

		if (enemyFacingDirection != directionToPlayerX)
		{
			Vector3 currentScale = transform.localScale;
			currentScale.x *= -1;
			transform.localScale = currentScale;
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<CharacterMove>())
		{
			//target = null;
			
			attackTimer += Time.deltaTime;

			if (attackTimer >= attackDelay)
			{
				collision.gameObject.GetComponent<CharacterHealth>().TakeDamage(damage);
				attackTimer = 0f;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("GuardianWheel"))
		{
			random = Random.Range(10, 30);
			ReduceHealth(random);

		}
	}

	public void ReduceHealth(int damage)
	{
		currentHealth -= damage;
		UpdateHealthBar();

		if (currentHealth <= 0)
			Die();
		else
			ShowDamagePopUp(damage);
	}

	private void ShowDamagePopUp(int damage)
	{
		damagePopUp.ShowDamage(damage);
	}

	private void UpdateHealthBar()
	{
		healthBarSlider.value = currentHealth / maxHealth;
	}

	private void Die()
	{
		Instantiate(coinPrefab, transform.position, Quaternion.identity);
		GameHandler.instance.KillCounter();
		Destroy(gameObject);
	}
}
