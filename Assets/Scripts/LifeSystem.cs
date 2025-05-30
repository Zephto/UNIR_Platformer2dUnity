using UnityEngine;

public class LifeSystem : MonoBehaviour
{

	[SerializeField] private float life = 100f;

	public void ReceiveDamage(float damage)
	{
		life -= damage;
		if (life <= 0)
		{
			Destroy(this.gameObject);
		}
	}

}
