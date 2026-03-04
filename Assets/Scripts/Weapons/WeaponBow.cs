using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class WeaponBow : WeaponBase
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Vector3 arrowVelocity;
        [SerializeField] private Vector3 specialOffset;
        [SerializeField] private Vector3 spawnOffset;

        private GameObject arrowObject;

        public override void UseWeapon(WeaponHandler weaponHandler)
        {
            if (isAttackOnCooldown) return;

            Transform cameraTransform = weaponHandler.GetCameraTransform();
            LaunchArrow(cameraTransform);

            //Debug.Log("Weapon used");
            StartNormalCooldown();
            //GetComponent<Animator>().SetBool("isAttacking", true);
        }

        public override void UseSpecialAttack(WeaponHandler weaponHandler)
        {
            if (isSpecialOnCooldown) return;
            
            Transform cameraTransform = weaponHandler.GetCameraTransform();
            
            LaunchArrow(cameraTransform, -specialOffset);
            LaunchArrow(cameraTransform);
            LaunchArrow(cameraTransform, specialOffset);
            
            //Debug.Log("WeaponBow: Special effect started");
            //GetComponent<Animator>().SetBool("isSpecial", true);
        }

        public override void EndSpecialAttack()
        {
            if (isSpecialOnCooldown) return;
            
            StartSpecialCooldown();
            //Debug.Log("WeaponScythe: Special effect ended");
        }

        private void LaunchArrow(Transform startingTransform, Vector3 rotationOffset = new Vector3())
        {
            arrowObject = Instantiate(arrowPrefab, startingTransform.position, startingTransform.rotation);
            arrowObject.transform.Rotate(rotationOffset);
            arrowObject.GetComponent<Rigidbody>().AddRelativeForce(arrowVelocity);
            
            arrowObject.GetComponent<TriggerNotifier>().OnTriggerEnterOccur += OnTargetHit;
        }
    }
}
