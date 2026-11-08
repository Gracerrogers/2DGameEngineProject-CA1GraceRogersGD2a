using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 200; //maximum health that any enemy can have
    public int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destroy(gameObject); //if the enemy has 0 health, they 'die' (their game object gets destroyed)
        }
    }
}
