using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

	public static WeaponHandler instance;

	public Transform weaponHolder;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}
}
