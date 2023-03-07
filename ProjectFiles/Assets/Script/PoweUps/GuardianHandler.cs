using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianHandler : MonoBehaviour
{
	public float rotationSpeed = 45f;
	public GameObject guardianWheel;

	public Transform[] spawnPoints;
	
	private int nextIndex = 0;

	void Start()
	{
		SpawnWheel();
	}

	public void AddInstance()
	{
		if (nextIndex >= spawnPoints.Length)
		{
			Debug.Log("No more available slots!");
			return;
		}

		SpawnWheel();
	}

	private void SpawnWheel()
	{
		GameObject temp = Instantiate(guardianWheel, spawnPoints[nextIndex].position, Quaternion.identity);
		temp.transform.SetParent(spawnPoints[nextIndex]);
		nextIndex++;
	}

	private void Update()
	{
		transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
	}

	public void IncreceSpeed()
	{
		rotationSpeed += 20;
	}
}
