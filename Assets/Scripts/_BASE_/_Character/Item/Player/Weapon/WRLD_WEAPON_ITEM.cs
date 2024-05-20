using UnityEngine;

namespace Nutbusterz.Calamitus
{
    [CreateAssetMenu(menuName = "FFPS/Item/Weapon")]
    public class WRLD_WEAPON_ITEM : WRLD_ITEM
    {
        //[Header("WEAPON MODEL")]
        //[Tooltip("Prefab for the weapon model")]
        //public GameObject weaponModel; 

        [Header("WEAPON BASE DAMAGE")]
        [Tooltip("Physical damage of the weapon")]
        public int physicalDamage = 0; 
        [Tooltip("Fire damage of the weapon")]
        public int fireDamage = 0; 
        [Tooltip("Ice damage of the weapon")]
        public int iceDamage = 0; 
        [Tooltip("Lightning damage of the weapon")]
        public int lightningDamage = 0; 

        [Header("WEAPON POISE")]
        [Tooltip("The value of poise break for the weapon")]
        public float poiseDamage; 
        // Offensive poise bonus when attacking

        [Header("DAMAGE MODIFIER")]
        [Tooltip("Bonus damage modifier for the weapon")]
        public float bonus_Damage_Modifier = 1.1f;

        [Header("STAMINA DATA")]
        [Tooltip("Base stamina cost for the weapon")]
        public float baseStaminaCost = 5f;

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
