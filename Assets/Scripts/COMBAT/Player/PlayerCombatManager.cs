using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        private PlayerManager player; 

        [Header("REFERENCES")]
        public Transform orientation;
        public Transform playerCam;
        public GameObject leftHandHitbox;
        public GameObject rightHandHitbox;
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }
        public void AttemptToWallRun()
        {
            if(player.playerStatsManager.currentStamina >= 0f)
            {
                RaycastHit hit;
                if (!player.isWallRunning && !player.isGrounded && DetectWallRunCollision(out hit))
                {
                    player.playerLocomotionManager.wallNormal = hit.normal;
                    StartWallRun();
                }
                else if (player.isWallRunning && (player.isGrounded || !DetectWallRunCollision(out hit)))
                {
                    ResetWallRun();
                }

                UseWallRunning();

                // Handle jump input while wall running
                if (player.isWallRunning && player.playerInputManager.jump_Input && !player.playerInputManager.jumpInputHandled)
                {
                    player.playerLocomotionManager.AttemptToPerformJump();
                    player.playerInputManager.jumpInputHandled = true;
                }
            }
        }

        private void UseWallRunning()
        {
            if (player.isWallRunning)
            {
                player.playerLocomotionManager.wallRunTimer -= Time.deltaTime;
                if (player.playerLocomotionManager.wallRunTimer <= 0)
                {
                    ResetWallRun();
                }
                else
                {
                    player.playerController.Move(player.playerLocomotionManager.wallNormal * player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeWallRunForce * Time.deltaTime);
                    player.playerLocomotionManager.velocity.y = player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeWallRunGravity * Time.deltaTime;
                }
            }
        }
        public void StartWallRun()
        {
            player.isWallRunning = true;
            player.playerLocomotionManager.wallRunTimer = player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeWallRunMaxTime;
            player.playerStatsManager.DeductStamina(player.playerInventoryManager.currentPlayerDataBeingUsed.wallRunStaminaCost);
        }
        public void ResetWallRun()
        {
            player.isWallRunning = false;
        }

        private bool DetectWallRunCollision(out RaycastHit hit)
        {
            Vector3 origin = transform.position + new Vector3(0, player.playerController.height / 2, 0);
            Vector3 direction = player.transform.right;
            float distance = player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeWallRunDetectionDistance;

            if (Physics.Raycast(origin, direction, out hit, distance, player.playerInventoryManager.currentPlayerDataBeingUsed.wallRunLayerMask) ||
                Physics.Raycast(origin, -direction, out hit, distance, player.playerInventoryManager.currentPlayerDataBeingUsed.wallRunLayerMask))
            {
                return true;
            }

            return false;
        }
        public void AttemptToUseSliding()
        {
            player.isSliding = true;
            player.playerStatsManager.DeductStamina(player.playerInventoryManager.currentPlayerDataBeingUsed.slidingStaminaCost);
            player.playerLocomotionManager.slideTimer = player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeSlideDuration;
            player.playerController.height = player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeSlideHeight;
        }
        public void ResetSliding()
        {
            player.isSliding = false;
            player.playerController.height = player.playerLocomotionManager.originalHeight;
        }

        public void AttemptToUseLeftAttack()
        {
            if(player.playerStatsManager.currentStamina >= 0f)
            {
                player.playerStatsManager.DeductStamina(player.playerInventoryManager.currentWeaponDataBeingUsed.baseStaminaCost);
                leftHandHitbox.SetActive(true);
                player.playerLocomotionManager.stationaryTime = 0f;
                WRLD_AUDIO_FX_MANAGER.instance.PlaySoundFixedPitchFX(player.audioSource, player.playerInventoryManager.currentWeaponDataBeingUsed.whooshes[0], 1f, 0.5f);
                player.playerUIManager.crosshairObject.GetComponent<Animator>().CrossFade("Crosshair Anim", 0.2f);
                player.playerAnimatorManager.PlayTargetActionAnimation(player.playerInventoryManager.currentWeaponDataBeingUsed.leftAttackAnimation, false, false, true);
            }
        }
        public void AttemptToUseRightAttack()
        {
            if(player.playerStatsManager.currentStamina >= 0f)
            {
                player.playerStatsManager.DeductStamina(player.playerInventoryManager.currentWeaponDataBeingUsed.baseStaminaCost);
                rightHandHitbox.SetActive(true);
                player.playerLocomotionManager.stationaryTime = 0f;
                WRLD_AUDIO_FX_MANAGER.instance.PlaySoundFixedPitchFX(player.audioSource, player.playerInventoryManager.currentWeaponDataBeingUsed.whooshes[0], 1f, 0.5f);
                player.playerUIManager.crosshairObject.GetComponent<Animator>().CrossFade("Crosshair Anim", 0.2f);
                player.playerAnimatorManager.PlayTargetActionAnimation(player.playerInventoryManager.currentWeaponDataBeingUsed.rightAttackAnimation, false, false, true);
            }
        }

        //public void AttemptToUseDashing()
        //{
        //    player.isDashing = true;

        //    Transform forwardT;

        //    if (player.playerLocomotionManager.useDashCameraForward)
        //        forwardT = playerCam;
        //    else
        //        forwardT = orientation;

        //    Vector3 dashDirection = GetDashDirection(forwardT);

        //    // Apply the dash force
        //    player.controller.Move(dashDirection * player.playerLocomotionManager.dashForce * Time.deltaTime);

        //    // Set a flag in PlayerManager to indicate dashing
        //    player.isDashing = true;

        //    // Invoke the method to reset dashing after the dash duration
        //    Invoke(nameof(ResetDashing), player.playerLocomotionManager.dashDuration);
        //}

        //private void ResetDashing()
        //{
        //    player.isDashing = false;
        //}
        //private Vector3 GetDashDirection(Transform forwardT)
        //{
        //    float horizontalInput = Input.GetAxisRaw("Horizontal");
        //    float verticalInput = Input.GetAxisRaw("Vertical");

        //    Vector3 dashDirection = new Vector3();

        //    if (player.playerLocomotionManager.allowDashInAllDirections)
        //        dashDirection = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        //    else
        //        dashDirection = forwardT.forward;

        //    if (verticalInput == 0 && horizontalInput == 0)
        //        dashDirection = forwardT.forward;

        //    dashDirection.y = 0f; 

        //    return dashDirection.normalized;
        //}
    }
}