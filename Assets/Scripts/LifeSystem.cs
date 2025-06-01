using UnityEngine;
using UnityEngine.Events;

public class LifeSystem : MonoBehaviour
{

	[SerializeField] private float life = 100f;

	[HideInInspector] public UnityEvent OnReceiveDamage = new UnityEvent();

	public void ReceiveDamage(float damage)
	{
		OnReceiveDamage?.Invoke();

		life -= damage;
		if (life <= 0)
		{
			Destroy(this.gameObject);
		}
	}

}
