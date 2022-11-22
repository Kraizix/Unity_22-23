using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float dam)
    {
        currentHealth -= (int)dam;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
