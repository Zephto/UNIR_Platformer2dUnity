using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    
    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerDetection"))
		{
			Debug.Log("Player Detectado");
		}
		else if (collision.gameObject.CompareTag("PlayerHitbox"))
		{
			Debug.Log("Player Atravesado");
			LifeSystem lf = collision.gameObject.GetComponent<LifeSystem>();
			lf.ReceiveDamage(attackDamage);
		}
	}
}
