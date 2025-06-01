using UnityEngine.UI;
using UnityEngine;

public class Hud_PlayerLife : MonoBehaviour
{
	private Image lifeImage;

	void Start()
	{
		lifeImage = this.GetComponent<Image>();
		GlobalData.OnPlayerLife.AddListener((value) => SetLife(value));
	}

	private void SetLife(float value) {
		Debug.Log("current life: " + value);
		lifeImage.fillAmount = value / 100f;
	}
}
