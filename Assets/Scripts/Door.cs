using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
	[HideInInspector] public UnityEvent OnTouchDoor = new UnityEvent();

	void OnTriggerEnter2D(Collider2D collision)
	{
		OnTouchDoor?.Invoke();
	}
}
