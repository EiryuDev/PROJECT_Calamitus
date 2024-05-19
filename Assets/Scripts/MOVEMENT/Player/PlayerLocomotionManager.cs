using UnityEngine;
using System.Collections;

namespace Nutbusterz.Calamitus
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        [HideInInspector] public PlayerManager player;
        [HideInInspector] public float verticalMovement; // Value of the vertical movement
        [HideInInspector] public float horizontalMovement; // Value of the horizontal movement

        [Header("MOVEMENT DATA")]
        public Transform groundCheck;
        [HideInInspector] public Vector3 velocity;
        [Header("Wall Running Data")]
        [HideInInspector] public float wallRunTimer;
        [HideInInspector] public Vector3 wallNormal;
        [Header("Sliding Data")]
        [HideInInspector] public float originalHeight;
        [HideInInspector] public float slideTimer;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
            originalHeight = player.controller.height;
        }
        public void UseAllMovement()
        {
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
            float speed = player.isSprinting ? player.playerInventoryManager.currentPlayerDataBeingUsed.sprintingSpeed :
                player.playerInputManager.moveAmount > 0.5f ? player.playerInventoryManager.currentPlayerDataBeingUsed.movementSpeed :
                player.playerInventoryManager.currentPlayerDataBeingUsed.walkingSpeed;
            Vector3 moveDirection = CalculateMoveDirection();

            player.controller.Move(moveDirection * speed * Time.deltaTime);

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
            if (player.canJump)
            {
                // Get the player's forward movement direction
                Vector3 moveDirection = CalculateMoveDirection();

                // Add the forward movement direction to the velocity
                velocity = moveDirection * player.playerInventoryManager.currentPlayerDataBeingUsed.movementSpeed;

                // Add the jump force to the Y velocity
                velocity.y = Mathf.Sqrt(player.playerInventoryManager.currentPlayerDataBeingUsed.jumpForce * -2f * player.playerInventoryManager.currentPlayerDataBeingUsed.gravity);

                // Start the jump cooldown
                StartCoroutine(JumpCooldown());
            }
        }
        private IEnumerator JumpCooldown()
        {
            player.canJump = false;
            yield return new WaitForSeconds(player.playerInventoryManager.currentPlayerDataBeingUsed.jumpCooldown);
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
            character.controller.Move(velocity * Time.deltaTime);

            // Reset the Y velocity and forward velocity after the movement is applied
            if (character.controller.isGrounded)
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
                    player.controller.Move(moveDirection * player.playerInventoryManager.currentPlayerDataBeingUsed.slideSpeed * Time.deltaTime);
                }
            }
        }
        public void AttemptToUseDash()
        {
            // Get the player's movement direction
            Vector3 dashDirection = CalculateMoveDirection();

            // Multiply the dashDirection by dashForce
            dashDirection *= player.playerInventoryManager.currentPlayerDataBeingUsed.dashForce;

            // Add the dashDirection to the velocity
            velocity += dashDirection;

            // Start the PerformDash coroutine
            StartCoroutine(PerformDash());
        }

        private IEnumerator PerformDash()
        {
            float elapsedTime = 0f;
            while (elapsedTime < player.playerInventoryManager.currentPlayerDataBeingUsed.dashDuration)
            {
                elapsedTime += Time.deltaTime;
                player.controller.Move(velocity * Time.deltaTime);
                yield return null;
            }
        }

    }
}