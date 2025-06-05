using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

	private TextMeshProUGUI timerText;
	private float elapsedTime = 0f;
	private bool isRunning = false;

	private void Start()
	{
		timerText = this.GetComponent<TextMeshProUGUI>();
	}

	public void StartTimer()
	{
		elapsedTime = 0f;
		isRunning = true;
	}

	public void StopTimer()
	{
		isRunning = false;
	}

	public void ResetTimer()
	{
		elapsedTime = 0f;
		UpdateTimerDisplay();
	}

	public string GetCurrentTime()
	{
		int minutes = Mathf.FloorToInt(elapsedTime / 60f);
		int seconds = Mathf.FloorToInt(elapsedTime % 60f);
		int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000);
		return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
	}

	void Update()
	{
		if (isRunning)
		{
			elapsedTime += Time.deltaTime;
			UpdateTimerDisplay();
		}
	}

	private void UpdateTimerDisplay()
	{
		int minutes = Mathf.FloorToInt(elapsedTime / 60f);
		int seconds = Mathf.FloorToInt(elapsedTime % 60f);
		int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000);

		timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
	}
}
