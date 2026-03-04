using Player;
using UnityEngine;

namespace Weapons
{
    internal enum LastAttackType
    {
        None,
        NormalAttack,
        SpecialAttack
    }
    
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

        [SerializeField] private TriggerNotifier triggerNotifier;
        private LastAttackType lastAttackType = LastAttackType.None;
        #endregion

        public abstract void UseWeapon(WeaponHandler weaponHandler = null);
        public abstract void UseSpecialAttack(WeaponHandler weaponHandler = null);
        public abstract void EndSpecialAttack();

        private void Awake()
        {
            triggerNotifier = transform.GetChild(0).gameObject.GetComponent<TriggerNotifier>();
            
            if (triggerNotifier)
            {
                triggerNotifier.OnTriggerEnterOccur += OnTargetHit;
                Debug.Log($"{gameObject.name}: {triggerNotifier.gameObject} is connected");
            }
            else
            {
                Debug.LogError($"{gameObject.name}: {transform.GetChild(0).gameObject} has no triggerNotifier component");
            }
        }
        
        protected void StartNormalCooldown()
        {
            if (isAttackOnCooldown) return;
            
            isAttackOnCooldown = true;
            currAttackCooldown = attackBaseCooldown;
            
            lastAttackType = LastAttackType.NormalAttack;
        }

        protected void StartSpecialCooldown()
        {
            if (isSpecialOnCooldown) return;
            
            isSpecialOnCooldown = true;
            currSpecialCooldown = specialBaseCooldown;
            
            lastAttackType = LastAttackType.SpecialAttack;
        }

        protected void OnTargetHit(Damageable damageable)
        {
            float finalDamage;
            
            switch (lastAttackType)
            {
                case LastAttackType.NormalAttack:
                    finalDamage = attackDamage;
                    break;
                case LastAttackType.SpecialAttack:
                    finalDamage = specialDamage;
                    break;
                default:
                    Debug.LogWarning("No last attack type found");
                    return;
            }
            
            Debug.Log($"{gameObject.name}: {damageable.gameObject.name}: {finalDamage} damage");
            damageable.TakeDamage(finalDamage);
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
        
    }
}