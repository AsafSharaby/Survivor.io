using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private float spwanRate;
	[SerializeField] private GameObject[] enemyPrefabs;
	[SerializeField] private bool canSpawn = true;
	void Start()
	{
		StartCoroutine(Spawner());
	}

    private IEnumerator Spawner()
	{
		WaitForSeconds wait = new WaitForSeconds(spwanRate);

		while (canSpawn)
		{
			yield return wait;
			int rand = Random.Range(0, enemyPrefabs.Length);

			SpawnObject(rand);
		}
	}

	public void SpawnObject(int rand)
	{
		int randomChildIndex = Random.Range(0, transform.childCount);
		Instantiate(enemyPrefabs[rand], transform.GetChild(randomChildIndex).position, transform.rotation);
	}
}
