using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
        [HideInInspector] public Rigidbody rigidBody;
        [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public CharacterController controller;
        [HideInInspector] public CharacterLocomotionManager characterLocomotionManager;
        [HideInInspector] public CharacterAnimatorManager characterAnimatorManager;
        [HideInInspector] public CharacterCombatManager characterCombatManager;

        [Header("MOVEMENT FLAGS")]
        public bool canMove = true;
        public bool canJump = true;
        public bool applyRootMotion = false;
        public bool isPerformingAction = false;
        public bool isGrounded = true;
        public bool isSprinting = false;
        public bool isDashing = false;
        public bool isWallRunning = false;
        public bool isSliding = false;

        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            controller = GetComponent<CharacterController>();
            characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
            characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
            characterCombatManager = GetComponent<CharacterCombatManager>();
        }
    }
}