using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private Vector3 direction;
	public float speed = 10;

	void Start()
	{
		Destroy(gameObject, 5);
	}

	void Update()
	{
		transform.position += direction * speed * Time.deltaTime;
	}

	public void DirectionCheker(Vector3 dir)
	{
		direction = dir;
		float dirX = direction.x;
		float dirY = direction.y;

		Vector3 scale = transform.localScale;
		Vector3 rotation = transform.rotation.eulerAngles;

		if (dirX < 0 && dirY == 0)
		{
			scale.x = scale.x * -1;
			scale.y = scale.y * -1;
		}
		else if (dirX == 0 && dirY < 0)
			scale.x = scale.x * -1;
		else if (dirX == 0 && dirY > 0)
			scale.y = scale.y * -1;
		else if (dir.x > 0 && dir.y > 0)
			rotation.z = -90;
		else if (dir.x > 0 && dir.y < 0)
			rotation.z = -180;
		else if (dir.x < 0 && dir.y > 0)
		{
			scale.x = scale.x * -1;
			scale.y = scale.y * -1;
			rotation.z = 180;
		}
		else if (dir.x < 0 && dir.y < 0)
		{
			scale.x = scale.x * -1;
			scale.y = scale.y * -1;
			rotation.z = -90;
		}

		transform.localScale = scale;
		transform.rotation = Quaternion.Euler(rotation);

	}



	private int random;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<EnemyHandler>() != null)
		{
			random = Random.Range(10, 35);
			collision.gameObject.GetComponent<EnemyHandler>().ReduceHealth(random);
		}
	}
}
