using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	public bool IsGameStart;

	public UnityEvent OnGameStart;
	public UnityEvent OnGameOver;

	public CinemachineCamera GameCamera;
	public CinemachineCamera MenuCamera;

	public TextMeshProUGUI HighScore;
	public TextMeshProUGUI CurrentScore;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		IsGameStart = false;
		HighScore.text = PlayerPrefs.GetFloat("Highscore", 0).ToString();
	}

	public void GameStart()
	{
		OnGameStart?.Invoke();
		IsGameStart = true;
		GameCamera.Priority = MenuCamera.Priority +1;
	}

	public void GameOver(float score)
	{
		OnGameOver?.Invoke();
		IsGameStart = false;
		CurrentScore.text = score.ToString();
		if(score > PlayerPrefs.GetFloat("Highscore", 0))
		{
			PlayerPrefs.SetFloat("Highscore", score);
		}
	}

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
