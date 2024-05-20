using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class AICharacterStatsManager : MonoBehaviour
    {
        [HideInInspector] public AICharacterManager aiCharacter;

        [Header("STATS BAR")]
        public UI_HealthBar healthBar;

        [Header("PLAYER DATA")]
        public int currentHealth = 100;
        public int maxHealth = 100;

        private void Awake()
        {
            aiCharacter = GetComponent<AICharacterManager>();
        }
        void Start()
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            healthBar.SetCurrentHealth(currentHealth);

            if(currentHealth <= 0)
            {
                Destroy(aiCharacter.gameObject);   
            }
        }
    }
}
