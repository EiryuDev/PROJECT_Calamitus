using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_PLAYER_RESET_FLAGS : MonoBehaviour
    {
        [HideInInspector] public PlayerManager player;
        private void Awake()
        {
            player = FindFirstObjectByType<PlayerManager>();    
        }
        public void DisableHitbox()
        {
            player.playerCombatManager.leftHandHitbox.SetActive(false);
            player.playerCombatManager.rightHandHitbox.SetActive(false);
        }
    }
}
