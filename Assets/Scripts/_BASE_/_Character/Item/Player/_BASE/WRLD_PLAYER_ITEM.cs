using UnityEngine;

namespace Nutbusterz.Calamitus
{
    [CreateAssetMenu(menuName = "FFPS/Item/Player/Player Data")]
    public class WRLD_PLAYER_ITEM : WRLD_ITEM
    {
        [Header("MOVEMENT DATA")]
        [Header("SKILL CHANGE DATA")]
        [Tooltip("How much is the walking speed")]
        public float baseWalkingSpeed = 3.0f;
        public float runtimeWalkingSpeed = 3.0f; 

        [Header("JUMP DATA")]
        [Header("SKILL CHANGE DATA")]
        [Tooltip("How much is the jump force")]
        public float baseJumpForce = 10f;
        [HideInInspector] public float runtimeJumpForce = 10f; 
        [Tooltip("How long the player will jump")]
        public float baseJumpCooldown = 0.2f;
        [HideInInspector] public float runtimeJumpCooldown = 0.2f;

        [Header("STATIC CHANGE DATA")]
        public float gravity = -9.81f;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        [Header("DASH DATA")]
        [Header("SKILL CHANGE DATA")]
        [Tooltip("How much is the dash force")]
        public float baseDashForce = 20f;
        [HideInInspector] public float runtimeDashForce = 20f;
        [Tooltip("How much is the dash duration")]
        public float baseDashDuration = 5f;
        [HideInInspector] public float runtimeDashDuration = 5f;

        [Header("STATIC CHANGE DATA")]
        public bool useDashCameraForward = true;
        public bool allowDashInAllDirections = true;
        public bool resetDashVelocity = true;

        [Header("WALL RUNNING DATA")]
        [Header("SKILL CHANGE DATA")]
        [Tooltip("How much is the wall running speed")]
        public float baseWallRunSpeed = 5;
        [HideInInspector] public float runtimeWallRunSpeed = 5;
        [Tooltip("How much is the wall running force")]
        public float baseWallRunForce = 5f;
        [HideInInspector] public float runtimeWallRunForce = 5f;
        [Tooltip("How much is the wall running maximum time")]
        public float baseWallRunMaxTime = 1f;
        [HideInInspector] public float runtimeWallRunMaxTime = 1f;
        [Tooltip("How much is the wall run gravity")]
        public float baseWallRunGravity = -5f;
        [HideInInspector] public float runtimeWallRunGravity = -5f;
        [Tooltip("How much is the wall run detection distance")]
        public float baseWallRunDetectionDistance = 1f;
        [HideInInspector] public float runtimeWallRunDetectionDistance = 1f;

        [Header("STATIC CHANGE DATA")]
        [Tooltip("Which is the layer for the wall for wall run")]
        public LayerMask wallRunLayerMask;

        [Header("SLIDING DATA")]
        [Tooltip("How much is the sliding speed")]
        public float baseSlideSpeed = 10f;
        [HideInInspector] public float runtimeSlideSpeed = 10f;
        [Tooltip("How much is the sliding duration")]
        public float baseSlideDuration = 1f;
        [HideInInspector] public float runtimeSlideDuration = 1f;
        [Tooltip("How much is the sliding Height")]
        public float baseSlideHeight = 0.5f;
        [HideInInspector] public float runtimeSlideHeight = 0.5f;

        [Header("PUNCHING DATA")]
        public float basePunchingForce = 500f;
        [HideInInspector] public float runtimePunchingForce = 500f;

        [Header("STATS DATA")]
        public float movementStaminaCost = 1f;
        public float jumpingStaminaCost = 5f;
        public float slidingStaminaCost = 10f;
        public float dashingStaminaCost = 20f;
        public float wallRunStaminaCost = 2f;

        private void OnEnable()
        {
            runtimeWalkingSpeed = baseWalkingSpeed;

            runtimeJumpForce = baseJumpForce;
            runtimeJumpCooldown = baseJumpCooldown;

            runtimeDashForce = baseDashForce;
            runtimeDashDuration = baseDashDuration;

            runtimeWallRunSpeed = baseWallRunSpeed;
            runtimeWallRunForce = baseWallRunForce;
            runtimeWallRunMaxTime = baseWallRunMaxTime; 
            runtimeWallRunGravity = baseWallRunGravity;
            runtimeWallRunDetectionDistance = baseWallRunDetectionDistance;

            runtimeSlideSpeed = baseSlideSpeed;
            runtimeSlideDuration = baseSlideDuration;
            runtimeSlideHeight = baseSlideHeight;

            runtimePunchingForce = basePunchingForce;
        }
    }
}
