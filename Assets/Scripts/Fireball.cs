using UnityEngine;

public class Fireball : MonoBehaviour
{
	private Rigidbody2D rb;
	[SerializeField] private float impulse;

	void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();

		//transform.forward --> Eje Z (azul)
		//transform.up --> Eje Y (verde)
		//transform.right --> Eje X (rojo)
		rb.AddForce(this.transform.right * impulse, ForceMode2D.Impulse);
	}

	void Update()
	{
		
	}
}
