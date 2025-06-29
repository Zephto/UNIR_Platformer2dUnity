using UnityEngine;
using UnityEngine.Events;

public class GlobalData : MonoBehaviour
{
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

	public static void ResetGlobalValues()
	{
		_currentLevel = 1;
		_currentPlayerLife = 100;
	}

	public static UnityEvent<float> OnPlayerLife = new UnityEvent<float>();
	public static UnityEvent OnEndGame = new UnityEvent();
	public static UnityEvent OnGameOver = new UnityEvent();
	
}
