using TMPro;
using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class PlayerUIManager : MonoBehaviour
    {
        [HideInInspector] public PlayerManager player;

        [Header("GUI DATA")]
        public GameObject crosshairObject;
        public GameObject levelUpInfo;
        public GameObject skillMenu;
        [Header("STATS DATA")]
        public UI_HealthBar healthBar;
        public UI_StaminaBar staminaBar;

        [Header("FLAGS")]
        public bool isPaused = false;
        private bool isActive = true;

        [Header("SKILL DATA")]
        public TextMeshProUGUI levelSkillLevel;
        public TextMeshProUGUI breathingSkillLevel;
        public TextMeshProUGUI lookingSkillLevel;
        public TextMeshProUGUI walkingSkillLevel;
        public TextMeshProUGUI jumpingSkillLevel;
        public TextMeshProUGUI fallingSkillLevel;
        public TextMeshProUGUI landingSkillLevel;
        public TextMeshProUGUI dashingSkillLevel;
        public TextMeshProUGUI wallRunningSkillLevel;
        public TextMeshProUGUI wallJumpingSkillLevel;
        public TextMeshProUGUI slidingSkillLevel;
        public TextMeshProUGUI punchingDamageSkillLevel;
        public TextMeshProUGUI punchingSpeedSkillLevel;
        private void Awake()
        {
            player = FindFirstObjectByType<PlayerManager>();    
        }
        public void UseTheSkillStats()
        {
            breathingSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeBreathingSkillLevel.ToString();
            lookingSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeLookingSkillLevel.ToString();   
            walkingSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeWalkingSkillLevel.ToString();
            jumpingSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeJumpingSkillLevel.ToString();
            fallingSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeFallingSkillLevel.ToString();
            landingSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeLandingSkillLevel.ToString();
            dashingSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeDashingSkillLevel.ToString();
            wallRunningSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeWallRunningSkillLevel.ToString();
            wallJumpingSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeWallJumpingSkillLevel.ToString();
            slidingSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimeSlidingSkillLevel.ToString();
            punchingDamageSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimePunchingDamageSkillLevel.ToString();
            punchingSpeedSkillLevel.text = player.playerInventoryManager.currentSkillDataBeingUsed.runtimePunchingDamageSkillLevel.ToString();
        }
        public void AttemptToUseSkillMenu()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                // Toggle the active state of the target object
                skillMenu.SetActive(!skillMenu.activeSelf);

                // Check if the game is currently paused
                if (isPaused)
                {
                    // Resume the game
                    Time.timeScale = 1;
                }
                else
                {
                    // Pause the game
                    Time.timeScale = 0;
                }

                // Toggle the isPaused variable
                isPaused = !isPaused;
            }
        }

        public void AttemptToShowHUD()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                isActive = !isActive;
                healthBar.gameObject.SetActive(isActive);
                staminaBar.gameObject.SetActive(isActive);
                crosshairObject.SetActive(isActive);
            }
        }
    }
}
