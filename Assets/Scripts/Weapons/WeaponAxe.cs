using Player;
using UnityEngine;

namespace Weapons
{
    public class WeaponAxe : WeaponBase
    {
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private GameObject effectAreaShowPrefab;
        
        private GameObject effectAreaShowObject;
        private WeaponHandler currWeaponHandler;

        private bool isSpecialActive;

        private void FixedUpdate()
        {
            if (isSpecialActive)
            {
                effectAreaShowObject.transform.position = currWeaponHandler.GetLookAtPosition();
            }
        }
        
        public override void UseWeapon()
        {
            if (isAttackOnCooldown) return;

            Debug.Log("Weapon used");
            StartNormalCooldown();
            GetComponent<Animator>().SetBool("isAttacking", true);
        }

        public override void UseSpecialAttack(WeaponHandler weaponHandler)
        {
            if (isSpecialOnCooldown) return;
            
            Debug.Log("WeaponAxe: Special effect started");
            GetComponent<Animator>().SetBool("isSpecial", true);
            effectAreaShowObject = Instantiate(effectAreaShowPrefab, gameObject.transform.position, gameObject.transform.rotation); 
            
            isSpecialActive = true;
            currWeaponHandler = weaponHandler;
        }

        public override void EndSpecialAttack()
        {
            if (isSpecialOnCooldown) return;
            
            StartSpecialCooldown();
            Debug.Log("WeaponAxe: Special effect ended");
            
            isSpecialActive = false;
            currWeaponHandler = null;
            
            if (effectAreaShowObject != null)
            {
                Instantiate(effectPrefab, effectAreaShowObject.transform.position, effectAreaShowObject.transform.rotation);
            
                Destroy(effectAreaShowObject);
            }
        }
    }
}