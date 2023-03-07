using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiledHandler : MonoBehaviour
{
	[SerializeField] private float attackDelay = 1f;
	private float attackTimer = 0f;

	private int random;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<EnemyHandler>() != null)
			random = Random.Range(1 , 3);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<EnemyHandler>() != null)
		{
			attackTimer += Time.deltaTime;

			if (attackTimer >= attackDelay)
			{
				collision.gameObject.GetComponent<EnemyHandler>().ReduceHealth(random);
				attackTimer = 0f;
			}
		}
	}

	public void IncreaseScale()
	{
		transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
	}
}
