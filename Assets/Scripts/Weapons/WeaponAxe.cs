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
        
        public override void UseWeapon(WeaponHandler weaponHandler)
        {
            if (isAttackOnCooldown) return;

            //Debug.Log("Weapon used");
            StartNormalCooldown();
            GetComponent<Animator>().SetBool("isAttacking", true);
        }

        public override void UseSpecialAttack(WeaponHandler weaponHandler)
        {
            if (isSpecialOnCooldown) return;
            
            //Debug.Log("WeaponAxe: Special effect started");
            GetComponent<Animator>().SetBool("isSpecial", true);
            effectAreaShowObject = Instantiate(effectAreaShowPrefab, gameObject.transform.position, gameObject.transform.rotation); 
            
            isSpecialActive = true;
            
            if (weaponHandler)
            {
                currWeaponHandler = weaponHandler;
            }
        }

        public override void EndSpecialAttack()
        {
            if (isSpecialOnCooldown) return;
            
            StartSpecialCooldown();
            //Debug.Log("WeaponAxe: Special effect ended");
            
            isSpecialActive = false;
            currWeaponHandler = null;
            
            if (!effectAreaShowObject)
            {
                Debug.LogError("No effect area show found");
                return;
            }
            
            GameObject effectInstance = Instantiate(effectPrefab, effectAreaShowObject.transform.position, effectAreaShowObject.transform.rotation);
            effectInstance.GetComponent<TriggerNotifier>().OnTriggerEnterOccur += OnTargetHit;
                
            Destroy(effectAreaShowObject);
        }
    }
}