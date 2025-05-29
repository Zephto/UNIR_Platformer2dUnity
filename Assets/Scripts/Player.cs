using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D rb;
	private float inputH;

	[SerializeField] private float playerVelocity = 5;
	[SerializeField] private float jumpForce = 12;

	void Start() {
		rb = this.GetComponent<Rigidbody2D>();
	}

	void Update() {
		inputH = Input.GetAxisRaw("Horizontal");
		//Con el velocity no hay que jugar con el delta, por que ya esta medida por segundos
		rb.linearVelocity = new Vector2(inputH * playerVelocity, rb.linearVelocity.y);

		if(Input.GetKeyDown(KeyCode.Space)){
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}
}
