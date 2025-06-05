using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public static HUD Instance { get; private set; }

	[Header("Initial Instructions references")]
	[SerializeField] private GameObject initialInstructions;
	[SerializeField] private Button startButton;

	[Header("InGame References")]
	[SerializeField] private GameObject inGameObjects;

	[Header("Ending References")]
	[SerializeField] private GameObject endingObjects;
	[SerializeField] private TextMeshProUGUI finalScore;
	[SerializeField] private Button playAgain;

	[Header("Gameover references")]
	[SerializeField] private GameObject gameoverObjects;
	[SerializeField] private TextMeshProUGUI gameoverScore;
	[SerializeField] private Button gameoverPlayAgain;

	void Awake()
	{
		if (Instance != null)
		{
			Debug.Log("Ya existe el hud we :v");
			Destroy(this.gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		if (GlobalData.CurrentLevel == 1)
		{
			initialInstructions.SetActive(true);
			inGameObjects.SetActive(false);
			endingObjects.SetActive(false);
			gameoverObjects.SetActive(false);
			FindAnyObjectByType<Player>().CanPlay(false);
		}

		startButton.onClick.AddListener(() => StartGame());
		playAgain.onClick.AddListener(() => RestartGame());
		gameoverPlayAgain.onClick.AddListener(() => RestartGame());

		GlobalData.OnEndGame.AddListener(() => ShowEnding());
		GlobalData.OnGameOver.AddListener(() => ShowGameover());
	}

	private void StartGame()
	{
		// FindAnyObjectByType<Timer>().StartTimer();
		TransitionScreen.Instance.In(() =>
		{
			initialInstructions.SetActive(false);
			inGameObjects.SetActive(true);
			endingObjects.SetActive(false);
			gameoverObjects.SetActive(false);
			FindAnyObjectByType<Player>().CanPlay(true);
			FindAnyObjectByType<MainLevel>().StartLevel();
		});
	}

	private void RestartGame()
	{
		TransitionScreen.Instance.In(() =>
		{
			GlobalData.ResetGlobalValues();
			// FindAnyObjectByType<MainLevel>().StartLevel();
			SceneChanger.Instance.LoadSceneAsync("Level");
			Destroy(this.gameObject);
		});
	}

	public void ShowEnding()
	{

		finalScore.text = FindAnyObjectByType<Timer>().GetCurrentTime();
		initialInstructions.SetActive(false);
		inGameObjects.SetActive(false);
		endingObjects.SetActive(true);
		gameoverObjects.SetActive(false);

		TransitionScreen.Instance.Out(null);
	}

	public void ShowGameover()
	{

		gameoverScore.text = FindAnyObjectByType<Timer>().GetCurrentTime();
		initialInstructions.SetActive(false);
		inGameObjects.SetActive(false);
		endingObjects.SetActive(false);
		gameoverObjects.SetActive(true);

		TransitionScreen.Instance.Out(null);
	}
}
