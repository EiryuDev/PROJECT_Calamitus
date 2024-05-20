using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class PlayerStatsManager : MonoBehaviour
    {
        [HideInInspector] public PlayerManager player;

        [Header("PLAYER DATA")]
        public int currentHealth = 100;
        public int maxHealth = 100;

        [Header("STAMINA DATA")]
        public float maxStamina; // Max stamina of the player
        public float currentStamina; // Current stamina of the player
        public float staminaRegenAmount = 5; // How much the regen amount for the stamina regen
        public float staminaRegenTimer = 0; // Timer for the stamina regen
        private float sprintingTimer = 0;
        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }
        void Start()
        {
            player.playerUIManager.healthBar.SetMaxHealth(maxHealth);
            player.playerUIManager.staminaBar.SetMaxStamina(maxStamina);
            player.playerInventoryManager.currentSkillDataBeingUsed.Refresh();
            player.playerInventoryManager.currentPlayerDataBeingUsed.Refresh(); 
        }
        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            player.playerUIManager.healthBar.SetCurrentHealth(currentHealth);
        }
        public void DeductStamina(float staminaToDeduct)
        {
            currentStamina = currentStamina - staminaToDeduct;
            player.playerUIManager.staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
        }
        public void DeductSprintingStamina(float staminaToDeduct)
        {
            if (player.isSprinting)
            {
                sprintingTimer = sprintingTimer + Time.deltaTime;

                if (sprintingTimer > 0.1f)
                {
                    // Reset Timer
                    sprintingTimer = 0;
                    // Deduct Stamina
                    currentStamina = currentStamina - staminaToDeduct;
                    player.playerUIManager.staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
            else
            {
                sprintingTimer = 0;
            }
        }
        public void RegenStamina()
        {
            // BELOW CODE: Do not regenerate stamina if we performing an action or sprinting
            if (player.isPerformingAction || player.isSprinting)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaRegenTimer > 1f)
                {
                    currentStamina += staminaRegenAmount * Time.deltaTime;
                    player.playerUIManager.staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }
    }
}
