using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class TestDummyHandler : MonoBehaviour
{
    private Damageable damageable;

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
        damageable.OnDamage += TakeDamage;
    }
    
    void TakeDamage(float damage)
    {
        Debug.Log(damage);
    }
}
