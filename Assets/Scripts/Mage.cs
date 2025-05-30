using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Mage : MonoBehaviour
{
	[SerializeField] private GameObject fireball; // prefab
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private float attackTime;
	[SerializeField] private float attackDamage;
	private Animator anim;

	void Start()
	{
		anim = this.GetComponent<Animator>();
		StartCoroutine(AttackRoutine());
	}

	void Update()
	{

	}

	private void ShotFireball() {
		Instantiate(fireball, spawnPoint.position, this.transform.rotation);
	}
	
	IEnumerator AttackRoutine()
	{
		while (true)
		{
			anim.SetTrigger("atacar");
			yield return new WaitForSeconds(attackTime);
		}
		// yield return null;
	}
}
