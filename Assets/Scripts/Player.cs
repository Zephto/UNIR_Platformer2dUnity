using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D rb;
	private float inputH;
	private Animator anim;

	[Header("Movement system")]
	[SerializeField] private Transform playerBase;
	[SerializeField] private float playerVelocity = 5;
	[SerializeField] private float jumpForce = 12;
	[SerializeField] private float distanceFloorDetection = 0.15f;
	[SerializeField] private LayerMask layerFloor;


	[Header("Combat system")]
	[SerializeField] private Transform attackPoint;
	[SerializeField] private float radioAttack;
	[SerializeField] private float attackDamage;
	[SerializeField] private LayerMask layerToAttack;

	void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();
		anim = this.GetComponent<Animator>();
	}

	void Update()
	{
		Movement();
		Jump();
		StartAttack();
	}

	private void StartAttack()
	{
		if (Input.GetMouseButtonDown(0))
		{
			anim.SetTrigger("attack");
		}
	}

	//Se manda a llamar desde animacion
	private void Attack()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, radioAttack, layerToAttack);

		foreach (Collider2D item in colliders)
		{
			LifeSystem lf = item.gameObject.GetComponent<LifeSystem>();
			lf.ReceiveDamage(attackDamage);
		}
	}

	private void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space) && IsPlayerOnFloor())
		{
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			anim.SetTrigger("jump");
		}
	}

	private bool IsPlayerOnFloor()
	{
		Debug.DrawRay(playerBase.position, Vector3.down, Color.cyan, 0.4f);
		return Physics2D.Raycast(playerBase.position, Vector3.down, distanceFloorDetection, layerFloor);
	}

	private void Movement()
	{
		inputH = Input.GetAxisRaw("Horizontal");
		//Con el velocity no hay que jugar con el delta, por que ya esta medida por segundos
		rb.linearVelocity = new Vector2(inputH * playerVelocity, rb.linearVelocity.y);
		if (inputH != 0)
		{
			anim.SetBool("running", true);

			if (inputH > 0)
			{ //Derecha
				this.transform.eulerAngles = Vector3.zero;
			}
			else
			{ //Izquierda
				this.transform.eulerAngles = new Vector3(0, 180, 0);
			}
		}
		else
		{
			anim.SetBool("running", false);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawSphere(attackPoint.position, radioAttack);
		// Gizmos.DrawLine(playerBase.position, Vector3.down);
	}
}
