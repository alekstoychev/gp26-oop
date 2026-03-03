using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class WeaponHandler : MonoBehaviour
    {
        #region Variables
        [Header("References")]
        [SerializeField] private float pickupRange; 
        [SerializeField] private float weaponMaxRange; 
        [SerializeField] private GameObject playerCamera;
        
        [Header("Input Actions")]
        private InputAction interactAction;
        private InputAction attackAction;
        private InputAction specialAction;

        [Header("Weapon")]
        [SerializeField] private Transform weaponTransform;
        private WeaponBase equippedWeaponBase;
        private GameObject equippedWeaponObject = null;
        #endregion
        
        private void Awake()
        {
            PlayerInput playerInput = FindFirstObjectByType<PlayerInput>();
            if (playerInput)
            {
                interactAction = playerInput.actions.FindAction("Interact");
                if (interactAction != null)
                {
                    interactAction.performed += TryPickupWeapon;
                }
                else
                {
                    Debug.LogError("WeaponHandler: No interact action found");
                }
                
                attackAction = playerInput.actions.FindAction("Fire");
                if (attackAction != null)
                {
                    attackAction.performed += Attack;
                }
                else
                {
                    Debug.LogError("WeaponHandler: No attack action found");
                }
                
                specialAction = playerInput.actions.FindAction("Special");
                if (specialAction != null)
                {
                    specialAction.started += SpecialAttackBegin;
                    specialAction.canceled += SpecialAttackEnd;
                }
                else
                {
                    Debug.LogError("WeaponHandler: no  special action found");
                }
            }
        }

        void Attack(InputAction.CallbackContext callbackContext)
        {
            equippedWeaponBase?.UseWeapon();
        }

        void SpecialAttackBegin(InputAction.CallbackContext callbackContext)
        {
            equippedWeaponBase?.UseSpecialAttack(this);
        }

        void SpecialAttackEnd(InputAction.CallbackContext callbackContext)
        {
            equippedWeaponBase?.EndSpecialAttack();
        }

        public Vector3 GetLookAtPosition()
        {
            Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit,
                weaponMaxRange);
            return hit.point;
        }
        
        private void TryPickupWeapon(InputAction.CallbackContext callbackContext)
        {
            Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, pickupRange);
            
            Pickupable weaponHit = hit.collider.gameObject.GetComponent<Pickupable>();
            if (!weaponHit)
            {
                Debug.Log("WeaponHandler: No pickupable weapon found");
                return;
            }
            
            if (equippedWeaponObject != null)
            {
                Destroy(equippedWeaponObject);
            }

            equippedWeaponObject = Instantiate(weaponHit.weaponPrefab, weaponTransform);
            equippedWeaponObject.transform.localPosition = Vector3.zero;
            equippedWeaponObject.transform.localRotation = Quaternion.identity;

            equippedWeaponBase = equippedWeaponObject.GetComponent<WeaponBase>();
            if (!equippedWeaponBase)
            {
                Debug.LogError("WeaponHandler: No weaponBase script found");
                return;
            }
        }
    }
}