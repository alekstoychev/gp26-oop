using Player;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        #region Variables
        [Header("Normal attack")]
        [SerializeField] private float attackDamage;
        [SerializeField] private float attackBaseCooldown;
        protected bool isAttackOnCooldown;
        private float currAttackCooldown;
        
        [Header("Special attack")]
        [SerializeField] private float specialDamage;
        [SerializeField] private float specialBaseCooldown;
        protected bool isSpecialOnCooldown;
        private float currSpecialCooldown;
        #endregion

        protected void StartNormalCooldown()
        {
            if (isAttackOnCooldown) return;
            
            isAttackOnCooldown = true;
            currAttackCooldown = attackBaseCooldown;
        }

        protected void StartSpecialCooldown()
        {
            if (isSpecialOnCooldown) return;
            
            isSpecialOnCooldown = true;
            currSpecialCooldown = specialBaseCooldown;
        }

        void Update()
        {
            if (isAttackOnCooldown)
            {
                currAttackCooldown -= Time.deltaTime;
                if (currAttackCooldown <= 0)
                {
                    isAttackOnCooldown = false;
                    GetComponent<Animator>().SetBool("isAttacking", false);
                }
            }

            if (isSpecialOnCooldown)
            {
                currSpecialCooldown -= Time.deltaTime;
                if (currSpecialCooldown <= 0)
                {
                    isSpecialOnCooldown = false;
                    GetComponent<Animator>().SetBool("isSpecial", false);
                }
            }
        }
        
        public abstract void UseWeapon();
        public abstract void UseSpecialAttack(WeaponHandler weaponHandler);
        public abstract void EndSpecialAttack();
    }
}