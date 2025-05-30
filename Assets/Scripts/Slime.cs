using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{

	[SerializeField] private Transform[] waypoints;
	[SerializeField] private float walkVelocity;
	[SerializeField] private float attackDamage;
	private Vector3 currentDestination;
	private int currentIndex = 0;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		currentDestination = waypoints[currentIndex].position;
		FocusToDestination();
		StartCoroutine(Patrol());
	}

	// Update is called once per frame
	void Update()
	{
	}

	IEnumerator Patrol()
	{

		while (true)
		{
			while (this.transform.position != currentDestination)
			{
				this.transform.position = Vector3.MoveTowards(this.transform.position, currentDestination, walkVelocity * Time.deltaTime);
				yield return null;
			}
			SetNewDestination();
		}

	}

	private void SetNewDestination()
	{

		currentIndex++;
		if (currentIndex >= waypoints.Length)
		{
			currentIndex = 0;
		}

		currentDestination = waypoints[currentIndex].position;
		FocusToDestination();
	}

	private void FocusToDestination()
	{
		if (currentDestination.x > transform.position.x)
		{
			this.transform.localScale = Vector3.one;
		}
		else
		{
			this.transform.localScale = new Vector3(-1, 1, 1);
		}
	}

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
