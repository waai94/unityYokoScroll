using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    public delegate void DeathEventHandler();
    public event DeathEventHandler OnDeath;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health at the start
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            //Ž€–Sˆ—‚ð‘—‚è‚½‚¢
            Debug.Log("Player has died");
            OnDeath?.Invoke();
        }
        // Optionally, you can add logic here for when health reaches zero
    }
}
