using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExpirianceHandler : MonoBehaviour
{
	public int exp = 100;
	public int totalexp = 50;
	public Text manaText;
	public Button startButton;

	private void Start()
	{
		exp = totalexp;
		UpdateManaText();

		startButton.onClick.AddListener(OnStartButtonPressed);
	}

	public void OnStartButtonPressed()
	{
		exp -= 5;
		UpdateManaText();

		SceneManager.LoadScene(1);
	}

	private void UpdateManaText()
	{
		manaText.text = exp.ToString() + " / " + totalexp.ToString();
	}
}
