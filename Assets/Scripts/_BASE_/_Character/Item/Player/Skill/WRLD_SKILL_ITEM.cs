using UnityEngine;

namespace Nutbusterz.Calamitus
{
    [CreateAssetMenu(menuName = "FFPS/Item/Player/Skill Data")]
    public class WRLD_SKILL_ITEM : WRLD_ITEM
    {
        [Header("MOVEMENT DATA")]
        [Header("NORMAL SKILL DATA")]
        [Tooltip("How much is the breathing skill level")]
        public float breathingSkillLevel = 1f;
        [Tooltip("How much is the looking skill level")]
        public float lookingSkillLevel = 1f;
        [Tooltip("How much is the walking skill level")]
        public float walkingSkillLevel = 1f;
        [Tooltip("How much is the sprinting skill level")]
        public float sprintingSkillLevel = 1f;
        [Header("JUMPING SKILL DATA")]
        [Tooltip("How much is the jumping skill level")]
        public float jumpingSkillLevel = 1f;
        [Tooltip("How much is the falling skill level")]
        public float fallingSkillLevel = 1f;
        [Tooltip("How much is the landing skill level")]
        public float landingSkillLevel = 1f;
        [Header("DASHING SKILL DATA")]
        [Tooltip("How much is the dashing skill level")]
        public float dashingSkillLevel = 1f;
        [Header("WALL RUNNING SKILL DATA")]
        [Tooltip("How much is the wall running skill level")]
        public float wallRunningSkillLevel = 1f;
        [Tooltip("How much is the wall jumping skill level")]
        public float wallJumpingSkillLevel = 1f;
        [Header("SLIDING SKILL DATA")]
        [Tooltip("How much is the sliding skill level")]
        public float slidingSkillLevel = 1f;

        [Header("COMBAT DATA")]
        [Tooltip("How much is the punching damage skill level")]
        public float punchingDamageSkillLevel = 1f;
        [Tooltip("How much is the punching speed skill level")]
        public float punchingSpeedSkillLevel = 1f;
    }
}