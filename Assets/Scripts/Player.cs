using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D rb;
	private float inputH;
	private Animator anim;
	private LifeSystem lifeSystem;

	[Header("Movement system")]
	[SerializeField] private Transform playerBase;
	[SerializeField] private float playerVelocity = 5;
	[SerializeField] private float jumpForce = 12;
	[SerializeField] private float coyoteTime = 0.02f;
	[SerializeField] private float distanceFloorDetection = 0.15f;
	[SerializeField] private LayerMask layerFloor;
	private float coyoteTimeCounter;
	private float floorCheckerTimer;
	private bool isOnFloor = false;
	private bool canPlay = true;


	[Header("Combat system")]
	[SerializeField] private Transform attackPoint;
	[SerializeField] private float radioAttack;
	[SerializeField] private float attackDamage;
	[SerializeField] private LayerMask layerToAttack;

	[Header("Movement system")]
	[SerializeField] private ParticleSystem bloodParticles;

	void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();
		anim = this.GetComponent<Animator>();
		lifeSystem = this.GetComponent<LifeSystem>();

		lifeSystem.OnReceiveDamage.AddListener((value) => ReceiveDamage(value));
		lifeSystem.OnReceiveHeal.AddListener((value) => ReceiveHeal(value));
		lifeSystem.OnDeath.AddListener(()=>Death());

		GlobalData.OnPlayerLife?.Invoke(GlobalData.CurrentPlayerLife);
		lifeSystem.SetLife(GlobalData.CurrentPlayerLife);
	}

	public void SetInitValues()
	{
		GlobalData.CurrentPlayerLife = lifeSystem.GetCurrentLife();
	}

	void Update()
	{
		if (canPlay)
		{
			Movement();
			// CoyoteCheck();
			Jump();
			StartAttack();
		}
	}

	// private void CoyoteCheck()
	// {
	// 	floorCheckerTimer -= Time.deltaTime;
	// 		Debug.Log("floorCheckerTimer =" + floorCheckerTimer);
	// 	if (floorCheckerTimer <= 0)
	// 	{
	// 		isOnFloor = IsPlayerOnFloor();
	// 		floorCheckerTimer = 0.1f;

	// 		if (isOnFloor) coyoteTimeCounter = coyoteTime;
	// 	}

	// 	if (!isOnFloor) coyoteTime -= Time.deltaTime;
	// }

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
		// if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
		if (Input.GetKeyDown(KeyCode.Space) && IsPlayerOnFloor())
		{
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			anim.SetTrigger("jump");
			coyoteTimeCounter = 0f;
		}
	}

	private bool IsPlayerOnFloor()
	{
		bool isLanding = Physics2D.Raycast(playerBase.position, Vector3.down, distanceFloorDetection, layerFloor);
		Debug.DrawRay(playerBase.position, Vector3.down, Color.cyan, 0.4f);

		Debug.Log(isLanding);
		return isLanding;
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

	private void ReceiveDamage(float value)
	{
		Debug.Log("Receive Damage");
		GlobalData.CurrentPlayerLife -= value;
		GlobalData.OnPlayerLife?.Invoke(GlobalData.CurrentPlayerLife);

		bloodParticles.Play();
		LeanTween.color(this.gameObject, Color.red, 0.0f);

		LeanTween.delayedCall(0.3f, () =>
		{
			LeanTween.color(this.gameObject, Color.white, 0.0f);
		});

		if (lifeSystem.GetCurrentLife() <= 0)
		{
			//Death
			anim.SetTrigger("death");
			canPlay = false;
			// this.enabled = false;
		}
		else
		{
			//Hurt
			anim.SetTrigger("isHurt");
		}
	}

	private void ReceiveHeal(float value)
	{
		GlobalData.CurrentPlayerLife += value;
		GlobalData.OnPlayerLife?.Invoke(GlobalData.CurrentPlayerLife);
	}

	private void Death()
	{
		LeanTween.delayedCall(1f, () =>
		{
			TransitionScreen.Instance.In(() =>
			{
				GlobalData.OnGameOver?.Invoke();
			});
		});
	}

	public void CanPlay(bool set) => canPlay = set;

	void OnDrawGizmos()
	{
		Gizmos.DrawSphere(attackPoint.position, radioAttack);
		// Gizmos.DrawLine(playerBase.position, Vector3.down);
	}
}
