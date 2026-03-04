using System;
using Player;
using UnityEngine;

namespace Weapons
{
    public class WeaponScythe : WeaponBase
    {
        [SerializeField] private GameObject effectPrefab;

        private WeaponHandler currWeaponHandler;
        
        public override void UseWeapon(WeaponHandler weaponHandler)
        {
            if (isAttackOnCooldown) return;

            //Debug.Log("Weapon used");
            StartNormalCooldown();
            GetComponent<Animator>().SetBool("isAttacking", true);
        }

        private void DisableIsSpecial()
        {
            GetComponent<Animator>().SetBool("isSpecial", false);
        }

        public override void UseSpecialAttack(WeaponHandler weaponHandler)
        {
            if (isSpecialOnCooldown) return;
            
            //Debug.Log("WeaponScythe: Special effect started");
            GetComponent<Animator>().SetBool("isSpecial", true);
            
            if (weaponHandler)
            {
                currWeaponHandler = weaponHandler;
            }
        }

        private void SpawnEffect()
        {
            Instantiate(effectPrefab, currWeaponHandler.gameObject.transform.position, Quaternion.identity);
        }

        public override void EndSpecialAttack()
        {
            if (isSpecialOnCooldown) return;
            
            StartSpecialCooldown();
            //Debug.Log("WeaponScythe: Special effect ended");
        }
    }
}
