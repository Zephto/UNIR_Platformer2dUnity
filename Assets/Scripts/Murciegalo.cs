using System.Collections;
using UnityEngine;

public class Murciegalo : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float walkVelocity;
    [SerializeField] private float attackDamage;
    [SerializeField] private ParticleSystem batBlood;
    [SerializeField] private GameObject gem;
    private Vector3 currentDestination;
    private int currentIndex = 0;

    private Animator anim;
    private LifeSystem lifeSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = this.GetComponent<Animator>();
        lifeSystem = this.GetComponent<LifeSystem>();

        lifeSystem.OnReceiveDamage.AddListener(() => ReceiveDamage());

        currentDestination = waypoints[currentIndex].position;
        FocusToDestination();
    }

    void OnEnable()
    {
        StartCoroutine(Patrol());
    }

    private void ReceiveDamage()
    {
        anim.SetTrigger("hit");
        batBlood.Play();
        LeanTween.color(this.gameObject, Color.red, 0.0f);

        LeanTween.delayedCall(0.3f, () =>
        {
            LeanTween.color(this.gameObject, Color.white, 0.0f);
        });

        // if (lifeSystem.GetCurrentLife() <= 0)// && Random.value > 0.5)
        // {
        //     GameObject newGem = Instantiate(gem, this.transform.position, this.transform.rotation);
        //     newGem.GetComponent<Rigidbody2D>().AddForce(Vector3.up, ForceMode2D.Impulse);
        // }
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
        Debug.Log("Colisionando con algo + " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Collectables"))
        {
            Debug.Log("la gemaaa");
            return;
        }

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
