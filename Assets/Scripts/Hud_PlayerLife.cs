using UnityEngine.UI;
using UnityEngine;

public class Hud_PlayerLife : MonoBehaviour
{
	private Image lifeImage;

	void Start()
	{
		GlobalData.OnPlayerLife.AddListener((value) => SetLife(value));
	}

	private void SetLife(float value) {
		lifeImage.fillAmount = value / 100f;
	}
}
