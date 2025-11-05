using UnityEngine;

public class EnemyDeathScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize enemy death logic here
        HealthManager healthManager = GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.OnDeath += OnEnemyDeath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnemyDeath()
    {
        Debug.Log("Enemy has died by script.");
        // Add additional logic for enemy death here
    }
}
