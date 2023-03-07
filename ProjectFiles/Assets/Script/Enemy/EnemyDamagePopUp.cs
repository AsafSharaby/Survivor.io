using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamagePopUp : MonoBehaviour
{
	public Text damageText;

	private void Awake()
	{
		damageText.gameObject.SetActive(false);
	}

	public void ShowDamage(float damage)
	{
		damageText.gameObject.SetActive(true);
		damageText.text = damage.ToString();
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		yield return new WaitForSeconds(0.5f);

		var color = damageText.color;
		while (color.a > 0)
		{
			color.a -= Time.deltaTime / 0.5f;
			damageText.color = color;
			yield return null;
		}

		FadeIn();
	}

	public void FadeIn()
	{
		var color = damageText.color;
		color.a = 1;

		damageText.color = color;
		damageText.gameObject.SetActive(false);
	}
}
