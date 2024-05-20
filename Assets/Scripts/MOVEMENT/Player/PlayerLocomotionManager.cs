using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace Nutbusterz.Calamitus
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        [HideInInspector] public PlayerManager player;
        [HideInInspector] public float verticalMovement; // Value of the vertical movement
        [HideInInspector] public float horizontalMovement; // Value of the horizontal movement

        [Header("MOVEMENT DATA")]
        public Transform groundCheck;
        private Vector3 lastPosition;
        private float totalDistanceCovered;
        [HideInInspector] public Vector3 velocity;
        [Header("Wall Running Data")]
        [HideInInspector] public float wallRunTimer;
        [HideInInspector] public Vector3 wallNormal;
        [Header("Sliding Data")]
        [HideInInspector] public float originalHeight;
        [HideInInspector] public float slideTimer;

        [Header("SKILL DATA")]
        [Header("BREATHING DATA")]
        public float currentTimeToLevelUpBreathing = 300f;
        [HideInInspector] public float stationaryTime = 0f;
        [Header("LOOKING DATA")]
        public float currentLookingTimeToLevelUp = 60f;
        private float totalLookingTime;
        [Header("WALKING DATA")]
        public float currentDistanceToLevelUp = 100f;

        private DepthOfField depthOfField;
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }
        private void Start()
        {
            originalHeight = player.playerController.height;

            if (player.globalVolume != null && player.globalVolume.profile != null)
            {
                if (!player.globalVolume.profile.TryGet(out depthOfField))
                {
                    Debug.LogError("DepthOfField component not found in the Volume profile.");
                }
                else
                {
                    Debug.Log("DepthOfField component found in the Volume profile.");
                }
            }
            else
            {
                Debug.LogError("Global Volume or its profile is not assigned.");
            }

        }
        public void UseAllMovement()
        {
            UseTrackLookingTime();
            UseTrackBreathingSkill();
            UseTrackDistance();

            // BELOW CODE: Grounded movement
            UseGroundedMovement();
            // BELOW CODE: Jumping movement
            UseJumpingMovement();
            // BELOW CODE: Sliding movement
            UseSlidingMovement();
        }
        private void GetMovementValues()
        {
            verticalMovement = player.playerInputManager.verticalInput;
            horizontalMovement = player.playerInputManager.horizontalInput;
        }
        private void UseGroundedMovement()
        {
            if (!player.canMove || player.isPerformingAction || !player.isGrounded)
                return; // To stop the player from moving while interacting in the falling

            GetMovementValues();
            MovePlayer();
        }
        private void MovePlayer()
        {
            float speed = player.isSprinting ? player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeWalkingSpeed :
                player.playerInputManager.moveAmount > 0.5f ? player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeWalkingSpeed :
                player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeWalkingSpeed;
            Vector3 moveDirection = CalculateMoveDirection();

            player.playerController.Move(moveDirection * speed * Time.deltaTime);

            if (player.isSprinting)
            {
                // TO-DO: Deduct stamina 
            }
        }
        public Vector3 CalculateMoveDirection()
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;

            Vector3 moveDirection = (cameraForward * verticalMovement) + (cameraRight * horizontalMovement);
            moveDirection.y = 0;
            moveDirection.Normalize();

            return moveDirection;
        }
        public void AttemptToPerformJump()
        {
            if (player.canJump && player.playerStatsManager.currentStamina >= 0f)
            {
                player.playerStatsManager.DeductStamina(player.playerInventoryManager.currentPlayerDataBeingUsed.jumpingStaminaCost);

                // Get the player's forward movement direction
                Vector3 moveDirection = CalculateMoveDirection();

                // Add the forward movement direction to the velocity
                velocity = moveDirection * player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeWalkingSpeed;

                // Add the jump force to the Y velocity
                velocity.y = Mathf.Sqrt(player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeJumpForce * -2f * player.playerInventoryManager.currentPlayerDataBeingUsed.gravity);

                // Start the jump cooldown
                StartCoroutine(JumpCooldown());
            }
        }
        private IEnumerator JumpCooldown()
        {
            player.canJump = false;
            yield return new WaitForSeconds(player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeJumpCooldown);
            player.canJump = true;
        }

        public void UseJumpingMovement()
        {
            character.isGrounded = Physics.CheckSphere(groundCheck.position, player.playerInventoryManager.currentPlayerDataBeingUsed.groundDistance, player.playerInventoryManager.currentPlayerDataBeingUsed.groundMask);

            if (character.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += player.playerInventoryManager.currentPlayerDataBeingUsed.gravity * Time.deltaTime;
            player.playerController.Move(velocity * Time.deltaTime);

            // Reset the Y velocity and forward velocity after the movement is applied
            if (player.playerController.isGrounded)
            {
                velocity.y = 0f;
                // Reset the forward velocity
                velocity.x = 0f;
                velocity.z = 0f;
            }
        }
        public void UseSlidingMovement()
        {
            if (player.isSliding)
            {
                slideTimer -= Time.deltaTime;
                if (slideTimer <= 0)
                {
                    player.playerCombatManager.ResetSliding();
                }
                else
                {
                    Vector3 moveDirection = CalculateMoveDirection();
                    player.playerController.Move(moveDirection * player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeSlideSpeed * Time.deltaTime);
                }
            }
        }
        public void AttemptToUseDash()
        {
            player.playerStatsManager.DeductStamina(player.playerInventoryManager.currentPlayerDataBeingUsed.dashingStaminaCost);

            // Get the player's movement direction
            Vector3 dashDirection = CalculateMoveDirection();

            // Multiply the dashDirection by dashForce
            dashDirection *= player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeDashForce;

            // Add the dashDirection to the velocity
            velocity += dashDirection;

            // Start the PerformDash coroutine
            StartCoroutine(PerformDash());
        }

        private IEnumerator PerformDash()
        {
            float elapsedTime = 0f;
            while (elapsedTime < player.playerInventoryManager.currentPlayerDataBeingUsed.runtimeDashDuration)
            {
                elapsedTime += Time.deltaTime;
                player.playerController.Move(velocity * Time.deltaTime);
                yield return null;
            }
        }
        private void UseTrackBreathingSkill()
        {
            if (player.playerInputManager.moveAmount == 0)
            {
                stationaryTime += Time.deltaTime;

                if (stationaryTime >= currentTimeToLevelUpBreathing)
                {
                    LevelUpBreathingSkill();
                    stationaryTime = 0f; // Reset the timer after leveling up
                }
            }
            else
            {
                stationaryTime = 0f; // Reset the timer if the player starts moving
            }
        }
        private void UseTrackLookingTime()
        {
            totalLookingTime += Time.deltaTime;

            // Check if enough time has been spent to level up
            if (totalLookingTime >= currentLookingTimeToLevelUp)
            {
                totalLookingTime = 0;
                LevelUpLookingSkill();
            }
        }
        private void UseTrackDistance()
        {
            // Calculate distance covered since the last frame
            float distanceCovered = Vector3.Distance(lastPosition, transform.position);
            totalDistanceCovered += distanceCovered;
            lastPosition = transform.position;

            // Check if enough distance has been covered to level up
            if (totalDistanceCovered >= currentDistanceToLevelUp)
            {
                totalDistanceCovered = 0;
                LevelUpWalkingSkill();
            }
        }

        private void LevelUpBreathingSkill()
        {
            WRLD_SKILL_ITEM skillItem = player.playerInventoryManager.currentSkillDataBeingUsed;
            skillItem.runtimeBreathingSkillLevel++;
            currentTimeToLevelUpBreathing += player.playerInventoryManager.currentSkillDataBeingUsed.increaseTimeToLevelUpBreathing;
            StartCoroutine(ShowLevelUpInfo("BREATHING INCREASE TO " + (player.playerInventoryManager.currentSkillDataBeingUsed.runtimeBreathingSkillLevel)));
            Debug.Log("Breathing Skill Leveled up to: " + (player.playerInventoryManager.currentSkillDataBeingUsed.runtimeBreathingSkillLevel));
            // You can add additional logic here if you want to increase other stats or provide feedback to the player
        }
        private void LevelUpLookingSkill()
        {
            if (depthOfField != null)
            {
                Debug.Log($"Current focusDistance value: {depthOfField.focusDistance.value}");
                Debug.Log($"Current aperture value: {depthOfField.aperture.value}");

                depthOfField.focusDistance.value += player.playerInventoryManager.currentSkillDataBeingUsed.increasingLookDistance;
                depthOfField.aperture.value += player.playerInventoryManager.currentSkillDataBeingUsed.increasingLookAperture;
                depthOfField.focusDistance.overrideState = true;
                depthOfField.aperture.overrideState = true;

                Debug.Log($"Updated focusDistance value: {depthOfField.focusDistance.value}");
                Debug.Log($"Updated aperture value: {depthOfField.aperture.value}");
            }

            WRLD_SKILL_ITEM skillItem = player.playerInventoryManager.currentSkillDataBeingUsed;
            skillItem.runtimeLookingSkillLevel++;
            currentLookingTimeToLevelUp += player.playerInventoryManager.currentSkillDataBeingUsed.increaseLookingToLevelUp; // Increase time required to level up for next level
            StartCoroutine(ShowLevelUpInfo("LOOKING INCREASE TO " + (player.playerInventoryManager.currentSkillDataBeingUsed.runtimeLookingSkillLevel)));
            Debug.Log("Looking Skill Leveled up to: " + (player.playerInventoryManager.currentSkillDataBeingUsed.runtimeLookingSkillLevel));  
        }
        private void LevelUpWalkingSkill()
        {
            WRLD_SKILL_ITEM skillItem = player.playerInventoryManager.currentSkillDataBeingUsed;
            WRLD_PLAYER_ITEM playerItem = player.playerInventoryManager.currentPlayerDataBeingUsed;
            skillItem.runtimeWalkingSkillLevel++;
            playerItem.runtimeWalkingSpeed += player.playerInventoryManager.currentSkillDataBeingUsed.increasingWalkingSpeed;
            currentDistanceToLevelUp += player.playerInventoryManager.currentSkillDataBeingUsed.increaseWalkingToLevelUp;
            StartCoroutine(ShowLevelUpInfo("WALKING INCREASE TO " + (player.playerInventoryManager.currentSkillDataBeingUsed.runtimeWalkingSkillLevel)));
            Debug.Log("Walking Skill Leveled up to: " + (player.playerInventoryManager.currentSkillDataBeingUsed.runtimeWalkingSkillLevel));
        }

        private IEnumerator ShowLevelUpInfo(string info)
        {
            player.playerUIManager.levelUpInfo.SetActive(true);
            player.playerUIManager.levelUpInfo.GetComponent<TextMeshProUGUI>().text = info;
            yield return null;
        }
    }
}