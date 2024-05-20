using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class PlayerUIManager : MonoBehaviour
    {
        [HideInInspector] public PlayerManager player;

        [Header("GUI DATA")]
        public GameObject crosshairObject;
        public GameObject levelUpInfo;
        [Header("STATS DATA")]
        public UI_HealthBar healthBar;
        public UI_StaminaBar staminaBar;
        private void Awake()
        {
            player = FindFirstObjectByType<PlayerManager>();    
        }
    }
}
