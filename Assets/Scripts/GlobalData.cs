using UnityEngine;

public class GlobalData : MonoBehaviour
{
	private static int _currentLevel = 0;
	public static int CurrentLevel
	{
		get => _currentLevel;
		set => _currentLevel = value;
	}
}
