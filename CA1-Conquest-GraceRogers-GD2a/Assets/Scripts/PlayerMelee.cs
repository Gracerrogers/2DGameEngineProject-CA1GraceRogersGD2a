using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMelee : MonoBehaviour
{
    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask enemyMask;

    public int attackDamage = 25;

    public float cooldownTime = .5f;
    private float cooldownTimer = 0f;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (cooldownTimer <= 0) //stops player from attacking multiple times at once
        {
            if (InputSystem.actions["Attack"].IsPressed())
            {
                animator.SetTrigger("Attack");

                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyMask);

                foreach (var enemy in enemiesInRange)
                {
                    enemy.GetComponent<HealthManager>().TakeDamage(attackDamage);
                }

                cooldownTimer = cooldownTime;
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos() //draws in the damage zone for the player 
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}
