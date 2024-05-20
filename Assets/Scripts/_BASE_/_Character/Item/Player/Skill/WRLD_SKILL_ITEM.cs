using UnityEngine;

namespace Nutbusterz.Calamitus
{
    [CreateAssetMenu(menuName = "FFPS/Item/Player/Skill Data")]
    public class WRLD_SKILL_ITEM : WRLD_ITEM
    {
        [Header("MOVEMENT DATA")]
        [Header("NORMAL SKILL DATA")]
        [Tooltip("How much is the breathing skill level")]
        public float baseBreathingSkillLevel = 1f;
        [HideInInspector] public float runtimeBreathingSkillLevel = 1f;
        [Tooltip("How much is the looking skill level")]
        public float baseLookingSkillLevel = 1f;
        [HideInInspector] public float runtimeLookingSkillLevel = 1f;
        [Tooltip("How much is the walking skill level")]
        public float baseWalkingSkillLevel = 1f;
        [HideInInspector] public float runtimeWalkingSkillLevel = 1f;

        [Header("JUMPING SKILL DATA")]
        [Tooltip("How much is the jumping skill level")]
        public float baseJumpingSkillLevel = 1f;
        [HideInInspector] public float runtimeJumpingSkillLevel = 1f;
        [Tooltip("How much is the falling skill level")]
        public float baseFallingSkillLevel = 1f;
        [HideInInspector] public float runtimeFallingSkillLevel = 1f;
        [Tooltip("How much is the landing skill level")]
        public float baseLandingSkillLevel = 1f;
        [HideInInspector] public float runtimeLandingSkillLevel = 1f;

        [Header("DASHING SKILL DATA")]
        [Tooltip("How much is the dashing skill level")]
        public float baseDashingSkillLevel = 1f;
        [HideInInspector] public float runtimeDashingSkillLevel = 1f;

        [Header("WALL RUNNING SKILL DATA")]
        [Tooltip("How much is the wall running skill level")]
        public float baseWallRunningSkillLevel = 1f;
        [HideInInspector] public float runtimeWallRunningSkillLevel = 1f;
        [Tooltip("How much is the wall jumping skill level")]
        public float baseWallJumpingSkillLevel = 1f;
        [HideInInspector] public float runtimeWallJumpingSkillLevel = 1f;

        [Header("SLIDING SKILL DATA")]
        [Tooltip("How much is the sliding skill level")]
        public float baseSlidingSkillLevel = 1f;
        [HideInInspector] public float runtimeSlidingSkillLevel = 1f;

        [Header("COMBAT DATA")]
        [Tooltip("How much is the punching damage skill level")]
        public float basePunchingDamageSkillLevel = 1f;
        [HideInInspector] public float runtimePunchingDamageSkillLevel = 1f;
        [Tooltip("How much is the punching speed skill level")]
        public float basePunchingSpeedSkillLevel = 1f;
        [HideInInspector] public float runtimePunchingSpeedSkillLevel = 1f;

        [Header("INCREASE DATA")]
        [Header("LOOKING DATA")]
        public float increaseTimeToLevelUpBreathing = 18f;
        [Header("LOOKING DATA")]
        public float increaseLookingToLevelUp = 15f;
        [Header("WALKING DATA")]
        public float increasingWalkingSpeed = 0.5f;
        public float increaseWalkingToLevelUp = 200f;

        private void OnEnable()
        {
            runtimeBreathingSkillLevel = baseBreathingSkillLevel;
            runtimeLookingSkillLevel = baseLookingSkillLevel;
            runtimeWalkingSkillLevel = baseWalkingSkillLevel;

            runtimeJumpingSkillLevel = baseJumpingSkillLevel;
            runtimeFallingSkillLevel = baseFallingSkillLevel;
            runtimeLandingSkillLevel = baseLandingSkillLevel;

            runtimeDashingSkillLevel = baseDashingSkillLevel;

            runtimeWallRunningSkillLevel = baseWallRunningSkillLevel;
            runtimeWallJumpingSkillLevel = baseWallJumpingSkillLevel;

            runtimeSlidingSkillLevel = baseSlidingSkillLevel;

            runtimePunchingDamageSkillLevel = basePunchingDamageSkillLevel;
            runtimePunchingSpeedSkillLevel=  basePunchingSpeedSkillLevel;
        }
    }
}