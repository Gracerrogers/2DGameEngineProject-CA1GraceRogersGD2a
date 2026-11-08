using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float walkTime;
    [SerializeField] private float idleTime;
    private float elapsedTime;
    private int direction = 1;
    private bool isWalking = false;
    private Rigidbody2D rb;
    private Animator animator;

    //projectile variables
    private float fireCountDown;
    [SerializeField] private float fireTimer = 0.5f;
    [SerializeField] private GameObject projectile;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction", direction);
        animator.SetFloat("Move", 0);
    }

    void Update()
    {
        if (isWalking && elapsedTime < walkTime)
        {
            Vector2 position = rb.transform.position;
            position.x += direction * Time.deltaTime;
            rb.transform.position = position;
        }
        else if (!isWalking && elapsedTime > walkTime)
        {
            isWalking = true;
            elapsedTime = 0;
            direction *= -1;
            animator.SetInteger("Direction", direction);
            animator.SetFloat("Move", direction);
        }
        else if (isWalking && elapsedTime > walkTime)
        {
            isWalking = false;
            elapsedTime = 0;
            animator.SetFloat("Move", 0);
        }

        elapsedTime += Time.deltaTime;
    }

    //When player enters the enemy's range, the projectiles will begin to fire
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.transform.position, new Vector2(direction, 0), 5f, LayerMask.GetMask("Player"));

        if(hit.collider != null)
        {
            if(hit.collider.GetComponent<Player>() != null)
            {
                Fire();
            }
        }

        fireCountDown += Time.fixedDeltaTime;
    }

    private void Fire()
    {
       if(fireCountDown > fireTimer)
       {
            GameObject go = Instantiate(projectile, rb.transform.position, Quaternion.identity);
            Projectile pro = go.GetComponent<Projectile>();
            pro.Launch(new Vector2(direction, 0), 300);
            fireCountDown = 0;
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Projectile>() != null)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }
}
