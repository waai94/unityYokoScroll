using UnityEngine;

public class DamageZoneScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DamageZoneScript OnTriggerEnter: " + other.name);
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(99999); //落下用なので即死ダメージ
            }
        }
    }
}
