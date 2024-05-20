using UnityEngine;

namespace Nutbusterz.Calamitus
{
    [CreateAssetMenu(menuName = "FFPS/Item/Weapon/Melee")]
    public class WRLD_MELEE_WEAPON_ITEM : WRLD_WEAPON_ITEM
    {
        [Header("MELEE ATTACK DATA")]
        public float meleeRayCastDistance = 2f;
        public float meleeRayCastRadius = 0.1f;

        [Header("ATTACK ANIMATION DATA")]
        [Tooltip("For the left hand attack animation")]
        public string leftAttackAnimation = "Punch Left";
        [Tooltip("For the right hand attack animation")]
        public string rightAttackAnimation = "Punch Right";

        [Header("WEAPON WHOOSHES")]
        [Tooltip("List of audio clip containing weapon whooshes.")]
        public AudioClip[] whooshes;
    }
}
