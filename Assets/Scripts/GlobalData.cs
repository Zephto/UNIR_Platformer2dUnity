using UnityEngine;
using UnityEngine.Events;

public class GlobalData : MonoBehaviour
{
	public static GlobalData Instance { get; private set; }
	private static float _currentPlayerLife = 100;
	public static float CurrentPlayerLife
	{
		get => _currentPlayerLife;
		set
		{
			_currentPlayerLife = value;
			if (_currentPlayerLife >= 100) _currentPlayerLife = 100;
			if (_currentPlayerLife <= 0) _currentPlayerLife = 0;
		}
	}

	private static int _currentLevel = 1;
	public static int CurrentLevel
	{
		get => _currentLevel;
		set => _currentLevel = value;
	}

	public static UnityEvent<float> OnPlayerLife = new UnityEvent<float>();

	void Awake() {
		if(Instance != null){
			Debug.Log("Ya existe el transition screen we :v");
			Destroy(this.gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
}
