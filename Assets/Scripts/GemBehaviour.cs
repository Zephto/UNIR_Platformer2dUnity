using UnityEngine;

public class GemBehaviour : MonoBehaviour
{
	[SerializeField] private ParticleSystem ps;

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
			ParticleSystem newPs = Instantiate(ps, this.transform.position, Quaternion.identity);
			newPs.Play();
			collision.GetComponent<LifeSystem>().AddLife(10f);
			Destroy(this.gameObject);
			return;
		}
	}
}
