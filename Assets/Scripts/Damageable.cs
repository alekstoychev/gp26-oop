using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private float maxHealth = 15;
    private float health;

    private void Awake()
    {
        health = maxHealth;
    }
    
    public Action<float> OnDamage;

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        OnDamage?.Invoke(damage);
    }
}
