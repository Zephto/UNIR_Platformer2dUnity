using Mono.Cecil.Cil;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public static HUD Instance { get; private set; }

	[Header("Initial Instructions references")]
	[SerializeField] private GameObject InitialInstructions;
	[SerializeField] private Button startButton;

	[Header("InGame References")]
	[SerializeField] private GameObject inGameObjects;

	[Header("Ending References")]
	[SerializeField] private GameObject endingObjects;
	[SerializeField] private Button playAgain;

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
			InitialInstructions.SetActive(true);
			inGameObjects.SetActive(false);
			endingObjects.SetActive(false);
			FindAnyObjectByType<Player>().CanPlay(false);
		}

		startButton.onClick.AddListener(() => StartGame());
		playAgain.onClick.AddListener(() => RestartGame());
	}

	private void StartGame()
	{
		// FindAnyObjectByType<Timer>().StartTimer();
		TransitionScreen.Instance.In(() =>
		{
			InitialInstructions.SetActive(false);
			inGameObjects.SetActive(true);
			endingObjects.SetActive(false);
			FindAnyObjectByType<Player>().CanPlay(true);
			FindAnyObjectByType<MainLevel>().StartLevel();
		});
	}

	private void RestartGame()
	{

	}

	public void ShowEnding()
	{
		
	}
}
