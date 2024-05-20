using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class PlayerInputManager : MonoBehaviour
    {
        PlayerControls playerControls; // Reference to the PlayerControls
        PlayerManager player; // Reference to the Player Manager script

        [Header("PLAYER MOVEMENT INPUT")]
        [SerializeField] Vector2 movementInput; // Storing the data for refrencing the input values of the movement
        public float verticalInput; // Vertical input value for the player movement
        public float horizontalInput; // Horizontal input value for the player movement
        public float moveAmount; // The amount of movement value for the player input

        [Header("PLAYER ACTION INPUT")]
        public bool jump_Input = false; // Checks if the input for the jump is pressed or not
        public bool dash_Input = false; // Checks if the input for dash is pressed or not
        public bool slide_Input = false; // Checks if the input for dash is pressed or not
        public bool jumpInputHandled;
        public bool tapRBInput = false;
        public bool tapRTInput = false;

        void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                // BELOW CODE: Calling movement input
                playerControls.PlayerMovement.Movement.performed +=
                i => movementInput = i.ReadValue<Vector2>();

                // BELOW CODE: Calling jump input
                playerControls.PlayerActions.Jump.performed += i => jump_Input = true;

                // BELOW CODE: Calling dash input 
                playerControls.PlayerActions.Dash.performed += i => dash_Input = true;

                // BELOW CODE: Calling slide input 
                playerControls.PlayerActions.Slide.performed += i => slide_Input = true;

                // BELOW CODE: Calling Tap RB input
                playerControls.PlayerActions.TapRB.performed += i => tapRBInput = true;

                // BELOW CODE: Calling Tap RT input
                playerControls.PlayerActions.TapRT.performed += i => tapRTInput = true;
            }

            playerControls.Enable();
        }
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }
        public void UseAllInputs()
        {
            if (!player.playerUIManager.isPaused)
            {
                UseMovementInput();
                UseJumpInput();
                UseDashInput();
                UseSlideInput();
                UseTapRBInput();
                UseTapRTInput();
            }
        }

        public void UseMovementInput()
        {
            horizontalInput = movementInput.x;
            verticalInput = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            if (moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            if (player == null)
                return;
        }

        public void UseJumpInput()
        {
            if (jump_Input && !jumpInputHandled)
            {
                jump_Input = false;
                jumpInputHandled = true;
                player.playerLocomotionManager.AttemptToPerformJump();
            }
            else if (!jump_Input)
            {
                jumpInputHandled = false;
            }
        }

        public void UseDashInput()
        {
            if(dash_Input)
            {
                dash_Input = false;
                player.playerLocomotionManager.AttemptToUseDash(); // Here we will call Dash Movement
                StartCoroutine(player.playerCameraManager.ShakeCamera()); // Start the camera shake coroutine
            }
        }

        public void UseSlideInput()
        {
            if(slide_Input && player.isGrounded && !player.isSliding)
            {
                slide_Input = false;
                player.playerCombatManager.AttemptToUseSliding();
            }
        }
        public void UseTapRBInput()
        {
            if(tapRBInput)
            {
                tapRBInput = false;
                player.playerCombatManager.AttemptToUseRightAttack();
            }
        }

        public void UseTapRTInput()
        {
            if (tapRTInput)
            {
                tapRTInput = false;
                player.playerCombatManager.AttemptToUseLeftAttack();
            }
        }
    }
}
