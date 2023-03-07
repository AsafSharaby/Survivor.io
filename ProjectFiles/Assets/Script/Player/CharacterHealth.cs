using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
	[Header("Health")]
	private int maxHealth = 100;
	private int currentHealth;
	[SerializeField] private Slider healthSlider;

	[Header("SpriteBlink")]
	[SerializeField] private float duration = 0.1f;
	[SerializeField] private Color blinkColor = Color.white;

	private SpriteRenderer spriteRenderer;
	private Coroutine flashRoutine;
	[SerializeField]  private EnemyHandler[] enemesArray;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		currentHealth = maxHealth;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = currentHealth;
	}

	public void TakeDamage(int damageAmount)
	{
		currentHealth -= damageAmount;
		healthSlider.value = currentHealth;
		SpriteFlash(blinkColor);

		if (currentHealth <= 0)
			Die();
	}

	private IEnumerator WhiteHitEffectCoroutine(Color color)
	{
		spriteRenderer.color = color;

		yield return new WaitForSeconds(duration);

		spriteRenderer.color = Color.white;
		flashRoutine = null;
	}

	public void SpriteFlash(Color color)
	{
		if (flashRoutine != null)
			StopCoroutine(flashRoutine);
		flashRoutine = StartCoroutine(WhiteHitEffectCoroutine(color));
	}

	void Die()
	{
		FindObjectOfType<EnemySpawner>().gameObject.SetActive(false);

		enemesArray = FindObjectsOfType<EnemyHandler>();
		for (int i = 0; i < enemesArray.Length; i++)
		{
			EnemyHandler item = enemesArray[i];
			Destroy(item.gameObject);
		}

		GameHandler.instance.GameOver();
	}
}
