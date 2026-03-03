using UnityEngine;

namespace Weapons
{
    public class EffectDespawnHandle : MonoBehaviour
    {
        public float despawnTime;

        // Update is called once per frame
        void Update()
        {
            despawnTime -= Time.deltaTime;
            if (despawnTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
