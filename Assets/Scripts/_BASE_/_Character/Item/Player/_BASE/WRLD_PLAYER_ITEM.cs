using UnityEngine;

namespace Nutbusterz.Calamitus
{
    [CreateAssetMenu(menuName = "FFPS/Item/Player/Player Data")]
    public class WRLD_PLAYER_ITEM : WRLD_ITEM
    {
        [Header("MOVEMENT DATA")]
        [Header("SKILL CHANGE DATA")]
        [Tooltip("How much is the movement speed")]
        public float movementSpeed = 3.0f; 
        [Tooltip("How much is the walking speed")]
        public float walkingSpeed = 3.0f; 
        [Tooltip("How much is the sprint speed")]
        public float sprintingSpeed = 3.0f; 

        [Header("JUMP DATA")]
        [Header("SKILL CHANGE DATA")]
        [Tooltip("How much is the jump force")]
        public float jumpForce = 10f; 
        [Tooltip("How long the player will jump")]
        public float jumpCooldown = 0.2f;
        [Header("STATIC CHANGE DATA")]
        public float gravity = -9.81f;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        [Header("DASH DATA")]
        [Header("SKILL CHANGE DATA")]
        [Tooltip("How much is the dash force")]
        public float dashForce = 20f;
        [Tooltip("How much is the dash duration")]
        public float dashDuration = 5f;
        [Header("STATIC CHANGE DATA")]
        public bool useDashCameraForward = true;
        public bool allowDashInAllDirections = true;
        public bool resetDashVelocity = true;

        [Header("WALL RUNNING DATA")]
        [Header("SKILL CHANGE DATA")]
        [Tooltip("How much is the wall running speed")]
        public float wallRunSpeed = 5;
        [Tooltip("How much is the wall running force")]
        public float wallRunForce = 5f;
        [Tooltip("How much is the wall running maximum time")]
        public float wallRunMaxTime = 1f;
        [Tooltip("How much is the wall run gravity")]
        public float wallRunGravity = -5f;
        [Tooltip("How much is the wall run detection distance")]
        public float wallRunDetectionDistance = 1f;
        [Header("STATIC CHANGE DATA")]
        [Tooltip("Which is the layer for the wall for wall run")]
        public LayerMask wallRunLayerMask;

        [Header("SLIDING DATA")]
        [Tooltip("How much is the sliding speed")]
        public float slideSpeed = 10f;
        [Tooltip("How much is the sliding duration")]
        public float slideDuration = 1f;
        [Tooltip("How much is the sliding Height")]
        public float slideHeight = 0.5f;
    }
}
