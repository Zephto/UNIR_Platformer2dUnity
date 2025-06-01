using UnityEngine;

public class GemBehaviour : MonoBehaviour
{
	void Start()
	{
		if (Random.value > 0.4f)
		{
			this.gameObject.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerHitbox"))
		{
			Debug.Log("Colisiono con el jugador");
			collision.GetComponent<LifeSystem>().AddLife(10f);
			Destroy(this.gameObject);
			return;
		}
	}
}
