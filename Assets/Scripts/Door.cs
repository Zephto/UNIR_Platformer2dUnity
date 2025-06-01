using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
	[HideInInspector] public UnityEvent OnTouchDoor = new UnityEvent();

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHitbox"))
		{
			Debug.Log("TRIGGER CONM PLAYUER");
			OnTouchDoor?.Invoke();
			this.GetComponent<Collider2D>().enabled = false;
		}
	}
}
