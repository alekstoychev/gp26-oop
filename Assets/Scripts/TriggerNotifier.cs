using System;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerNotifier : MonoBehaviour
{
    public Action<Damageable> OnTriggerEnterOccur;

    private void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable)
        {
            OnTriggerEnterOccur?.Invoke(damageable);
            if (OnTriggerEnterOccur != null)
            {
                Debug.Log(gameObject.name + " has triggered " + damageable);
            }
            else
            {
                Debug.Log(gameObject.name + " has no triggered " + damageable);
            }
        }
        else
        {
            Debug.LogWarning("Hit target is not damageable");
        }
    }
}
