using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
	public static GameHandler instance;



	[Header("Button")]
	[SerializeField] private Button pauseButton;
	[SerializeField] private Button resumeButton;
	[SerializeField] private Button homeButton;
	[SerializeField] private Button restartButton;
	[Header("UI")]
	[SerializeField] private Slider experienceBar;
	public Text[] killText;
	public Text[] timerText;

	[Header("Container")]
	public Transform shopItemContainer;
	public GameObject gamePanel;
	public GameObject gameOverPanel;
	public GameObject gamepausePanel;

	[Header("float")]
	[SerializeField] private float duration;

	private int killCount = 0;
	private float maxExperience = 10f;
	private float experience = 0f;
	private string timeString;
	private float timeLeft = 0f;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}


	private void Start()
	{
		Time.timeScale = 1;
		experienceBar.value = experience;

		pauseButton.onClick.AddListener(PauseGame);
		resumeButton.onClick.AddListener(ResumeGame);
		homeButton.onClick.AddListener(HomeButton);
		restartButton.onClick.AddListener(RestartButton);
	}

	void Update()
	{
		if (experienceBar.value == maxExperience)
			LevelUp();

		Timer();
	}

	private void LevelUp()
	{
		experience = 0f;
		experienceBar.value = 0;
		maxExperience *= 1.2f;
		experienceBar.maxValue = maxExperience;
		shopItemContainer.gameObject.SetActive(true);

	}

	public void GainExperience(float amount)
	{
		experience += amount;
		StartCoroutine(MoveSlider(experience));
	}

	IEnumerator MoveSlider(float targetValue)
	{
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / duration;
			experienceBar.value = Mathf.Lerp(experienceBar.value, targetValue, t);
			yield return null;
		}
		experienceBar.value = targetValue;
	}


	public void KillCounter()
	{
		killCount += 1;
		killText[0].text = killCount.ToString();
		
	}

	private void Timer()
	{
		timeLeft += Time.deltaTime;

		string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
		string seconds = Mathf.Floor(timeLeft % 60).ToString("00");
		timeString = string.Format("{0}:{1}", minutes, seconds);

		timerText[0].text = timeString;
	}

	public void GameOver()
	{
		gameOverPanel.SetActive(true);
		timerText[1].text = timeString;
		killText[1].text = killCount.ToString();
	}

	public void PauseGame()
	{
		gamepausePanel.SetActive(true);
		Time.timeScale = 0;
	}

	public void ResumeGame()
	{
		gamepausePanel.SetActive(false);
		Time.timeScale = 1;
	}

	public void HomeButton()
	{
		SceneManager.LoadScene(0);
	}

	public void RestartButton()
	{
		SceneManager.LoadScene(1);
	}
}
