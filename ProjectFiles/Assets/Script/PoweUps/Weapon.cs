using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform shotPoint;

	private float timeBtwShots;
	[SerializeField] private float spwanRate;
	private GameObject curprojectile;
	private int projectileSpeed = 20;

	private void Update()
	{
		if (timeBtwShots <= 0)
			ShootProjectile();
		else
			timeBtwShots -= Time.deltaTime;
	}

	void ShootProjectile()
	{
		GameObject projectile = Instantiate(projectilePrefab);
		curprojectile = projectile;
		projectile.transform.position = transform.position;
		curprojectile.GetComponent<Projectile>().speed = projectileSpeed;
		projectile.GetComponent<Projectile>().DirectionCheker(GetComponentInParent<CharacterMove>().lastMove);
		timeBtwShots = spwanRate;
	}

	public void IncreaseProjectileSpeed()
	{
		projectileSpeed += 5;
	}
}
