using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        [HideInInspector] public PlayerManager player;

        [Header("PLAYER DATA")]
        public WRLD_PLAYER_ITEM currentPlayerDataBeingUsed;

        [Header("WEAPON DATA")]
        public WRLD_WEAPON_ITEM currentWeaponDataBeingUsed;

        [Header("SKILL DATA")]
        public WRLD_SKILL_ITEM currentSkillDataBeingUsed;

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }
    }
}
