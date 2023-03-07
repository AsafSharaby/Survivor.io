using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public PowerUpType powerUpType;
}

public enum PowerUpType
{
	Guardian,
	Shiled,
	Kunai
}
