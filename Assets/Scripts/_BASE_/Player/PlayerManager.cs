using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public CharacterController playerController;
        [HideInInspector] public PlayerCameraManager playerCameraManager;
        [HideInInspector] public PlayerInputManager playerInputManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerCombatManager playerCombatManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager; 
        [HideInInspector] public PlayerUIManager playerUIManager;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponentInChildren<Animator>();
            playerController = GetComponent<CharacterController>();
            playerCameraManager = GetComponentInChildren<PlayerCameraManager>();
            playerInputManager = GetComponent<PlayerInputManager>();
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerUIManager = FindFirstObjectByType<PlayerUIManager>(); 
        }
        public void Update()
        {
            animator.SetBool("isGrounded", isGrounded);

            playerInputManager.UseAllInputs();
            playerLocomotionManager.UseAllMovement();
            playerCameraManager.UseAllCameraMovement();
            playerCombatManager.AttemptToWallRun();
            playerStatsManager.RegenStamina();
            playerUIManager.UseTheSkillStats();
            playerUIManager.AttemptToUseSkillMenu();
        }
    }
}